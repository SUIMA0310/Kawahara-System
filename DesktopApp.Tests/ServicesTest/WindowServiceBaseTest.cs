using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DesktopApp.Services;

namespace DesktopApp.Tests.ServicesTest
{
    /// <summary>
    /// OverlayWindowServiceTest の概要の説明
    /// </summary>
    [TestClass]
    public class WindowServiceBaseTest
    {

        private class TestWindowService : WindowServiceBase<IWindowFactory, IWindowController>
        {

        }

        [TestMethod]
        public void Constructor_Doing_CannotThrowException()
        {

            var obj = new TestWindowService();

        }

        [TestMethod]
        public void Dispose_Doing_CannotThrowException()
        {

            var obj = new TestWindowService();
            var privateObj = new PrivateObject(obj);

            obj.Dispose();

            var disposable = privateObj.GetProperty("Disposable") as System.Reactive.Disposables.CompositeDisposable;

            Assert.AreEqual( 0, disposable.Count );

        }

        [TestMethod]
        public void SetWindowFactory_MopAs1stParam_CannotThrowException()
        {

            var mock = new Mock<IWindowFactory>();
            var mock2 = new Mock<IWindowFactory>();

            var obj = new TestWindowService();
            var privateObj = new PrivateObject(obj);

            obj.SetWindowFactory(mock.Object);
            Assert.AreEqual(mock.Object, privateObj.GetProperty("WindowFactory"));

            try {

                obj.SetWindowFactory(mock2.Object);

                Assert.Fail("propertiesの上書き禁止");

            } catch (InvalidOperationException) { }

            var ret = obj.SetWindowFactory(mock2.Object, false);
            Assert.AreEqual(false, ret);
            Assert.AreEqual(mock.Object, privateObj.GetProperty("WindowFactory"));

            obj.SetWindowFactory(null);
            Assert.AreEqual(null, privateObj.GetProperty("WindowFactory"));

            var ret2 = obj.SetWindowFactory(mock2.Object, true);
            Assert.AreEqual(true, ret2);
            Assert.AreEqual(mock2.Object, privateObj.GetProperty("WindowFactory"));

        }

        [TestMethod]
        public void SetWindowController_MopAs1stParam_CannotThrowException()
        {

            var mock = new Mock<IWindowController>();
            var mock2 = new Mock<IWindowController>();

            var obj = new TestWindowService();
            var privateObj = new PrivateObject(obj);

            obj.SetWindowController(mock.Object);
            Assert.AreEqual(mock.Object, privateObj.GetProperty("WindowController"));

            try {

                obj.SetWindowController(mock2.Object);

                Assert.Fail("propertiesの上書き禁止");

            } catch (InvalidOperationException) { }

            var ret = obj.SetWindowController(mock2.Object, false);
            Assert.AreEqual(false, ret);
            Assert.AreEqual(mock.Object, privateObj.GetProperty("WindowController"));

            obj.SetWindowController(null);
            Assert.AreEqual(null, privateObj.GetProperty("WindowController"));

            var ret2 = obj.SetWindowController(mock2.Object, true);
            Assert.AreEqual(true, ret2);
            Assert.AreEqual(mock2.Object, privateObj.GetProperty("WindowController"));

        }

    }
}
