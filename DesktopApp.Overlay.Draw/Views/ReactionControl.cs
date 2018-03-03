using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2dControl;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using DesktopApp.Overlay.Draw.Helpers;

namespace DesktopApp.Overlay.Draw.Views
{
    public class ReactionControl : D2dControl.D2dControl
    {

        public ReactionControl()
        {

            this.resCache.Add("Good", target => new Objects.GoodObject(target));

        }

        public override void Render(RenderTarget target)
        {
            target.Clear(null);

            target.Transform = Matrix3x2Helper.Identity;
            target.AntialiasMode = AntialiasMode.PerPrimitive;


            var good = this.resCache["Good"] as Objects.ObjectBase;

            good.Render(new RawPoint(150, 650), new RawColor4(1.0f, 0.25f, 0.25f, 0.5f));

        }

    }
}
