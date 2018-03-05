using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopApp.Tests.ModulesTest
{
    [TestClass]
    public class ModuleTests
    {

        [TestMethod]
        public void MainModule()
        {

            var container = new DryIoc.Container();
            var mock = new Mock<Prism.Regions.IRegionManager>();
            mock.Setup(m => m.RegisterViewWithRegion(It.IsNotNull<string>(), It.IsNotNull<Type>()));

            var module = new MainModule(container, mock.Object);

            module.Initialize();

            container.Dispose();

        }

        [TestMethod]
        public void CoreModule()
        {

            var container = new DryIoc.Container();

            var module = new CoreModule(container);

            module.Initialize();

            container.Dispose();

        }

        [TestMethod]
        public void OverlayModule()
        {

            var container = new DryIoc.Container();
            var mock = new Mock<Prism.Regions.IRegionManager>();
            mock.Setup(m => m.RegisterViewWithRegion(It.IsNotNull<string>(), It.IsNotNull<Type>()));

            var module = new OverlayModule(container, mock.Object);

            module.Initialize();

            container.Dispose();

        }

        [TestMethod]
        public void SignalRModule()
        {

            var container = new DryIoc.Container();
            var mock = new Mock<Prism.Regions.IRegionManager>();

            var module = new SignalRModule(container, mock.Object);

            module.Initialize();

            container.Dispose();

        }
    }
}
