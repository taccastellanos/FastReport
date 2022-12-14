using System;


using System.ComponentModel;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Specifies a kind of the shape.
  /// </summary>
  public enum ShapeKind 
  { 
    /// <summary>
    /// Specifies a rectangle shape.
    /// </summary>
    Rectangle,

    /// <summary>
    /// Specifies a round rectangle shape.
    /// </summary>
    RoundRectangle,

    /// <summary>
    /// Specifies an ellipse shape.
    /// </summary>
    Ellipse,

    /// <summary>
    /// Specifies a triangle shape.
    /// </summary>
    Triangle,

    /// <summary>
    /// Specifies a diamond shape.
    /// </summary>
    Diamond
  }

  /// <summary>
  /// Represents a shape object.
  /// </summary>
  /// <remarks>
  /// Use the <see cref="ShapeKind"/> property to specify a shape. To set the width, style and color of the
  /// shape's border, use the <b>Border.Width</b>, <b>Border.Style</b> and <b>Border.Color</b> properties.
  /// </remarks>
  public partial class ShapeObject : ReportComponentBase
  {
    #region Fields
    private ShapeKind shape;
    private float curve;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a shape kind.
    /// </summary>
    [DefaultValue(ShapeKind.Rectangle)]
    [Category("Appearance")]
    public ShapeKind Shape
    {
      get { return shape; }
      set { shape = value; }
    }

    /// <summary>
    /// Gets or sets a shape curvature if <see cref="ShapeKind"/> is <b>RoundRectangle</b>.
    /// </summary>
    /// <remarks>
    /// 0 value means automatic curvature.
    /// </remarks>
    [DefaultValue(0f)]
    [Category("Appearance")]
    public float Curve
    {
      get { return curve; }
      set { curve = value; }
    }

    #endregion

    #region Private Methods
#if MONO
    private SkiaSharp.SKPath GetRoundRectPath(float x, float y, float x1, float y1, float radius)
    {
      SkiaSharp.SKPath gp = new GraphicsPath();
      if (radius < 1)
        radius = 1;
      gp.AddLine(x + radius, y, x1 - radius, y);
      gp.AddArc(x1 - radius - 1, y, radius + 1, radius + 1, 270, 90);
      gp.AddLine(x1, y + radius, x1, y1 - radius);
      gp.AddArc(x1 - radius - 1, y1 - radius - 1, radius + 1, radius + 1, 0, 90);
      gp.AddLine(x1 - radius, y1, x + radius, y1);
      gp.AddArc(x, y1 - radius - 1, radius + 1, radius + 1, 90, 90);
      gp.AddLine(x, y1 - radius, x, y + radius);
      gp.AddArc(x, y, radius, radius, 180, 90);
      gp.CloseFigure();
      return gp;
    }
#else    
    private SkiaSharp.SKPath GetRoundRectPath(float x, float y, float x1, float y1, float radius)
    {
        SkiaSharp.SKPath gp = new SkiaSharp.SKPath();
        if (radius < 1)
            radius = 1;
            /*TODO
        gp.AddArc(x1 - radius - 1, y, radius + 1, radius + 1, 270, 90);
        gp.AddArc(x1 - radius - 1, y1 - radius - 1, radius + 1, radius + 1, 0, 90);
        gp.AddArc(x, y1 - radius - 1, radius + 1, radius + 1, 90, 90);
        gp.AddArc(x, y, radius, radius, 180, 90);
        gp.Close();*/
        return gp;
    }
#endif
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      ShapeObject src = source as ShapeObject;
      Shape = src.Shape;
      Curve = src.Curve;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      /*TODO
      if (Math.Abs(Width) < 1 || Math.Abs(Height) < 1)
        return;

      IGraphics g = e.Graphics;
      float x = (AbsLeft + Border.Width / 2) * e.ScaleX;
      float y = (AbsTop + Border.Width / 2) * e.ScaleY;
      float dx = (Width - Border.Width) * e.ScaleX - 1;
      float dy = (Height - Border.Width) * e.ScaleY - 1;
      float x1 = x + dx;
      float y1 = y + dy;

      /*Pen/SkiaSharp.SKPaint pen = e.Cache.GetPen(Border.Color, Border.Width * e.ScaleX, Border.DashStyle);
      /*Brush/SkiaSharp.SKPaint brush = null;
      if (Fill is SolidFill)
        /*Brush/SkiaSharp.SKPaint = e.Cache.GetBrush((Fill as SolidFill).Color);
      else
        /*Brush/SkiaSharp.SKPaint = Fill.CreateBrush(new SkiaSharp.SKRect(x, y, dx, dy), e.ScaleX, e.ScaleY);
        
      Report report = Report;
      if (report != null && report.SmoothGraphics && Shape != ShapeKind.Rectangle)
      {
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.SmoothingMode = SmoothingMode.AntiAlias;
      }  

      switch (Shape)
      {
        case ShapeKind.Rectangle:
          g.FillAndDrawRectangle(pen, brush, x, y, dx, dy);
          break;

        case ShapeKind.RoundRectangle:
          float min = Math.Min(dx, dy);
          if (curve == 0)
            min = min / 4;
          else
            min = Math.Min(min, curve * e.ScaleX * 10);
          SkiaSharp.SKPath gp = GetRoundRectPath(x, y, x1, y1, min);
          g.FillAndDrawPath(pen, brush, gp);
          gp.Dispose();
          break;

        case ShapeKind.Ellipse:
          g.FillAndDrawEllipse(pen, brush, x, y, dx, dy);
          break;

        case ShapeKind.Triangle:
          SkiaSharp.SKSkiaSharp.SKPoint[] triPoints = { 
            new SkiaSharp.SKPoint(x1, y1), new SkiaSharp.SKPoint(x, y1), new SkiaSharp.SKPoint(x + dx / 2, y), new SkiaSharp.SKPoint(x1, y1) };
          g.FillAndDrawPolygon(pen, brush, triPoints);
          break;

        case ShapeKind.Diamond:
          SkiaSharp.SKSkiaSharp.SKPoint[] diaPoints = { 
            new SkiaSharp.SKPoint(x + dx / 2, y), new SkiaSharp.SKPoint(x1, y + dy / 2), new SkiaSharp.SKPoint(x + dx / 2, y1),
            new SkiaSharp.SKPoint(x, y + dy / 2) };
          g.FillAndDrawPolygon(pen, brush, diaPoints);
          break;
      }

      if (!(Fill is SolidFill))
        brush.Dispose();
      if (report != null && report.SmoothGraphics)
      {
        g.InterpolationMode = InterpolationMode.Default;
        g.SmoothingMode = SmoothingMode.Default;
      }  */
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      Border.SimpleBorder = true;
      base.Serialize(writer);
      ShapeObject c = writer.DiffObject as ShapeObject;
      
      if (Shape != c.Shape)
        writer.WriteValue("Shape", Shape);
      if (Curve != c.Curve)
        writer.WriteFloat("Curve", Curve);  
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapeObject"/> class with default settings.
    /// </summary>
    public ShapeObject()
    {
      shape = ShapeKind.Rectangle;
      FlagSimpleBorder = true;
    }
  }
}