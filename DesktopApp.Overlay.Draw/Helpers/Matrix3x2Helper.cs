using SharpDX.Mathematics.Interop;

namespace DesktopApp.Overlay.Draw.Helpers
{
    public static class Matrix3x2Helper
    {
        public static RawMatrix3x2 Identity = new RawMatrix3x2(1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f);

        #region Translation

        public static RawMatrix3x2 Translation( RawVector2 point )
        {
            return new RawMatrix3x2( 1.0f, 0.0f, 0.0f, 1.0f, point.X, point.Y );
        }

        public static RawMatrix3x2 Translation( float x, float y )
        {
            return new RawMatrix3x2( 1.0f, 0.0f, 0.0f, 1.0f, x, y );
        }

        public static RawMatrix3x2 Translation( this RawMatrix3x2 baseMatrix, RawVector2 point )
        {
            baseMatrix.M31 = point.X;
            baseMatrix.M32 = point.Y;
            return baseMatrix;
        }

        public static RawMatrix3x2 Translation( this RawMatrix3x2 baseMatrix, float x, float y )
        {
            baseMatrix.M31 = x;
            baseMatrix.M32 = y;
            return baseMatrix;
        }

        #endregion Translation

        #region Scale

        public static RawMatrix3x2 Scale( float scale ) => Scale( scale, scale );

        public static RawMatrix3x2 Scale( float sx, float sy ) => Identity.Scale( sx, sy );

        public static RawMatrix3x2 Scale( this RawMatrix3x2 baseMatrix, float scale )
            => baseMatrix.Scale( scale, scale );

        public static RawMatrix3x2 Scale( this RawMatrix3x2 baseMatrix, float sx, float sy )
        {
            baseMatrix.M11 *= sx;
            baseMatrix.M22 *= sy;
            return baseMatrix;
        }

        #endregion Scale
    }
}