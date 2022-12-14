using System;
using System.Collections.Generic;
using System.Text;


namespace FastReport.Utils
{
    /// <summary>
    /// Provides a data for paint event.
    /// </summary>
    public class FRPaintEventArgs
    {
        private SkiaSharp.SKDrawable graphics;
        private float scaleX;
        private float scaleY;
        private GraphicCache cache;

        /// <summary>
        /// Gets a <b>Graphics</b> object to draw on.
        /// </summary>
        public SkiaSharp.SKDrawable Graphics
        {
            get { return graphics; }
        }

        /// <summary>
        /// Gets the X scale factor.
        /// </summary>
        public float ScaleX
        {
            get { return scaleX; }
        }

        /// <summary>
        /// Gets the Y scale factor.
        /// </summary>
        public float ScaleY
        {
            get { return scaleY; }
        }

        /// <summary>
        /// Gets the cache that contains graphics objects.
        /// </summary>
        public GraphicCache Cache
        {
            get { return cache; }
        }

        /// <summary>
        /// Initializes a new instance of the <b>FRPaintEventArgs</b> class with specified settings.
        /// </summary>
        /// <param name="g"><b>IGraphicsRenderer</b> object to draw on.</param>
        /// <param name="scaleX">X scale factor.</param>
        /// <param name="scaleY">Y scale factor.</param>
        /// <param name="cache">Cache that contains graphics objects.</param>
        public FRPaintEventArgs(SkiaSharp.SKDrawable g, float scaleX, float scaleY, GraphicCache cache)
        {
            graphics = g;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.cache = cache;
        }

       
    }

}
