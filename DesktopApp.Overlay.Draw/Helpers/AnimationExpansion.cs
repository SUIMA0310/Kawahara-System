using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Overlay.Draw.Helpers
{
    public static class AnimationExpansion
    {

        private static float BezierCurve(float x1, float x2, float x3, float x4, float t)
        {
            return (float)(Math.Pow(1 - t, 3) * x1 + 3 * Math.Pow(1 - t, 2) * t * x2 + 3 * (1 - t) * Math.Pow(t, 2) * x3 + Math.Pow(t, 3) * x4);
        }

        public static RawVector2 Point(this Models.Bezier bezier, float t)
        {
            return new RawVector2(
                BezierCurve(bezier.StartPoint.X,
                             bezier.Point1.X,
                             bezier.Point2.X,
                             bezier.EndPoint.X, t),
                BezierCurve(bezier.StartPoint.Y,
                             bezier.Point1.Y,
                             bezier.Point2.Y,
                             bezier.EndPoint.Y, t)
                );
        }

        public static float Elapsed(this DateTime time, TimeSpan span)
        {
            var elapsedTime = DateTime.Now - time;
            float per = elapsedTime.Ticks / (float)span.Ticks;
            return per.CutOut();
        }

        public static float CutOut(this float input)
        {
            return Math.Min( Math.Max( input, 0.0f ), 1.0f );
        }


    }
}
