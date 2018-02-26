using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopApp.Tests.ModulesTest
{
    [TestClass]
    public class SinnalRModuleTest
    {
        [TestMethod]
        public void Initialize()
        {

            var container = new DryIoc.Container();

            var module = new SignalRModule( container );

            module.Initialize();

            container.Dispose();

        }
    }
}
