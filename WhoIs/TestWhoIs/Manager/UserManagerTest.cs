using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unity;
using WhoIs;
using WhoIs.Managers.Interface;
using WhoIs.Models;

namespace TestWhoIs.Manager
{
    [TestClass]
    public class UserManagerTest
    {
        public UserManagerTest()
        {
            DependencyContainer.InitializeCore();
        }

        [TestMethod]
        public async Task GetUsers_NotDeleted()
        {
            
            IUserManager userManager = DependencyContainer.Container.Resolve<IUserManager>();
            List<User> users = await userManager.GetUsers();

            Assert.IsTrue(users.Count.Equals(3));
        }

    }
}
