﻿using System.ComponentModel;


using FastReport.Utils;

namespace FastReport.Gauge.Linear
{
    /// <summary>
    /// Represents a linear pointer.
    /// </summary>
#if !DEBUG
    [DesignTimeVisible(false)]
#endif
    public class LinearPointer : GaugePointer
    {
        #region Fields

        private float left;
        private float top;
        private float height;
        private float width;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets o sets the height of gauge pointer.
        /// </summary>
        [Browsable(false)]
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Gets or sets the width of a pointer.
        /// </summary>
        [Browsable(false)]
        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearPointer"/>
        /// </summary>
        /// <param name="parent">The parent gauge object.</param>
        public LinearPointer(GaugeObject parent) : base(parent)
        {
            height = 4.0f;
            width = 8.0f;
        }

        #endregion // Constructors

        #region Private Methods

        private void DrawHorz(FRPaintEventArgs e)
        {
            
            var g = e.Graphics;
            /*Pen*/SkiaSharp.SKPaint pen = e.Cache.GetPen(BorderColor, BorderWidth * e.ScaleX, DashStyle.Solid);

            left = (float)(Parent.AbsLeft + 0.5f * Units.Centimeters + (Parent.Width - 1.0f * Units.Centimeters) * (Parent.Value - Parent.Minimum) / (Parent.Maximum - Parent.Minimum)) * e.ScaleX;
            top = (Parent.AbsTop + Parent.Height / 2) * e.ScaleY;
            height = Parent.Height * 0.4f * e.ScaleY;
            width = Parent.Width * 0.036f * e.ScaleX;

            float dx = width / 2;
            float dy = height * 0.3f;
            var r = new SkiaSharp.SKRect();
            r.Location = new SkiaSharp.SKPoint(left - dx, top);
            r.Size = new SkiaSharp.SKSize(width, height);
            /*Brush*/SkiaSharp.SKPaint brush = Fill.CreateBrush(r, e.ScaleX, e.ScaleY);
            SkiaSharp.SKPoint[] p = new SkiaSharp.SKPoint[]
            {
                new SkiaSharp.SKPoint(left, top),
                new SkiaSharp.SKPoint(left + dx, top + dy),
                new SkiaSharp.SKPoint(left + dx, top + height),
                new SkiaSharp.SKPoint(left - dx, top + height),
                new SkiaSharp.SKPoint(left - dx, top + dy)
            };

            if ((Parent as LinearGauge).Inverted)
            {
                p[1].Y = top - dy;
                p[2].Y = top - height;
                p[3].Y = top - height;
                p[4].Y = top - dy;
            }
        
            SkiaSharp.SKPath path = new SkiaSharp.SKPath();
            path.MoveTo(p[0]);
            path.LineTo(p[1]);
            path.LineTo(p[2]);
            path.LineTo(p[3]);
            path.LineTo(p[4]);
            path.LineTo(p[4].X, p[0].Y);

            g.DrawPath(path, pen);
            g.DrawPath(path, brush);
        }

        private void DrawVert(FRPaintEventArgs e)
        {
            
            var g = e.Graphics;
            /*Pen*/SkiaSharp.SKPaint pen = e.Cache.GetPen(BorderColor, BorderWidth * e.ScaleX, DashStyle.Solid);

            left = (Parent.AbsLeft + Parent.Width / 2) * e.ScaleX;
            top = (float)(Parent.AbsTop + Parent.Height - 0.5f * Units.Centimeters - (Parent.Height - 1.0f * Units.Centimeters) * (Parent.Value - Parent.Minimum) / (Parent.Maximum - Parent.Minimum)) * e.ScaleY;
            height = Parent.Height * 0.036f * e.ScaleY;
            width = Parent.Width * 0.4f * e.ScaleX;

            float dx = width * 0.3f;
            float dy = height / 2;
            var r = new SkiaSharp.SKRect();
            r.Location = new SkiaSharp.SKPoint(left, top - dy);
            r.Size = new SkiaSharp.SKSize(width, height);
            /*Brush*/SkiaSharp.SKPaint brush = Fill.CreateBrush(r, e.ScaleX, e.ScaleY);
            SkiaSharp.SKPoint[] p = new SkiaSharp.SKPoint[]
            {
                new SkiaSharp.SKPoint(left, top),
                new SkiaSharp.SKPoint(left + dx, top - dy),
                new SkiaSharp.SKPoint(left + width, top - dy),
                new SkiaSharp.SKPoint(left + width, top + dy),
                new SkiaSharp.SKPoint(left + dx, top + dy)
            };

            if ((Parent as LinearGauge).Inverted)
            {
                p[1].X = left - dx;
                p[2].X = left - width;
                p[3].X = left - width;
                p[4].X = left - dx;
            }

            SkiaSharp.SKPath path = new SkiaSharp.SKPath();
            path.MoveTo(p[0]);
            path.LineTo(p[1]);
            path.LineTo(p[2]);
            path.LineTo(p[3]);
            path.LineTo(p[4]);            
            path.LineTo(p[4].X, p[0].Y);

            g.DrawPath(path, pen);
            g.DrawPath(path, brush);
            
        }

        #endregion // Private Methods

        #region Public Methods

        /// <inheritdoc/>
        public override void Assign(GaugePointer src)
        {
            base.Assign(src);

            LinearPointer s = src as LinearPointer;
            Height = s.Height;
            Width = s.Width;
        }

        /// <inheritdoc/>
        public override void Draw(FRPaintEventArgs e)
        {
            base.Draw(e);

            if (Parent.Vertical)
            {
                DrawVert(e);
            }
            else
            {
                DrawHorz(e);
            }
        }

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer, string prefix, GaugePointer diff)
        {
            base.Serialize(writer, prefix, diff);

            LinearPointer dc = diff as LinearPointer;
            if (Height != dc.Height)
            {
                writer.WriteFloat(prefix + ".Height", Height);
            }
            if (Width != dc.Width)
            {
                writer.WriteFloat(prefix + ".Width", Width);
            }
        }

        #endregion // Public Methods
    }
}
