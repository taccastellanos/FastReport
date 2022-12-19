using System;


namespace FastReport.Utils
{
    internal enum MonoRendering 
    { 
        Undefined, 
        Pango, 
        Cairo 
    }

    public static partial class DrawUtils
    {
        private static FastReport.SKFont FDefaultFont;
        private static FastReport.SKFont FDefault96Font;
        private static FastReport.SKFont FDefaultReportFont;
        private static FastReport.SKFont FDefaultTextObjectFont;
        private static FastReport.SKFont FFixedFont;
        private static int FScreenDpi;
        private static float FDpiFX;
        private static MonoRendering FMonoRendering = MonoRendering.Undefined;

        public static int ScreenDpi
        {
            get
            {
                if (FScreenDpi == 0)
                    FScreenDpi = GetDpi();
                return FScreenDpi;
            }
        }

        public static float ScreenDpiFX
        {
            get
            {
                if (FDpiFX == 0)
                    FDpiFX = 96f / DrawUtils.ScreenDpi;
                return FDpiFX;
            }
        }

        private static int GetDpi()
        {
            /*TODO
            using (SkiaSharp.SKBitmap bmp = new SkiaSharp.SKBitmap(1, 1))
            
            using (var g = SkiaSharp.SKGraphics.FromImage(bmp))
            {
                return (int)g.DpiX;
            }*/
            return 72;
        }

        public static FastReport.SKFont DefaultFont
        {
            get
            {
                if (FDefaultFont == null)
                {
                    
                    switch (System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "ja":
                            FDefaultFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName( "MS UI Gothic"), 9);
                            break;

                        case "zh":
                            FDefaultFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("SimSun"), 9);
                            break;

                        default:
                            FDefaultFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Tahoma"), 8);
                            break;
                    }
                }
                return FDefaultFont;
            }
        }

        public static FastReport.SKFont DefaultReportFont
        {
            get
            {
                
                if (FDefaultReportFont == null)
                {
                    switch (System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "ja":
                            FDefaultReportFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("MS UI Gothic"), 9);
                            break;

                        case "zh":
                            FDefaultReportFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("SimSun"), 9);
                            break;

                        default:
                            FDefaultReportFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Arial"), 10);
                            
                            break;
                    }
                }
                return FDefaultReportFont;
            }
        }

        public static FastReport.SKFont DefaultTextObjectFont
        {
            get
            {
                if (FDefaultTextObjectFont == null)
                    FDefaultTextObjectFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Arial"), 10);
                return FDefaultTextObjectFont;
            }
        }

        public static FastReport.SKFont FixedFont
        {
            get
            {
                
                if (FFixedFont == null)
                    FFixedFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Courier New"), 10);
                
                return FFixedFont;
            }
        }

        public static FastReport.SKFont Default96Font
        {
            get
            {
                if (FDefault96Font == null)
                {
                    float sz = 96f / ScreenDpi;
                    switch (System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "ja":
                            FDefault96Font = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("MS UI Gothic"), 9 * sz);
                            break;

                        case "zh":
                            FDefault96Font = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("SimSun"), 9 * sz);
                            break;

                        default:
                            FDefault96Font = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Tahoma"), 8 * sz);
                            break;
                    }
                }
                return FDefault96Font;
            }
        }

        public static SkiaSharp.SKSize MeasureString(string text)
        {
            return MeasureString(text, DefaultFont);
        }

        public static SkiaSharp.SKSize MeasureString(string text, FastReport.SKFont font)
        {
            
            var outlinePaint = new SkiaSharp.SKPaint();
            var sf = new StringFormat();
            return MeasureString(outlinePaint, text, font,  sf);
                    
        }

        public static SkiaSharp.SKSize MeasureString(SkiaSharp.SKPaint p, string text, FastReport.SKFont font, StringFormat format)
        {
            return MeasureString(p, text, font, new SkiaSharp.SKRect(0, 0, 10000, 10000), format);
        }

        public static SkiaSharp.SKSize MeasureString(SkiaSharp.SKPaint p, string text, FastReport.SKFont font, SkiaSharp.SKRect layoutRect, StringFormat format)
        {
            
            if (String.IsNullOrEmpty(text))
                return new SkiaSharp.SKSize(0, 0);

            var textPaint = new SkiaSharp.SKPaint(font);
            textPaint.Style = p.Style;
            textPaint.StrokeWidth = p.StrokeWidth;
            textPaint.PathEffect = p.PathEffect;

            var textBounds = new SkiaSharp.SKRect();
            
            textPaint.MeasureText(text, ref textBounds);
            
            return textBounds.Size;
            
            
        }
        
        public static float[] GetDashStyle(DashStyle ds)
        {   
            switch(ds)
            {
                case DashStyle.Dot:
                    return new float[]{10,10};
                    
                case DashStyle.Dash:
                    return new float[]{30,10};
                    
                case DashStyle.DashDot:
                    return new float[]{30,10,10,10};

                case DashStyle.DashDotDot:     
                    return new float[]{30,10,10,10,10,10};
            }
            return new float[]{};
        }
        public static void FloodFill(SkiaSharp.SKBitmap bmp, int x, int y, SkiaSharp.SKColor color, SkiaSharp.SKColor replacementColor)
        {
            if (x < 0 || y < 0 || x >= bmp.Width || y >= bmp.Height || bmp.GetPixel(x, y) != color)
                return;
            bmp.SetPixel(x, y, replacementColor);
            FloodFill(bmp, x - 1, y, color, replacementColor);
            FloodFill(bmp, x + 1, y, color, replacementColor);
            FloodFill(bmp, x, y - 1, color, replacementColor);
            FloodFill(bmp, x, y + 1, color, replacementColor);
        }

        internal static MonoRendering GetMonoRendering(SkiaSharp.SKCanvas printerGraphics)
        {
            if (FMonoRendering == MonoRendering.Undefined)
            {
                
                SkiaSharp.SKPaint p = new SkiaSharp.SKPaint();

                const string s = "test string test string test string test string";
                float f1 = p.MeasureText(s);
                FMonoRendering = f1 > 200 ? MonoRendering.Pango : MonoRendering.Cairo;
            }
            return FMonoRendering;
        }

        
    }
}