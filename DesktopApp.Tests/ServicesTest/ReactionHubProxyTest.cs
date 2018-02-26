using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopApp.Tests.ServicesTest
{
    [TestClass]
    public class ReactionHubProxyTest
    {
        [TestMethod]
        [Ignore]
        public void Connnection()
        {

            var logger = new Prism.Logging.TextLogger();
            var connection = new Services.ConnectionService(logger);
            var reactionHubProxy = new Services.ReactionHubProxy( logger, connection );

            reactionHubProxy.Open();

            connection.Dispose();

        }
    }
}
