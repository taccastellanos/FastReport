using System;

namespace FastReport
{
    public class SKFont : FastReport.SKFont
    {

        public SKFont():base()
        {
            
        }
        public SKFont(SkiaSharp.SKTypeface typeface, float size = 12, float scaleX = 1, float skewX = 0):base(typeface, size, scaleX, skewX)
        {
            
        }
        public FontStyle Style{get;set;} 
    }
}