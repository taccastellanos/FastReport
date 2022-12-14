using FastReport.Utils;
using System;
using System.Collections;



namespace FastReport
{
    /// <summary>
    /// Represents a cache of graphics objects such as pens, brushes, fonts and text formats.
    /// </summary>
    /// <remarks>
    /// Cache holds all used graphics objects. There is no need to dispose objects returned
    /// by GetXXX calls.
    /// </remarks>
    /// <example>This example demonstrates how to use graphic cache.
    /// <code>
    /// public void Draw(FRPaintEventArgs e)
    /// {
    ///   /*Brush*/SkiaSharp.SKPaint brush = e.Cache.GetBrush(BackColor);
    ///   /*Pen*/SkiaSharp.SKPaint pen = e.Cache.GetPen(BorderColor, 1, BorderStyle);
    ///   e.Graphics.FillRectangle(brush, Bounds);
    ///   e.Graphics.DrawRectangle(pen, Bounds);
    /// }
    /// </code>
    /// </example>
    public class GraphicCache : IDisposable
    {
        private Hashtable pens;
        private Hashtable brushes;
        private Hashtable fonts;
        private Hashtable stringFormats;

        /// <summary>
        /// Gets a pen with specified settings.
        /// </summary>
        /// <param name="color">Color of a pen.</param>
        /// <param name="width">Width of a pen.</param>
        /// <param name="style">Dash style of a pen.</param>
        /// <returns>The <b>Pen</b> object.</returns>
        public /*Pen*/SkiaSharp.SKPaint GetPen(SkiaSharp.SKColor color, float width, DashStyle style)
        {
            return  new SkiaSharp.SKPaint();//TODOGetPen(color, width, style, LineJoin.Miter);
        }

        /// <summary>
        /// Gets a pen with specified settings.
        /// </summary>
        /// <param name="color">Color of a pen.</param>
        /// <param name="width">Width of a pen.</param>
        /// <param name="style">Dash style of a pen.</param>
        /// <param name="lineJoin">Line join of a pen.</param>
        /// <returns>The <b>Pen</b> object.</returns>
        public /*Pen*/SkiaSharp.SKPaint GetPen(SkiaSharp.SKColor color, float width, DashStyle style, LineJoin lineJoin)
        {
            int hash = color.GetHashCode() ^ width.GetHashCode() ^ style.GetHashCode() ^ lineJoin.GetHashCode();
            /*TODO
            /*Pen/SkiaSharp.SKPaint result = pens[hash] as /*Pen/SkiaSharp.SKPaint;
            if (result == null)
            {
                result = new /*Pen/SkiaSharp.SKPaint (color, width);
                result.DashStyle = style;
                result.LineJoin = lineJoin;
                pens[hash] = result;
            }
            return result;
            */
            return new SkiaSharp.SKPaint();
        }

        /// <summary>
        /// Gets a /*Brush*/SkiaSharp.SKPaint with specified color.
        /// </summary>
        /// <param name="color">Color of a brush.</param>
        /// <returns>The <b>SolidBrush</b> object.</returns>
        public /*SolidBrush*/SkiaSharp.SKPaint GetBrush(SkiaSharp.SKColor color)
        {
            int hash = color.GetHashCode();
            
            /*SolidBrush*/SkiaSharp.SKPaint result = brushes[hash] as /*SolidBrush*/SkiaSharp.SKPaint;
            /*TODO
            if (result == null)
            {
                result = new /*SolidBrush/SkiaSharp.SKPaint(color);
                brushes[hash] = result;
            }*/
            return result;
        }

        /// <summary>
        /// Gets a font with specified settings.
        /// </summary>
        /// <param name="name">Family of a font.</param>
        /// <param name="size">Size of a font.</param>
        /// <param name="style">Style of a font.</param>
        /// <returns>The <b>Font</b> object.</returns>
        public SkiaSharp.SKFont GetFont(SkiaSharp.SKTypeface name, float size, SkiaSharp.SKFontStyle style)
        {
            int hash = name.GetHashCode() ^ size.GetHashCode() ^ style.GetHashCode();
            var result = fonts[hash] as SkiaSharp.SKFont;
            if (result == null)
            {
                result = new SkiaSharp.SKFont( name, size);
                fonts[hash] = result;
            }
            return result;
        }

