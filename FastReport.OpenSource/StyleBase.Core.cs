using FastReport.Utils;


namespace FastReport
{
    partial class StyleBase
    {
        #region Private Methods

        private SkiaSharp.SKFont GetDefaultFontInternal()
        {
            return DrawUtils.DefaultFont;
        }

        #endregion Private Methods
    }
}