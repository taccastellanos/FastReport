

namespace FastReport.ReportBuilder
{
    public class ReportTitleDefinition
    {
        public string Text { get; set; }
        public Font Font { get; set; } = new SkiaSharp.SKFont("Times New Roman", 14, SkiaSharp.SKFontStyle.Bold | SkiaSharp.SKFontStyle.Regular);
        public bool Visible { get; set; }
        public SkiaSharp.SKColor TextColor { get; set; } = SkiaSharp.SKColors.Black;
        public SkiaSharp.SKColor FillColor { get; set; }
        public VertAlign? VertAlign { get; set; }
        public HorzAlign? HorzAlign { get; set; }
    }
}
