using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Overlay.Draw.Models
{
    public struct Bezier
    {

        public RawVector2 StartPoint;
        public RawVector2 EndPoint;

        public RawVector2 Point1;
        public RawVector2 Point2;

        public Bezier(RawVector2 startPoint, RawVector2 endPoint) : this(startPoint, endPoint, startPoint, endPoint) { }
        public Bezier(RawVector2 startPoint, RawVector2 endPoint, RawVector2 point1, RawVector2 point2)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.Point1 = point1;
            this.Point2 = point2;
        }
    }
}
