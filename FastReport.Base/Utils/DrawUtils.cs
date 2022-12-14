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
        private static SkiaSharp.SKFont FDefaultFont;
        private static SkiaSharp.SKFont FDefault96Font;
        private static SkiaSharp.SKFont FDefaultReportFont;
        private static SkiaSharp.SKFont FDefaultTextObjectFont;
        private static SkiaSharp.SKFont FFixedFont;
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
            return 1;
        }

        public static SkiaSharp.SKFont DefaultFont
        {
            get
            {
                if (FDefaultFont == null)
                {
                    
                    switch (System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "ja":
                            FDefaultFont = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName( "MS UI Gothic"), 9);
                            break;

                        case "zh":
                            FDefaultFont = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("SimSun"), 9);
                            break;

                        default:
                            FDefaultFont = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Tahoma"), 8);
                            break;
                    }
                }
                return FDefaultFont;
            }
        }

        public static SkiaSharp.SKFont DefaultReportFont
        {
            get
            {
                if (FDefaultReportFont == null)
                {
                    
                    switch (System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "ja":
                            FDefaultReportFont = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("MS UI Gothic"), 9);
                            break;

                        case "zh":
                            FDefaultReportFont = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("SimSun"), 9);
                            break;

                        default:
                            FDefaultReportFont = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Arial"), 10);
                            break;
                    }
                }
                return FDefaultReportFont;
            }
        }

        public static SkiaSharp.SKFont DefaultTextObjectFont
        {
            get
            {
                if (FDefaultTextObjectFont == null)
                    FDefaultTextObjectFont = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Arial"), 10);
                return FDefaultTextObjectFont;
            }
        }

        public static SkiaSharp.SKFont FixedFont
        {
            get
            {
                
                if (FFixedFont == null)
                    FFixedFont = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Courier New"), 10);
                
                return FFixedFont;
            }
        }

        public static SkiaSharp.SKFont Default96Font
        {
            get
            {
                if (FDefault96Font == null)
                {
                    float sz = 96f / ScreenDpi;
                    switch (System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "ja":
                            FDefault96Font = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("MS UI Gothic"), 9 * sz);
                            break;

                        case "zh":
                            FDefault96Font = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("SimSun"), 9 * sz);
                            break;

                        default:
                            FDefault96Font = new SkiaSharp.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Tahoma"), 8 * sz);
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

        public static SkiaSharp.SKSize MeasureString(string text, SkiaSharp.SKFont font)
        {
            /*TODO
            using (SkiaSharp.SKBitmap bmp = new SkiaSharp.SKBitmap(1, 1))
            
            using (StringFormat sf = new StringFormat())
            {
                SkiaSharp.SKGraphics g = Graphics.FromImage(bmp);
                return MeasureString(g, text, font, sf);
            }*/
            return new SkiaSharp.SKSize(0,0);
        }

        public static SkiaSharp.SKSize MeasureString(SkiaSharp.SKDrawable g, string text, SkiaSharp.SKFont font, SkiaSharp.SKTextAlign format)
        {
            return MeasureString(g, text, font, new SkiaSharp.SKRect(0, 0, 10000, 10000), format);
        }

        public static SkiaSharp.SKSize MeasureString(SkiaSharp.SKDrawable g, string text, SkiaSharp.SKFont font, SkiaSharp.SKRect layoutRect, SkiaSharp.SKTextAlign format)
        {
            
            if (String.IsNullOrEmpty(text))
                return new SkiaSharp.SKSize(0, 0);
            /*TODO
            CharacterRange[] characterRanges = { new CharacterRange(0, text.Length) };
            StringFormatFlags saveFlags = format.FormatFlags;
            format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            format.SetMeasurableCharacterRanges(characterRanges);
            Region[] regions = g.MeasureCharacterRanges(text, font, layoutRect, format);
            format.FormatFlags = saveFlags;
            SkiaSharp.SKRect rect = regions[0].GetBounds(g);
            regions[0].Dispose();
            return rect.Size;*/
            return new SkiaSharp.SKSize(0,0);
            
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

        internal static MonoRendering GetMonoRendering(SkiaSharp.SKDrawable printerGraphics)
        {
            if (FMonoRendering == MonoRendering.Undefined)
            {
                /*TODO
                GraphicsUnit savedUnit = printerGraphics.PageUnit;
                printerGraphics.PageUnit = GraphicsUnit.Point;

                const string s = "test string test string test string test string";
                float f1 = printerGraphics.MeasureString(s, DefaultReportFont).Width;
                FMonoRendering = f1 > 200 ? MonoRendering.Pango : MonoRendering.Cairo;

                printerGraphics.PageUnit = savedUnit;*/
            }
            return FMonoRendering;
        }

        
    }
}