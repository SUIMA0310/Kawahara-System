using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopApp.Overlay.Draw.Helpers;
using DesktopApp.Overlay.Draw.Models;

namespace DesktopApp.Tests.HelpersTest
{
    [TestClass]
    public class ParameterCurveHelpersTest
    {
        public class TestParameterCurve1 : IParameterCurve
        {
            private static TestParameterCurve1 _Instance;
            public static IParameterCurve Instance => _Instance ?? (_Instance = new TestParameterCurve1());
            public float GetValue( float t ) { throw new NotImplementedException(); }
        }

        public class TestParameterCurve2 { }

        public class TestParameterCurve3 : IParameterCurve
        {
            public float GetValue( float t ) { throw new NotImplementedException(); }
        }

        public class TestParameterCurve4 : IParameterCurve
        {
            public static IParameterCurve Instance => null;
            public float GetValue( float t ) { throw new NotImplementedException(); }
        }

        [TestMethod]
        public void GetParameterCurveInstance_string()
        {

            Assert.AreEqual( TestParameterCurve1.Instance, typeof( TestParameterCurve1 ).FullName.GetParameterCurveInstance() );

        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void GetParameterCurveInstance_string_thrown()
        {

            "fakeClassName".GetParameterCurveInstance();

        }

        [TestMethod]
        public void GetParameterCurveInstance_string_thrownCancel()
        {

            Assert.IsNull( "fakeClassName".GetParameterCurveInstance(false));

        }

        [TestMethod]
        public void GetParameterCurveInstance_Type()
        {

            Assert.AreEqual( TestParameterCurve1.Instance, typeof( TestParameterCurve1 ).GetParameterCurveInstance() );

        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void GetParameterCurveInstance_Type_thrown()
        {

            typeof( TestParameterCurve2 ).GetParameterCurveInstance();

        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void GetParameterCurveInstance_Type_thrown2()
        {

            typeof( TestParameterCurve3 ).GetParameterCurveInstance();

        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void GetParameterCurveInstance_Type_thrown3()
        {

            typeof( TestParameterCurve4 ).GetParameterCurveInstance();

        }

        [TestMethod]
        public void GetParameterCurveInstance_Type_thrownCancel()
        {

            Assert.IsNull( typeof( TestParameterCurve4 ).GetParameterCurveInstance( false ) );

        }
    }
}
