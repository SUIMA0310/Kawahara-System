using System;
using DesktopApp.MultiUsers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopApp.Tests.ModelsTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void User_Name()
        {

            IUser user = new User();

            Assert.AreEqual( null, user.Name );

            user.Name = "AAA";
            Assert.AreEqual( "AAA", user.Name );

            user.Name = "BBB";
            Assert.AreEqual( "BBB", user.Name );

            user.Name = null;
            Assert.AreEqual( null, user.Name );

        }
    }
}
