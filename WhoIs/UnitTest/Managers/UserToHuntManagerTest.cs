using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhoIs;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using Unity;

namespace UnitTest.Managers
{
    [TestClass]
    public class UserToHuntManagerTest {

        IUserToHuntManager _userToHuntManager;

        public UserToHuntManagerTest()
        {
             DependencyContainer.InitializeCore();
            _userToHuntManager = DependencyContainer.Container.Resolve<IUserToHuntManager>();
        }

        [TestMethod]
        public async Task UserToHuntManager_Hunt_Sucess()
        {
            int countHuntedBeforeHunt = _userToHuntManager.GetCountUsersHunted();

            UserToHunt usersToHunt = new UserToHunt();
            await _userToHuntManager.HuntUser(usersToHunt);


            Assert.AreEqual(_userToHuntManager.GetCountUsersHunted(), countHuntedBeforeHunt + 1);
        }     

    }
}
