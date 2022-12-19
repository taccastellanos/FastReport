using System;

namespace FastReport
{
    public class SKFont : SkiaSharp.SKFont
    {
        private FontStyle style = FontStyle.Regular;
        public SKFont() : base()
        {
            
        }
        public SKFont(SkiaSharp.SKTypeface typeface, float size = 12, float scaleX = 1, float skewX = 0):base(typeface, size, scaleX, skewX)
        {
            if(typeface.IsBold)
            {
                style |= FontStyle.Bold;
            }
            if(typeface.IsItalic)
            {
                style |= FontStyle.Italic;
            }
        }
 
        public FontStyle Style{
            get
            { 
                return style;
            }
            set
            {
                style = value;
                
                if(style!= value)
                {
                    //alinhamento com o SKTypeFace
                    var s = SkiaSharp.SKFontStyle.Normal;

                    if((style & FontStyle.Bold)>0 && (style & FontStyle.Italic)>0)
                        s = SkiaSharp.SKFontStyle.BoldItalic;
                    else if((style & FontStyle.Bold)>0 && (style & ~FontStyle.Italic)>0)
                        s = SkiaSharp.SKFontStyle.Bold;
                    else if((style & ~FontStyle.Bold)>0 && (style & FontStyle.Italic)>0)
                        s = SkiaSharp.SKFontStyle.Italic;

                    Typeface = SkiaSharp.SKTypeface.FromFamilyName(Typeface.FamilyName, s);
                }
            }
        } 
    }
}