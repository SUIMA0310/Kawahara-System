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

                obj.Show();

            } catch (InvalidOperationException) { }

            var ret = obj.SetWindowFactory( factory.Object );
            Assert.IsTrue(ret);
            Assert.AreEqual(factory.Object, privateObj.GetProperty("WindowFactory"));

            obj.Show();
            Assert.AreEqual(eWindowStateTypes.Initializeing, obj.WindowState);

            try {

                obj.Show();

            } catch (InvalidOperationException) { }

            var ret2 = obj.SetWindowController(controller.Object);
            Assert.IsTrue(ret2);
            Assert.AreEqual(controller.Object, privateObj.GetProperty("WindowController"));

            obj.Show();

            var ret3 = obj.SetWindowController(null);
            Assert.IsTrue(ret3);
            Assert.AreEqual(null, privateObj.GetProperty("WindowController"));


            obj.Dispose();

            var disposable = privateObj.GetProperty("Disposable") as System.Reactive.Disposables.CompositeDisposable;
            var d1 = privateObj.GetField("ObservableWindowInitialized");
            var d2 = privateObj.GetField("ObservableWindowClosed");
            var d3 = privateObj.GetField("ObservableHiddenChanged");

            Assert.AreEqual(0, disposable.Count);
            Assert.IsNull(d1);
            Assert.IsNull(d2);
            Assert.IsNull(d3);

        }

        [TestMethod]
        public void Hide()
        {
            var factory = new Mock<IWindowFactory>();
            var controller = new Mock<IWindowController>();
            controller.SetupProperty(x => x.IsHidden);                      

            var obj = new WindowService();
            var privateObj = new PrivateObject(obj);

            obj.Hide();

            obj.SetWindowFactory(factory.Object);
            obj.Show();

            controller.Raise( x => x.WindowInitialized += null );

            try {
                obj.Hide();
            } catch (InvalidOperationException) { }

            obj.SetWindowController( controller.Object );
            obj.Hide();

            Assert.IsTrue( controller.Object.IsHidden );
            

        }
        
        [TestMethod]
        public void Event()
        {

            var factory = new Mock<IWindowFactory>();
            var controller = new Mock<IWindowController>();

            var obj = new WindowService();
            var privateObj = new PrivateObject(obj);

            obj.SetWindowFactory( factory.Object );
            Assert.AreEqual(eWindowStateTypes.Closed, obj.WindowState);

            obj.Show();
            Assert.AreEqual(eWindowStateTypes.Initializeing, obj.WindowState);

            obj.SetWindowController(controller.Object);

            controller.Raise( x => x.WindowInitialized += null );
            Assert.AreEqual( eWindowStateTypes.Shown, obj.WindowState );

            controller.Raise(x => x.HiddenChanged += null, true);
            Assert.AreEqual(eWindowStateTypes.Hide, obj.WindowState);

            controller.Raise(x => x.HiddenChanged += null, false);
            Assert.AreEqual(eWindowStateTypes.Shown, obj.WindowState);

            controller.Raise(x => x.WindowClosed += null);
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
