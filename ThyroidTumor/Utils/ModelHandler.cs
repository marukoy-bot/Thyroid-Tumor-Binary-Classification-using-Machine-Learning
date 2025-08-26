using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThyroidTumor.Utils
{
    public class ModelHandler
    {
        Process? pythonServerProcess = null;
        public async Task<string> PredictImage(string method, string imagePath)
        {
            if (method == "local")
            {
                string exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                //string scriptDir = Path.GetFullPath(Path.Combine(exeDir, @"..\..\..\PythonAPI"));
                string scriptDir = Path.Combine(exeDir, "PythonAPI");
                string scriptPath = Path.Combine(scriptDir, "predict.py");

                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{scriptPath}\" \"{imagePath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = scriptDir
                };

                using var process = new Process { StartInfo = psi };
                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    MessageBox.Show("Prediction Error: " + error);
                    return "{}";
                }

                return output;
            }
            else if (method == "api")
            {
                using var client = new HttpClient();
                using var content = new MultipartFormDataContent();
                using var imageContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                content.Add(imageContent, "file", Path.GetFileName(imagePath));

                try
                {
                    var response = await client.PostAsync("http://127.0.0.1:8000/predict", content);
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("API Error: " + ex.Message);
                    return "{}";
                }
            }
            else
            {
                MessageBox.Show("Invalid prediction method specified.");
                return "{}";
            }
        }

        private TaskCompletionSource<bool>? serverReadyTcs;

        public async Task<bool> StartPythonServerAsync()
        {
            try
            {
                KillPort8000Process();
                if (pythonServerProcess != null && !pythonServerProcess.HasExited)
                    return true;

                serverReadyTcs = new TaskCompletionSource<bool>();

                string exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                string scriptDir = Path.GetFullPath(Path.Combine(exeDir, "PythonAPI"));

                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "start_server.py",
                    WorkingDirectory = scriptDir,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                pythonServerProcess = new Process { StartInfo = psi };

                pythonServerProcess.OutputDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        Debug.WriteLine("OUT: " + e.Data);
                        if (e.Data.Contains("Uvicorn running on http://127.0.0.1:8000"))
                        {
                            serverReadyTcs?.TrySetResult(true);
                        }
                    }
                };

                pythonServerProcess.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        Debug.WriteLine("ERR: " + e.Data);
                        if (e.Data.Contains("Uvicorn running on http://127.0.0.1:8000"))
                        {
                            serverReadyTcs?.TrySetResult(true);
                        }
                    }
                };

                pythonServerProcess.Start();
                pythonServerProcess.BeginOutputReadLine();
                pythonServerProcess.BeginErrorReadLine();

                bool isReady = await WaitForServerReadyAsync("http://127.0.0.1:8000/docs", 60000);

                return isReady;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start Python Server:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void StopPythonServer()
        {
            if (pythonServerProcess != null && !pythonServerProcess.HasExited)
            {
                pythonServerProcess.Kill(true);  // Force kill including children
                pythonServerProcess.Dispose();
                pythonServerProcess = null;
            }
        }

        private void KillPort8000Process()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C netstat -aon | findstr :8000",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);
                string output = process!.StandardOutput.ReadToEnd();
                process.WaitForExit();

                var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 5 && parts[1].EndsWith(":8000"))
                    {
                        int pid = int.Parse(parts[4]);
                        try
                        {
                            Process.GetProcessById(pid).Kill();
                            Debug.WriteLine($"Killed process using port 8000: PID {pid}");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Failed to kill PID {pid}: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error checking/killing port 8000: " + ex.Message);
            }
        }

        private async Task<bool> WaitForServerReadyAsync(string url, int timeoutMs = 30000)
        {
            var sw = Stopwatch.StartNew();
            using var httpClient = new HttpClient();

            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                try
                {
                    var res = await httpClient.GetAsync(url);
                    if (res.IsSuccessStatusCode)
                        return true;
                }
                catch
                {
                    // ignore until server is up
                }

                await Task.Delay(500);
            }

            return false;
        }

    }
}
