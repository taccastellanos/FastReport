using FastReport.Utils;


namespace FastReport
{
    partial class BandBase
    {
        /// <inheritdoc/>
        public override void Draw(FRPaintEventArgs e)
        {
            DrawBackground(e);
            Border.Draw(e, new SkiaSharp.SKRect(AbsLeft, AbsTop, Width, Height));
        }
    }
}