        /// <summary>
        /// Gets a string format with specified settings.
        /// </summary>
        /// <param name="align">Text alignment information on the vertical plane.</param>
        /// <param name="lineAlign">Line alignment on the horizontal plane.</param>
        /// <param name="trimming"><b>StringTrimming</b> enumeration.</param>
        /// <param name="flags"><b>StringFormatFlags</b> enumeration that contains formatting information.</param>
        /// <param name="firstTab">The number of spaces between the beginning of a line of text and the first tab stop.</param>
        /// <param name="tabWidth">Distance between tab stops.</param>
        /// <returns>The <b>StringFormat</b> object.</returns>
        public StringAlignment GetStringFormat(StringAlignment align, StringAlignment lineAlign,
          StringTrimming trimming, StringFormatFlags flags, float firstTab, float tabWidth)
        {
            /*TODO
            int hash = align.GetHashCode() ^ (lineAlign.GetHashCode() << 2) ^ (trimming.GetHashCode() << 5) ^
              (flags.GetHashCode() << 16) ^ (100 - firstTab).GetHashCode() ^ tabWidth.GetHashCode();
            StringAlignment result = stringFormats[hash] as StringFormat;
            if (result == null)
            {
                result = new StringFormat();
                result.Alignment = align;
                result.LineAlignment = lineAlign;
                result.Trimming = trimming;
                result.FormatFlags = flags;
                float[] tabStops = new float[64];
                // fixed issue 2823
                tabStops[0] = firstTab;
                for (int i = 1; i < 64; i++)
                {
                    tabStops[i] = tabWidth;
                }
                result.SetTabStops(0, tabStops);
                stringFormats[hash] = result;
            }
            return result;*/
            return StringAlignment.Center;
        }

        /// <summary>
        /// Gets a string format with specified settings.
        /// </summary>
        /// <param name="align">Text alignment information on the vertical plane.</param>
        /// <param name="lineAlign">Line alignment on the horizontal plane.</param>
        /// <param name="trimming"><b>StringTrimming</b> enumeration.</param>
        /// <param name="flags"><b>StringFormatFlags</b> enumeration that contains formatting information.</param>
        /// <param name="firstTab">The number of spaces between the beginning of a line of text and the first tab stop.</param>
        /// <param name="tabWidth">Distance between tab stops.</param>
        /// <param name="defaultTab">Default distance between default tabs stops.</param>
        /// <returns>The <b>StringFormat</b> object.</returns>
        public StringAlignment GetStringFormat(StringAlignment align, StringAlignment lineAlign,
          StringTrimming trimming, StringFormatFlags flags, float firstTab, FloatCollection tabWidth,
          float defaultTab = 48)
        {
            /*TODO
            int hash = align.GetHashCode() ^ (lineAlign.GetHashCode() << 2) ^ (trimming.GetHashCode() << 5) ^
              (flags.GetHashCode() << 16) ^ (100 - firstTab).GetHashCode() ^ tabWidth.GetHashCode();
            SkiaSharp.SKTextAlign result = stringFormats[hash] as StringFormat;
            if (result == null)
            {
                result = new  SkiaSharp.SKTextAlign();
                result.Alignment = align;
                result.LineAlignment = lineAlign;
                result.Trimming = trimming;
                result.FormatFlags = flags;
                float[] tabStops = new float[64];
                // fixed issue 2823
                tabStops[0] = firstTab;
                for (int i = 1; i < 64; i++)
                {
                    if (i > tabWidth.Count)
                    {
                        tabStops[i] = defaultTab;
                        continue;
                    }
                    tabStops[i] = tabWidth[i - 1];
                }
                result.SetTabStops(0, tabStops);
                stringFormats[hash] = result;
            }
            return result;*/
            return StringAlignment.Center;
        }

        /// <summary>
        /// Disposes resources used by this object.
        /// </summary>
        public void Dispose()
        {
            foreach  (/*Pen*/SkiaSharp.SKPaint pen in pens.Values)
            {
                pen.Dispose();
            }
            foreach (var brush in brushes.Values)
            {
                //TODObrush.Dispose();
            }
            foreach (SkiaSharp.SKFont font in fonts.Values)
            {
                font.Dispose();
            }
            /*TODO
            foreach (StringFormat format in stringFormats.Values)
            {
                format.Dispose();
            }*/
            pens.Clear();
            brushes.Clear();
            fonts.Clear();
            stringFormats.Clear();
        }

        /// <summary>
        /// Initializes a new instance of the <b>GraphicCache</b> class with default settings.
        /// </summary>
        public GraphicCache()
        {
            pens = new Hashtable();
            brushes = new Hashtable();
            fonts = new Hashtable();
            stringFormats = new Hashtable();
        }

    }
}
