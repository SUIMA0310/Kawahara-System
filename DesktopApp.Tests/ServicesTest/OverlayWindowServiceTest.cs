using System;
using System.Text;
using System.Collections.Generic;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopApp.Services;

namespace DesktopApp.Tests.ServicesTest
{
    /// <summary>
    /// OverlayWindowServiceTest の概要の説明
    /// </summary>
    [TestClass]
    public class OverlayWindowServiceTest
    {

        [TestMethod]
        public void Constructor()
        {

            var obj = new WindowService();

        }

        [TestMethod]
        public void Show()
        {

            var factory = new Mock<IWindowFactory>();
            var controller = new Mock<IWindowController>();

            var obj = new WindowService();
            var privateObj = new PrivateObject(obj);

            try {

                //Factory 無し
                obj.Show();
                Assert.Fail();

            } catch (InvalidOperationException) { }

            //Factory セット
            Assert.IsTrue(obj.SetWindowFactory(factory.Object));
            Assert.AreEqual(factory.Object, privateObj.GetProperty("WindowFactory"));

            //Factory Use
            obj.Show();
            Assert.AreEqual(eWindowStateTypes.Initializeing, obj.WindowState);

            try {

                //Controller 無し
                obj.Show();
                Assert.Fail();

            } catch (InvalidOperationException) { }

            //Controller セット
            Assert.IsTrue(obj.SetWindowController(controller.Object));
            Assert.AreEqual(controller.Object, privateObj.GetProperty("WindowController"));

            try {

                //初期化未完
                obj.Show();
                Assert.Fail();

            } catch (InvalidOperationException) { }

            //初期化完了
            controller.Raise(x => x.Initialized += null, new EventArgs());
            Assert.AreEqual(eWindowStateTypes.Shown, obj.WindowState);

            obj.Show();

            //Controller 削除
            Assert.IsTrue(obj.SetWindowController(null));
            Assert.AreEqual(null, privateObj.GetProperty("WindowController"));


            obj.Dispose();

            var disposable = privateObj.GetProperty("Disposable") as System.Reactive.Disposables.CompositeDisposable;

            Assert.AreEqual(0, disposable.Count);

        }

        [TestMethod]
        public void Hide()
        {
            var factory = new Mock<IWindowFactory>();
            var controller = new Mock<IWindowController>();
            controller.SetupProperty(x => x.Opacity);

            var obj = new WindowService();
            var privateObj = new PrivateObject(obj);

            obj.Hide();

            obj.SetWindowFactory(factory.Object);
            obj.Show();

            controller.Raise(x => x.Initialized += null);

            try {
                obj.Hide();
                Assert.Fail();
            } catch (InvalidOperationException) { }

            obj.SetWindowController(controller.Object);

            try {
                obj.Hide();
                Assert.Fail();
            } catch (InvalidOperationException) { }

            controller.Raise(x=>x.Initialized += null, new EventArgs());

            obj.Hide();

            Assert.AreEqual(0.0, controller.Object.Opacity);


        }

        [TestMethod]
        public void Event()
        {

            var factory = new Mock<IWindowFactory>();
            var controller = new Mock<IWindowController>();

            var obj = new WindowService();
            var privateObj = new PrivateObject(obj);

            obj.SetWindowFactory(factory.Object);
            Assert.AreEqual(eWindowStateTypes.Closed, obj.WindowState);

            obj.Show();
            Assert.AreEqual(eWindowStateTypes.Initializeing, obj.WindowState);

            obj.SetWindowController(controller.Object);

            controller.Raise(x => x.Initialized += null, new EventArgs());
            Assert.AreEqual(eWindowStateTypes.Shown, obj.WindowState);

            controller.Raise(x => x.Closed += null, new EventArgs());
            Assert.AreEqual(eWindowStateTypes.Closed, obj.WindowState);

        }

        [TestMethod]
        public void Dispose()
        {

            var obj = new WindowService();
            var privateObj = new PrivateObject(obj);


            obj.Dispose();

            var disposable = privateObj.GetProperty("Disposable") as System.Reactive.Disposables.CompositeDisposable;

            Assert.AreEqual(0, disposable.Count);

        }

    }
}
