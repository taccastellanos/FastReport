using System;
using System.Collections.Generic;
using System.Text;


namespace FastReport.Matrix
{
  internal class MatrixStyleSheet : StyleSheet
  {
    public SkiaSharp.SKBitmap GetStyleBitmap(int index)
    {
      StyleCollection styleCollection = this[index];
      Style style = styleCollection[styleCollection.IndexOf("Header")];
      
      var headerColor = SkiaSharp.SKColors.White;
      if (style.Fill is SolidFill)
        headerColor = (style.Fill as SolidFill).Color;
      else if (style.Fill is LinearGradientFill)
        headerColor = (style.Fill as LinearGradientFill).StartColor;

      style = styleCollection[styleCollection.IndexOf("Body")];
      var bodyColor = SkiaSharp.SKColors.White;
      if (style.Fill is SolidFill)
        bodyColor = (style.Fill as SolidFill).Color;
      else if (style.Fill is LinearGradientFill)
        bodyColor = (style.Fill as LinearGradientFill).StartColor;
        
      // draw style picture
      var result = new SkiaSharp.SKBitmap(16, 16);
      /*TODO
      using (Graphics g = Graphics.FromImage(result))
      {
        g.FillRectangle(Brushes.White, 0, 0, 16, 16);
        
        using (Brush b = new SolidBrush(headerColor))
        {
          g.FillRectangle(b, 0, 0, 15, 8);
        }
        using (Brush b = new SolidBrush(bodyColor))
        {
          g.FillRectangle(b, 0, 8, 15, 8);
        }
        
        g.DrawRectangle(Pens.Silver, 0, 0, 14, 14);
      }*/
      
      return result;
    }
  }
}
