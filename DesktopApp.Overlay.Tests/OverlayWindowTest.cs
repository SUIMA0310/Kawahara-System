using System;

using DesktopApp.Overlay.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopApp.Overlay.Test
{
    [TestClass]
    public class OverlayWindowTest
    {
        [TestMethod]
        public void Create()
        {
            new OverlayWindow();
        }

        [TestMethod]
        public void AltF4Cancel()
        {
            var window = new OverlayWindow();

            window.AltF4Cancel = true;
            window.AltF4Cancel = false;
            window.AltF4Cancel = true;
            window.AltF4Cancel = false;

            window.Show();
            System.Threading.Tasks.Task.Delay( TimeSpan.FromSeconds( 2 ) ).Wait();

            window.AltF4Cancel = true;
            window.AltF4Cancel = false;
            window.AltF4Cancel = true;
            window.AltF4Cancel = false;

            window.Close();
        }

        [TestMethod]
        public void ClickThrough()
        {
            var window = new OverlayWindow();

            window.ClickThrough = true;
            window.ClickThrough = false;
            window.ClickThrough = true;
            window.ClickThrough = false;

            window.Show();
            System.Threading.Tasks.Task.Delay( TimeSpan.FromSeconds( 2 ) ).Wait();

            window.ClickThrough = true;
            window.ClickThrough = false;
            window.ClickThrough = true;
            window.ClickThrough = false;

            window.Close();
        }

        [TestMethod]
        public void Show()
        {
            var window = new OverlayWindow();

            window.Show();

            System.Threading.Tasks.Task.Delay( TimeSpan.FromSeconds( 3 ) ).Wait();

            window.Close();
        }
    }
}