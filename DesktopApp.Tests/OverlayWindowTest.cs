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

            window.SourceInitialized += ( s, e ) =>
            {
                window.AltF4Cancel = true;
                window.AltF4Cancel = false;
                window.AltF4Cancel = true;
                window.AltF4Cancel = false;

                window.Close();
            };

            window.Show();

        }

        [TestMethod]
        public void ClickThrough()
        {
            var window = new OverlayWindow();

            window.ClickThrough = true;
            window.ClickThrough = false;
            window.ClickThrough = true;
            window.ClickThrough = false;

            window.SourceInitialized += ( s, e ) =>
            {
                window.ClickThrough = true;
                window.ClickThrough = false;
                window.ClickThrough = true;
                window.ClickThrough = false;

                window.Close();
            };

            window.Show();
        }

        [TestMethod]
        public void Show()
        {
            var window = new OverlayWindow();

            window.SourceInitialized += ( s, e ) =>
            {
                window.Close();
            };

            window.Show();
        }
    }
}