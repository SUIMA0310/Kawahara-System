using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Mathematics.Interop;

namespace DesktopApp.Overlay.Draw.Helpers
{
    public static class Matrix3x2Helper
    {

        public static RawMatrix3x2 Identity = new RawMatrix3x2(1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f);

        public static RawMatrix3x2 Translation(RawPoint point)
        {
            return new RawMatrix3x2(1.0f, 0.0f, 0.0f, 1.0f, point.X, point.Y);
        }

        public static RawMatrix3x2 Translation(float x, float y)
        {
            return new RawMatrix3x2(1.0f, 0.0f, 0.0f, 1.0f, x, y);
        }

        public static RawMatrix3x2 Translation(this RawMatrix3x2 baseMatrix, RawPoint point)
        {
            baseMatrix.M31 = point.X;
            baseMatrix.M32 = point.Y;
            return baseMatrix;
        }

        public static RawMatrix3x2 Translation(this RawMatrix3x2 baseMatrix, float x, float y)
        {
            baseMatrix.M31 = x;
            baseMatrix.M32 = y;
            return baseMatrix;
        }

    }
}
