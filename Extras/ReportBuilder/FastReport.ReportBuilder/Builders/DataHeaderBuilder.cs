

namespace FastReport.ReportBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataHeaderBuilder<T>
    {
        private readonly ReportBuilder<T> _report;

        /// <summary>
        /// Data header band
        /// </summary>
        /// <param name="report"></param>
        public DataHeaderBuilder(ReportBuilder<T> report)
        {
            _report = report;
        }

        /// <summary>
        /// Set report daha header font family name, size, style
        /// </summary>
        /// <param name="familyName"></param>
        /// <param name="emSize"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public DataHeaderBuilder<T> Font(string familyName, float emSize, SkiaSharp.SKFontStyle style)
        {
            _report._dataHeader.Font = new SkiaSharp.SKFont(familyName, emSize, style);
            return this;
        }


        /// <summary>
        /// Set report daha header font family name, size
        /// </summary>
        /// <param name="familyName"></param>
        /// <param name="emSize"></param>
        /// <returns></returns>
        public DataHeaderBuilder<T> Font(string familyName, float emSize)
        {
            return Font(familyName, emSize, SkiaSharp.SKFontStyle.Regular);
        }

        /// <summary>
        /// Set report daha header font family name
        /// </summary>
        /// <param name="familyName"></param>
        /// <returns></returns>
        public DataHeaderBuilder<T> Font(string familyName)
        {
            return Font(familyName, 10.0f, SkiaSharp.SKFontStyle.Regular);
        }

        /// <summary>
        /// Set data header visibility
        /// </summary>
        /// <param name="visible"></param>
        /// <returns></returns>
        public DataHeaderBuilder<T> Visible(bool visible)
        {
            _report._dataHeader.Visible = visible;
            return this;
        }

        /// <summary>
        /// Set data header text color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public DataHeaderBuilder<T> TextColor(SkiaSharp.SKColor color)
        {
            _report._dataHeader.TextColor = color;
            return this;
        }

        /// <summary>
        /// Set data header background color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public DataHeaderBuilder<T> FillColor(SkiaSharp.SKColor color)
        {
            _report._dataHeader.FillColor = color;
            return this;
        }
    }
}
