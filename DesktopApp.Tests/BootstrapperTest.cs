using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopApp.Tests
{
    /// <summary>
    /// Bootstrapper
    /// </summary>
    [TestClass]
    public class BootstrapperTest
    {
        [TestMethod]
        public void ConfigureModuleCatalog()
        {
            var bootstrapper = new Bootstrapper();
            var privateObject = new PrivateObject(bootstrapper);
            var moduleCatalog = new Prism.Modularity.ModuleCatalog();

            privateObject.SetProperty( "ModuleCatalog", moduleCatalog );

            privateObject.Invoke( "ConfigureModuleCatalog" );
        }
    }
}