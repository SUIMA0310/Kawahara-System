using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace DesktopApp.Overlay.Draw.Objects
{
    public class GoodObject : ObjectBase
    {

        public GoodObject(RenderTarget renderTarget) : base(renderTarget) { }

        protected override void SetSink(GeometrySink sink)
        {

            sink.SetFillMode( FillMode.Winding );

            sink.BeginFigure( new RawVector2(0, 100), FigureBegin.Filled );
            sink.AddBeziers( new[] {
                new BezierSegment { Point3 = new RawVector2( 100,  -40), Point1 = new RawVector2(   0,  100), Point2 = new RawVector2( 100,   20) },
                new BezierSegment { Point3 = new RawVector2(  50, -100), Point1 = new RawVector2( 100,  -75), Point2 = new RawVector2(  75, -100) },
                new BezierSegment { Point3 = new RawVector2(   0,  -50), Point1 = new RawVector2(  25, -100), Point2 = new RawVector2(   0,  -75) },
                new BezierSegment { Point3 = new RawVector2( -50, -100), Point1 = new RawVector2(   0,  -75), Point2 = new RawVector2( -25, -100) },
                new BezierSegment { Point3 = new RawVector2(-100,  -40), Point1 = new RawVector2( -75, -100), Point2 = new RawVector2(-100,  -75) },
                new BezierSegment { Point3 = new RawVector2(   0,  100), Point1 = new RawVector2(-100,   20), Point2 = new RawVector2(   0,  100) }
            });
            sink.EndFigure( FigureEnd.Closed );

        }

    }
}
