using System.ComponentModel;


using FastReport.Utils;

namespace FastReport
{
    /// <summary>
    /// Specifies a line cap style.
    /// </summary>
    public enum CapStyle
    {
        /// <summary>
        /// Specifies a line without a cap.
        /// </summary>
        None,

        /// <summary>
        /// Specifies a line with a circle cap.
        /// </summary>
        Circle,

        /// <summary>
        /// Specifies a line with a square cap.
        /// </summary>
        Square,

        /// <summary>
        /// Specifies a line with a diamond cap.
        /// </summary>
        Diamond,


        /// <summary>
        /// Specifies a line with an arrow cap.
        /// </summary>
        Arrow
    }

    /// <summary>
    /// Specifies a start and end line caps.
    /// </summary>
    [TypeConverter(typeof(FastReport.TypeConverters.FRExpandableObjectConverter))]
    public class CapSettings
    {
        private float width;
        private float height;
        private CapStyle style;

        /// <summary>
        /// Gets or sets a width of the cap.
        /// </summary>
        [DefaultValue(8f)]
        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets or sets a height of the cap.
        /// </summary>
        [DefaultValue(8f)]
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Gets or sets a cap style.
        /// </summary>
        [DefaultValue(CapStyle.None)]
        public CapStyle Style
        {
            get { return style; }
            set { style = value; }
        }

        internal void GetCustomCapPath(out SkiaSharp.SKPath path, out float inset)
        {
            path = new SkiaSharp.SKPath();
            inset = 0;
           
            switch (Style)
            {
                case CapStyle.Arrow:
                    path.MoveTo( new SkiaSharp.SKPoint(-Width, -Height));
                    path.LineTo( new SkiaSharp.SKPoint(Width, -Height));
                    break;

                case CapStyle.Circle:
                    path.AddCircle(-Width / 2, -Height / 2, Width);
                    inset = Height / 2;
                    break;

                case CapStyle.Square:
                    path.AddRect(new SkiaSharp.SKRect(-Width / 2, -Height / 2, Width, Height));
                    inset = Height / 2;
                    break;

                case CapStyle.Diamond:
                    path.MoveTo(new SkiaSharp.SKPoint(0, -Height / 1.4f));
                    path.LineTo(new SkiaSharp.SKPoint(-Width / 1.4f, 0));
                    path.LineTo(new SkiaSharp.SKPoint(0, Height / 1.4f));
                    path.LineTo(new SkiaSharp.SKPoint(Width / 1.4f, 0));
                    inset = Height / 1.4f;
                    break;
            }
        }

        /// <summary>
        /// Assigns values from another source.
        /// </summary>
        /// <param name="source">Source to assign from.</param>
        public void Assign(CapSettings source)
        {
            Width = source.Width;
            Height = source.Height;
            Style = source.Style;
        }

        /// <summary>
        /// Creates exact copy of this object.
        /// </summary>
        /// <returns>Copy of this object.</returns>
        public CapSettings Clone()
        {
            CapSettings result = new CapSettings();
            result.Assign(this);
            return result;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            CapSettings c = obj as CapSettings;
            return c != null && Width == c.Width && Height == c.Height && Style == c.Style;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Serializes the cap settings.
        /// </summary>
        /// <param name="prefix">Name of the cap property.</param>
        /// <param name="writer">Writer object.</param>
        /// <param name="diff">Another cap to compare with.</param>
        /// <remarks>
        /// This method is for internal use only.
        /// </remarks>
        public void Serialize(string prefix, FRWriter writer, CapSettings diff)
        {
            if (Width != diff.Width)
                writer.WriteFloat(prefix + ".Width", Width);
            if (Height != diff.Height)
                writer.WriteFloat(prefix + ".Height", Height);
            if (Style != diff.Style)
                writer.WriteValue(prefix + ".Style", Style);
        }

        /// <summary>
        /// Initializes a new instance of the <b>CapSettings</b> class with default settings. 
        /// </summary>
        public CapSettings()
        {
            width = 8;
            height = 8;
        }
    }
}
