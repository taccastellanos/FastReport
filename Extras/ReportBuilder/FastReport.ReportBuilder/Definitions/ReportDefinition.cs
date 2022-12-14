

namespace FastReport.ReportBuilder
{
    public class ReportDefinition
    {
        public Font Font { get; set; } = new SkiaSharp.SKFont("Times New Roman", 10.0f, SkiaSharp.SKFontStyle.Regular);
        public VertAlign VertAlign { get; set; }
        public HorzAlign HorzAlign { get; set; }
    }
}
