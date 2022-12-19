using System;
using System.Collections.Generic;
using System.Text;


using FastReport.Utils;

namespace FastReport.Barcode
{
  /// <summary>
  /// The base class for 2D-barcodes such as PDF417 and Datamatrix.
  /// </summary>
  public abstract class Barcode2DBase : BarcodeBase
  {
    private static FastReport.SKFont FFont = new FastReport.SKFont(SkiaSharp.SKTypeface.FromFamilyName("Arial"), 8);

        private void DrawBarcode(SkiaSharp.SKDrawable g, float width, float height)
        {
            SkiaSharp.SKSize originalSize = CalcBounds();
            float kx = width / originalSize.Width;
            float ky = height / originalSize.Height;

            Draw2DBarcode(g, kx, ky);

            //If swiss qr, draw the swiss cross
            if (text.StartsWith("SPC"))
            {
                float top = showText ? height - 21 : height;
                /* TODO
                g.FillRectangle(Brushes.White, width / 2 - width / 100f * 7, top / 2 - top / 100 * 7, width / 100f * 14, top / 100 * 14);
                g.FillRectangle(Brushes.Black, width / 2 - width / 100f * 6, top / 2 - top / 100 * 6, width / 100f * 12, top / 100 * 12);
                g.FillRectangle(Brushes.White, width / 2 - width / 100f * 4, top / 2 - top / 100 * 1.5f, width / 100f * 8, top / 100 * 3);
                g.FillRectangle(Brushes.White, width / 2 - width / 100f * 1.5f, top / 2 - top / 100 * 4, width / 100f * 3, top / 100 * 8);
                */
            }
            if (text.StartsWith("ST"))
            {
              /* TODO
                /*Pen/SkiaSharp.SKPaint skyBluePen = new /*Pen/SkiaSharp.SKPaint (Brushes.Black);
                skyBluePen.Width = (kx * 4 * zoom) / 2;

                g.DrawLine(skyBluePen, width - 2, height / 2, width - 2, height - 2);
                g.DrawLine(skyBluePen, width / 2, height - 2, width - 2, height - 2);*/
            }
            // draw the text.
            if (showText)
            {
                string data = StripControlCodes(text);
                /* TODO
                if (data.Length > 0)
                {
                    // When we print, .Net automatically scales the font. However, we need to handle this process.
                    // Downscale the font to the screen resolution, then scale by required value (ky).
                    float fontZoom = 18f / (int)g.MeasureString(data, FFont).Height * ky;
                    using (var drawFont = new FastReport.SKFont(FFont.Typeface, FFont.Size * fontZoom))
                    {
                        g.DrawString(data, drawFont, Brushes.Black, new SkiaSharp.SKRect(0, height - 18 * ky, width, 18 * ky));
                    }
                }*/
            }
        }
    
    internal virtual void Draw2DBarcode(SkiaSharp.SKDrawable g, float kx, float ky)
    {
    }
    
    public override void DrawBarcode(SkiaSharp.SKDrawable g, SkiaSharp.SKRect displayRect)
    {
      float width = angle == 90 || angle == 270 ? displayRect.Height : displayRect.Width;
      float height = angle == 90 || angle == 270 ? displayRect.Width : displayRect.Height;
           /* TODO
            IGraphicsState state = g.Save();
            
            try
            {
              // rotate
              g.TranslateTransform(displayRect.Left, displayRect.Top);
              g.RotateTransform(angle);

              switch (angle)
              {
                case 90:
                  g.TranslateTransform(0, -displayRect.Width);
                  break;

                case 180:
                  g.TranslateTransform(-displayRect.Width, -displayRect.Height);
                  break;

                case 270:
                  g.TranslateTransform(-displayRect.Height, 0);
                  break;
              }

              DrawBarcode(g, width, height);
            }
            finally
            {
              g.Restore(state);
            }*/
    }
  }
}
