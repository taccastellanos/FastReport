using FastReport.Utils;
using System;


namespace FastReport.Gauge.Radial
{
     internal class RadialUtils
    {
        public static SkiaSharp.SKFont GetFont(FRPaintEventArgs e, GaugeObject gauge, SkiaSharp.SKFont font)
        {
            return e.Cache.GetFont(font.Typeface, gauge.IsPrinting ? font.Size : font.Size * e.ScaleX * 96f / DrawUtils.ScreenDpi, font.Typeface.FontStyle);
        }
        public static SkiaSharp.SKSize GetStringSize(FRPaintEventArgs e, GaugeObject gauge, SkiaSharp.SKFont font, string text)
        {
            /*TODO
            return e.Graphics.MeasureString(text, GetFont(e, gauge, font));
            */
            return SkiaSharp.SKSize.Empty;
        }

        public static SkiaSharp.SKPoint[] RotateVector(SkiaSharp.SKPoint[] vector, double angle, SkiaSharp.SKPoint center)
        {
            SkiaSharp.SKPoint[] rotatedVector = new SkiaSharp.SKPoint[2];
            rotatedVector[0].X = (float)(center.X + (vector[0].X - center.X) * Math.Cos(angle) + (center.Y - vector[0].Y) * Math.Sin(angle));
            rotatedVector[0].Y = (float)(center.Y + (vector[0].X - center.X) * Math.Sin(angle) + (vector[0].Y - center.Y) * Math.Cos(angle));
            rotatedVector[1].X = (float)(center.X + (vector[1].X - center.X) * Math.Cos(angle) + (center.Y - vector[1].Y) * Math.Sin(angle));
            rotatedVector[1].Y = (float)(center.Y + (vector[1].X - center.X) * Math.Sin(angle) + (vector[1].Y - center.Y) * Math.Cos(angle));
            return rotatedVector;
        }

         public static bool IsTop(GaugeObject radialGauge)
        {
            return ((radialGauge as RadialGauge).Position & RadialGaugePosition.Top) != 0;
        }
         public static bool IsBottom(GaugeObject radialGauge)
        {
            return ((radialGauge as RadialGauge).Position & RadialGaugePosition.Bottom) != 0;
        }
         public static bool IsLeft(GaugeObject radialGauge)
        {
            return ((radialGauge as RadialGauge).Position & RadialGaugePosition.Left) != 0;
        }
         public static bool IsRight(GaugeObject radialGauge)
        {
            return ((radialGauge as RadialGauge).Position & RadialGaugePosition.Right) != 0;
        }
         public static bool IsSemicircle(GaugeObject radialGauge)
        {
            return ((radialGauge as RadialGauge).Type & RadialGaugeType.Semicircle) != 0;
        }
        public static bool IsQuadrant(GaugeObject radialGauge)
        {
            return ((radialGauge as RadialGauge).Type & RadialGaugeType.Quadrant) != 0;
        }
    }
}
