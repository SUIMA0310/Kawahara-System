using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace DesktopApp.Overlay.Draw.Objects
{
    public class FunObject : ObjectBase
    {
        public FunObject( RenderTarget renderTarget ) : base( renderTarget )
        {
        }

        protected override void SetSink( GeometrySink sink )
        {
            sink.SetFillMode( FillMode.Alternate );

            sink.BeginFigure( new RawVector2( 0, 100 ), FigureBegin.Filled );
            sink.AddArc( new ArcSegment {
                Point = new RawVector2( 0, -100 ),
                Size = new Size2F( 100, 100 ),
                RotationAngle = 0,
                SweepDirection = SweepDirection.Clockwise,
                ArcSize = ArcSize.Large
            } );
            sink.AddArc( new ArcSegment {
                Point = new RawVector2( 0, 100 ),
                Size = new Size2F( 100, 100 ),
                RotationAngle = 0,
                SweepDirection = SweepDirection.Clockwise,
                ArcSize = ArcSize.Large
            } );
            sink.EndFigure( FigureEnd.Closed );

            sink.BeginFigure( new RawVector2( 42, 0 ), FigureBegin.Filled );
            sink.AddArc( new ArcSegment {
                Point = new RawVector2( 42, -30 ),
                Size = new Size2F( 15, 15 ),
                RotationAngle = 0,
                SweepDirection = SweepDirection.Clockwise,
                ArcSize = ArcSize.Large
            } );
            sink.AddArc( new ArcSegment {
                Point = new RawVector2( 42, 0 ),
                Size = new Size2F( 15, 15 ),
                RotationAngle = 0,
                SweepDirection = SweepDirection.Clockwise,
                ArcSize = ArcSize.Large
            } );
            sink.EndFigure( FigureEnd.Closed );

            sink.BeginFigure( new RawVector2( -42, 0 ), FigureBegin.Filled );
            sink.AddArc( new ArcSegment {
                Point = new RawVector2( -42, -30 ),
                Size = new Size2F( 15, 15 ),
                RotationAngle = 0,
                SweepDirection = SweepDirection.Clockwise,
                ArcSize = ArcSize.Large
            } );
            sink.AddArc( new ArcSegment {
                Point = new RawVector2( -42, 0 ),
                Size = new Size2F( 15, 15 ),
                RotationAngle = 0,
                SweepDirection = SweepDirection.Clockwise,
                ArcSize = ArcSize.Large
            } );
            sink.EndFigure( FigureEnd.Closed );

            sink.BeginFigure( new RawVector2( -60, 35 ), FigureBegin.Filled );
            sink.AddArc( new ArcSegment {
                Point = new RawVector2( 60, 35 ),
                Size = new Size2F( 60, 40 ),
                RotationAngle = 0,
                SweepDirection = SweepDirection.CounterClockwise,
                ArcSize = ArcSize.Small
            } );
            sink.EndFigure( FigureEnd.Closed );
        }
    }
}