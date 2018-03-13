using DesktopApp.Overlay.Draw.Helpers;

namespace DesktopApp.Overlay.Draw.Models
{
    public class Constant : IParameterCurve
    {
        private static IParameterCurve _Instance;

        public static IParameterCurve Instance
            => _Instance ?? (_Instance = new Constant());

        private Constant()
        {
        }

        public float GetValue( float t )
            => t.CutOut();

        public override string ToString()
            => "等速";
    }

    public class Quadratic : IParameterCurve
    {
        private static IParameterCurve _Instance;

        public static IParameterCurve Instance
            => _Instance ?? (_Instance = new Quadratic());

        private Quadratic()
        {
        }

        public float GetValue( float t )
            => (t * t).CutOut();

        public override string ToString()
            => "2乗";
    }
}