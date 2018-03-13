using System;

using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace DesktopApp.Overlay.Draw.Objects
{
    public abstract class ObjectBase : IDisposable
    {
        public RenderTarget RenderTarget { get; set; }
        public Brush Brush { get; set; }

        protected PathGeometry Geometry { get; private set; }

        public ObjectBase( RenderTarget renderTarget )
        {
            this.RenderTarget = renderTarget ?? throw new NullReferenceException( nameof( renderTarget ) );
            this.Brush = this.CreateBrush( this.RenderTarget ) ?? throw new NullReferenceException( nameof( CreateBrush ) );
            this.Geometry = new PathGeometry( this.RenderTarget.Factory );
            using ( var sink = this.Geometry.Open() ) {
                this.SetSink( sink );
                sink.Close();
            }
        }

        public virtual void Render( RawMatrix3x2 transform, RawColor4 color )
        {
            this.SetColor( color );

            this.RenderTarget.Transform = transform;
            this.RenderTarget.FillGeometry( this.Geometry, this.Brush );

            this.RenderTarget.Transform = Helpers.Matrix3x2Helper.Identity;
        }

        protected abstract void SetSink( GeometrySink sink );

        protected virtual Brush CreateBrush( RenderTarget renderTarget )
        {
            return new SolidColorBrush( renderTarget, new RawColor4() );
        }

        private void SetColor( RawColor4 color )
        {
            if ( this.Brush is SolidColorBrush brush ) {
                brush.Color = color;
            }
        }

        public virtual void Dispose()
        {
            this.RenderTarget = null;

            this.Brush.Dispose();
            this.Brush = null;

            this.Geometry.Dispose();
            this.Geometry = null;
        }
    }
}