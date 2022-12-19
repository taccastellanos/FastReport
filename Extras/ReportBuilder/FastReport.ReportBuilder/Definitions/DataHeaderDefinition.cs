

namespace FastReport.ReportBuilder
{
    public class DataHeaderDefinition
    {
        public Font Font { get; set; } = new FastReport.SKFont("Times New Roman", 10.0f, FontStyle.Regular);
        public bool Visible { get; set; } = true;
        public SkiaSharp.SKColor TextColor { get; set; } = SkiaSharp.SKColors.Black;
        public SkiaSharp.SKColor FillColor { get; set; } = Color.FromArgb(235, 243, 251);
    }
}
