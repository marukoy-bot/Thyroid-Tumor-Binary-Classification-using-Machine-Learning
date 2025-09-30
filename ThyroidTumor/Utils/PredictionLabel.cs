using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class PredictionLabel : Control
{
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Prediction { get; set; } = "";

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double Accuracy { get; set; } = 0;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using var regFont = new Font(Font, FontStyle.Regular);
        using var boldFont = new Font(Font, FontStyle.Bold);
        using var brush = new SolidBrush(ForeColor);

        string prefix = "Prediction: ";
        string middle = Prediction;
        string suffix = $" | Accuracy: {Accuracy * 100:F2}%";

        // Measure parts
        var prefixSize = e.Graphics.MeasureString(prefix, regFont);
        var middleSize = e.Graphics.MeasureString(middle, boldFont);
        var suffixSize = e.Graphics.MeasureString(suffix, regFont);

        float totalWidth = prefixSize.Width + middleSize.Width + suffixSize.Width;
        float startX = (Width - totalWidth) / 2; // center horizontally
        float y = (Height - Math.Max(prefixSize.Height, middleSize.Height)) / 2; // center vertically

        // Draw each part
        e.Graphics.DrawString(prefix, regFont, brush, startX, y);
        startX += prefixSize.Width;

        e.Graphics.DrawString(middle, boldFont, brush, startX, y);
        startX += middleSize.Width;

        e.Graphics.DrawString(suffix, regFont, brush, startX, y);
    }
}
