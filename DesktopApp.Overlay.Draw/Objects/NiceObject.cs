using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace DesktopApp.Overlay.Draw.Objects
{
    public class NiceObject : ObjectBase
    {
        public NiceObject( RenderTarget renderTarget ) : base( renderTarget )
        {
        }

        protected override void SetSink( GeometrySink sink )
        {
            sink.SetFillMode( FillMode.Winding );

            sink.BeginFigure( new RawVector2( -35, 85 ), FigureBegin.Filled );
            sink.AddLine( new RawVector2( 60, 85 ) );
            sink.AddBezier( new BezierSegment { Point3 = new RawVector2( 72, 58 ), Point1 = new RawVector2( 80, 85 ), Point2 = new RawVector2( 82, 58 ) } );
            sink.AddBezier( new BezierSegment { Point3 = new RawVector2( 85, 30 ), Point1 = new RawVector2( 100, 58 ), Point2 = new RawVector2( 95, 30 ) } );
            sink.AddBezier( new BezierSegment { Point3 = new RawVector2( 90, -5 ), Point1 = new RawVector2( 115, 30 ), Point2 = new RawVector2( 115, -5 ) } );
            sink.AddBezier( new BezierSegment { Point3 = new RawVector2( 85, -39 ), Point1 = new RawVector2( 110, -5 ), Point2 = new RawVector2( 105, -39 ) } );
            sink.AddLine( new RawVector2( 25, -39 ) );
            sink.AddBezier( new BezierSegment { Point3 = new RawVector2( -10, -98 ), Point1 = new RawVector2( 35, -105 ), Point2 = new RawVector2( 0, -105 ) } );
            sink.AddLine( new RawVector2( -10, -60 ) );
            sink.AddLine( new RawVector2( -35, -16 ) );
            sink.EndFigure( FigureEnd.Closed );

            sink.BeginFigure( new RawVector2( -90, 100 ), FigureBegin.Filled );
            sink.AddLine( new RawVector2( -48, 100 ) );
            sink.AddLine( new RawVector2( -48, -20 ) );
            sink.AddLine( new RawVector2( -90, -20 ) );
            sink.EndFigure( FigureEnd.Closed );
        }
    }
}