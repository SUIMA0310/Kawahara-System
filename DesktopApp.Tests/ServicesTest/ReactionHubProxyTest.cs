using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopApp.Tests.ServicesTest
{
    [TestClass]
    public class ReactionHubProxyTest
    {
        [TestMethod]
        public void Connnection()
        {

            string URL = "http://www.hogehoge.com/";
            var logger = new Prism.Logging.TextLogger();
            var connection = new Services.ConnectionService(logger);
            var reactionHubProxy = new Services.ReactionHubProxy(logger, connection);

            int StartCount = 0;
            int StopCount = 0;

            using (ShimsContext.Create()) {

                var mockHubProxy = new Moq.Mock<Microsoft.AspNet.SignalR.Client.IHubProxy>();

                Microsoft.AspNet.SignalR.Client.Fakes.ShimHubConnection.ConstructorString = (ins, url) =>
                {
                    Assert.AreEqual(URL, url);
                };

                Microsoft.AspNet.SignalR.Client.Fakes.ShimHubConnection.AllInstances.CreateHubProxyString = (ins, hubName) =>
                {
                    Assert.IsNotNull(hubName);
                    return mockHubProxy.Object;
                };

                Microsoft.AspNet.SignalR.Client.Fakes.ShimConnection.AllInstances.Start = (ins) =>
                {
                    StartCount++;
                    return System.Threading.Tasks.Task.CompletedTask;
                };

                Microsoft.AspNet.SignalR.Client.Fakes.ShimConnection.AllInstances.Stop = (ins) =>
                {
                    StopCount++;
                };

                reactionHubProxy.ServerURL = URL;
                reactionHubProxy.Open();
                connection.Dispose();

            }

            Assert.AreEqual(1, StartCount);
            Assert.AreEqual(1, StopCount);

        }
    }
}
