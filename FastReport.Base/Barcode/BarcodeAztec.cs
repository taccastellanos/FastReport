using System;
using System.Collections;
using System.Collections.Generic;

using System.ComponentModel;
using FastReport.Barcode.Aztec;
using FastReport.Utils;

namespace FastReport.Barcode
{
    /// <summary>
    /// Generates the 2D Aztec barcode.
    /// </summary>
    public class BarcodeAztec : Barcode2DBase
    {
        BitMatrix matrix;
        int errorCorrectionPercent;
        const int PIXEL_SIZE = 4;

        /// <summary>
        /// Gets or sets the error correction percent.
        /// </summary>
        [DefaultValue(33)]
        public int ErrorCorrectionPercent
        {
            get { return errorCorrectionPercent; }
            set { errorCorrectionPercent = (value < 5) ? 5 : ((value > 95) ? 95 : value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeAztec"/> class with default settings.
        /// </summary>
        public BarcodeAztec()
        {
            ErrorCorrectionPercent = 33;
        }

        internal override void Initialize(string text, bool showText, int angle, float zoom)
        {
            base.Initialize(text, showText, angle, zoom);

            matrix = Encoder.encode(System.Text.Encoding.ASCII.GetBytes(text), ErrorCorrectionPercent, 0).Matrix;
        }

        internal override SkiaSharp.SKSize CalcBounds()
        {
            int textAdd = showText ? 18 : 0;
            return new  SkiaSharp.SKSize(matrix.Width * PIXEL_SIZE, matrix.Height * PIXEL_SIZE + textAdd);
        }

        internal override void Draw2DBarcode(SkiaSharp.SKDrawable g, float kx, float ky)
        {
            /* TODO
            /*Brush/SkiaSharp.SKPaint light = Brushes.White;
            /*Brush/SkiaSharp.SKPaint dark = new SolidBrush(Color);     
            
            for (int y = 0; y < matrix.Height; y++)
            {
                for (int x = 0; x < matrix.Width; x++)
                {
                    bool b = matrix.getRow(y, null)[x];
                    
                    /*Brush/SkiaSharp.SKPaint brush = /*b == true ?/ dark /*: light/;
                    if (b == true)
                        g.FillRectangle(brush, x * PIXEL_SIZE * kx, y * PIXEL_SIZE * ky,
                                               PIXEL_SIZE * kx,     PIXEL_SIZE * ky);
                                            
                }
            }
            dark.Dispose();*/
        }

        /// <inheritdoc/>
        public override void Assign(BarcodeBase source)
        {
            base.Assign(source);
            BarcodeAztec src = source as BarcodeAztec;

            ErrorCorrectionPercent = src.ErrorCorrectionPercent;
        }

        internal override void Serialize(FastReport.Utils.FRWriter writer, string prefix, BarcodeBase diff)
        {
            base.Serialize(writer, prefix, diff);
            BarcodeAztec c = diff as BarcodeAztec;

            if (c == null || ErrorCorrectionPercent != c.ErrorCorrectionPercent)
                writer.WriteInt(prefix + "ErrorCorrection", ErrorCorrectionPercent);
        }
    }
}
