

namespace FastReport.ReportBuilder
{
    public class ReportDefinition
    {
        public Font Font { get; set; } = new FastReport.SKFont("Times New Roman", 10.0f, FontStyle.Regular);
        public VertAlign VertAlign { get; set; }
        public HorzAlign HorzAlign { get; set; }
    }
}
