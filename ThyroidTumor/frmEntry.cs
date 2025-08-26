using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using ThyroidTumor.Models;
using ThyroidTumor.Utils;
using System.Drawing.Drawing2D;

namespace ThyroidTumor
{
    public partial class frmEntry : Form
    {
        readonly ModelHandler handler = new();
        (int Width, int Height) formDims;
        (int Width, int Height) picBoxDims;

        public frmEntry()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

        }

        void UpdateGradient()
        {
            if (BackgroundImage != null)
                BackgroundImage.Dispose();

            Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            using (LinearGradientBrush brush = new LinearGradientBrush(
                    ClientRectangle,
                    Color.AliceBlue,
                    Color.PowderBlue,
                    LinearGradientMode.Vertical
                ))
            {
                g.FillRectangle(brush, ClientRectangle);
            }

            BackgroundImage = bmp;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateGradient();
        }

        private async void frmEntry_Load(object sender, EventArgs e)
        {
            formDims = (ClientSize.Width, ClientSize.Height);
            picBoxDims = (pictureBox1.Width, pictureBox1.Height);
            Cursor = Cursors.WaitCursor;
            btnBrowse.Enabled = false;
            lblServerStatus.Text = "Starting Server...";
            lblServerStatus.ForeColor = Color.Orange;

            if (await handler.StartPythonServerAsync())
            {
                lblServerStatus.Text = "Online";
                lblServerStatus.ForeColor = Color.ForestGreen;
            }
            else
            {
                lblServerStatus.Text = "Offline";
                lblServerStatus.ForeColor = Color.Red;
            }

            Cursor = Cursors.Default;
            btnBrowse.Enabled = true;
        }

        private async void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop)!;

            if (files.Length > 0)
            {
                string imagePath = files[0];
                string ext = Path.GetExtension(imagePath).ToLower();

                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                    string result = await handler.PredictImage("api", imagePath) ?? "{}";

                    if (result.Trim().StartsWith("{"))
                    {
                        var resultObj = JsonSerializer.Deserialize<PredictionResult?>(result);
                        lblResult.Text = $"Prediction: {resultObj?.label}";
                        //lblResult.Text = $"Prediction: {resultObj?.label} | Prediction Value: {resultObj?.prediction_value:F4}";
                    }
                    else
                    {
                        MessageBox.Show("Script Returned invalid response:\n" + result);
                        lblResult.Text = "Prediction Failed";
                    }

                }
                else
                {
                    MessageBox.Show("Unsupported File Type, Please use only JPG, JPEG, or PNG image file types.");
                }
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private async void btnBrowse_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png) |*.jpg;*.jpeg;*.png";
            openFileDialog.Title = "Select an Image";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                txtImagePath.Text = imagePath;
                lblResult.Text = "Analyzing...";
                pictureBox1.Image = Image.FromFile(imagePath);

                try
                {
                    Cursor = Cursors.WaitCursor;
                    btnBrowse.Enabled = false;
                    string result = await handler.PredictImage("api", imagePath) ?? "{}";

                    if (result.Trim().StartsWith("{"))
                    {
                        var resultObj = JsonSerializer.Deserialize<PredictionResult?>(result);
                        lblResult.Text = $"Prediction: {resultObj?.label}";
                        //lblResult.Text = $"Prediction: {resultObj?.label} | Prediction Value: {resultObj?.prediction_value:F4}";
                    }
                    else
                    {
                        MessageBox.Show("Script Returned invalid response:\n" + result);
                        lblResult.Text = "Prediction Failed";
                    }
                }
                finally
                {
                    Cursor = Cursors.Default;
                    btnBrowse.Enabled = true;
                }
            }
        }

        private void frmEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            handler.StopPythonServer();
        }

        private void frmEntry_Resize(object sender, EventArgs e)
        {
            //int prevDist = pictureBox1.Left;
            if (ClientSize.Width < formDims.Width)
            {
                pictureBox1.Left = (ClientSize.Width - pictureBox1.Width) / 2;
                pictureBox1.Width = 255;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.ActiveControl = null;
        }
    }
}
