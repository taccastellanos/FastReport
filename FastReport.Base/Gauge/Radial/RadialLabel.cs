using FastReport.Utils;

using System.ComponentModel;

namespace FastReport.Gauge.Radial
{
#if !DEBUG
    [DesignTimeVisible(false)]
#endif
    class RadialLabel : GaugeLabel
    {
        public RadialLabel(GaugeObject parent): base(parent)
        {
            Parent = parent as RadialGauge;
        }

        public override void Assign(GaugeLabel src)
        {
            base.Assign(src);
        }

        public override void Serialize(FRWriter writer, string prefix, GaugeLabel diff)
        {
            base.Serialize(writer, prefix, diff);
        }

        public override void Draw(FRPaintEventArgs e)
        {
            /*TODO
            if ((Parent as RadialGauge).Type == RadialGaugeType.Circle)
            {
                base.Draw(e);
                float x = (Parent.AbsLeft + Parent.Border.Width / 2) * e.ScaleX;
                float y = (Parent.AbsTop + Parent.Border.Width / 2) * e.ScaleY;
                float dx = (Parent.Width - Parent.Border.Width) * e.ScaleX - 1;
                float dy = (Parent.Height - Parent.Border.Width) * e.ScaleY - 1;

                SkiaSharp.SKPoint lblPt = new SkiaSharp.SKPoint(x + dx / 2, y + dy - ((Parent.Scale as RadialScale).AvrTick.Y - y));
                SkiaSharp.SKSize txtSize = RadialUtils.GetStringSize(e, Parent, Font, Text);
                SkiaSharp.SKFont font = RadialUtils.GetFont(e, Parent, Font);
                /*Brush/SkiaSharp.SKPaint brush = e.Cache.GetBrush(Color);
                e.Graphics.DrawString(Text, font, brush, lblPt.X - txtSize.Width / 2, lblPt.Y - txtSize.Height / 2);
            }*/
        }
    }
}
