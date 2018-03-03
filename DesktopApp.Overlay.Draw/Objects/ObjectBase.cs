using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace DesktopApp.Overlay.Draw.Objects
{
    public abstract class ObjectBase : IDisposable
    {

        public RenderTarget RenderTarget { get; set; }
        public Brush Brush { get; set; }

        protected PathGeometry Geometry { get; private set; }

        public ObjectBase(RenderTarget renderTarget)
        {
            this.RenderTarget = renderTarget ?? throw new NullReferenceException(nameof(renderTarget));
            this.Brush = this.CreateBrush(this.RenderTarget) ?? throw new NullReferenceException(nameof(CreateBrush));
            this.Geometry = new PathGeometry(this.RenderTarget.Factory);
            using (var sink = this.Geometry.Open()) {

                this.SetSink(sink);
                sink.Close();

            }
        }

        public virtual void Render(RawPoint point, RawColor4 color)
        {

            this.SetColor(color);

            this.RenderTarget.Transform = this.GetTranslation(point);
            this.RenderTarget.FillGeometry(this.Geometry, this.Brush);

            this.RenderTarget.Transform = Helpers.Matrix3x2Helper.Identity;

        }

        protected abstract void SetSink(GeometrySink sink);

        protected virtual Brush CreateBrush(RenderTarget renderTarget)
        {
            return new SolidColorBrush(renderTarget, new RawColor4());
        }

        protected virtual RawMatrix3x2 GetTranslation(RawPoint point)
        {
            return Helpers.Matrix3x2Helper.Translation(point);
        }

        private void SetColor(RawColor4 color)
        {

            if (this.Brush is SolidColorBrush brush) {

                brush.Color = color;

            }

        }

        public void Dispose()
        {
            this.RenderTarget = null;

            this.Brush.Dispose();
            this.Brush = null;

            this.Geometry.Dispose();
            this.Geometry = null;
        }

    }
}
