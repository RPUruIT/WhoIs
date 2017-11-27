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
    public class AppUserManagerTest
    {
        public AppUserManagerTest()
        {
            DependencyContainer.InitializeCore();
        }

        [TestMethod]
        public async Task GetUsers_NotDeleted()
        {
            
            IAppUserManager appUserManager = DependencyContainer.Container.Resolve<IAppUserManager>();
            List<AppUser> users = await appUserManager.GetUsersFromService();

            Assert.IsTrue(users.Count.Equals(3));
        }

    }
}
