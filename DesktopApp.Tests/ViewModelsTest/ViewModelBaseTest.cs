using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopApp.Tests.ViewModelsTest
{
    [TestClass]
    public class ViewModelBaseTest
    {
        [TestMethod]
        public void Title()
        {

            var viewModel = new ViewModels.ViewModelBase();

            Assert.IsNotNull(viewModel.Title.Value);

        }

        [TestMethod]
        public void Dispose()
        {

            var viewModel = new ViewModels.ViewModelBase();

            viewModel.Dispose();

        }
    }
}
