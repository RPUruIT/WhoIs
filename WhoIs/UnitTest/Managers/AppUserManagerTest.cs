using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Unity;
using WhoIs;
using WhoIs.Managers;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Repositories.Interface;

namespace UnitTest.Managers
{
    [TestClass]
    public class AppUserManagerTest
    {

        [TestMethod]
        public async Task AppUserManager_GetSpecificUsersFromService_NullResponse()
        {
            var userManagerMock = new Mock<IUserManager>();

            userManagerMock.Setup(x => x.GetUsersFromService()).ReturnsAsync((List<User>) null);

            AppUserManager appUserManager = new AppUserManager(null, userManagerMock.Object);

            List<AppUser> appUsers = await appUserManager.GetSpecificUsersFromService();

            Assert.IsNotNull(appUsers);
            Assert.IsTrue(appUsers.Count == 0);
        }


        [TestMethod]
        public async Task AppUserManager_GetLoggedAppUserExternalId_NotUserLogged()
        {
            var appUserManagerRepository = new Mock<IAppUserRepository>();

            AppUserManager appUserManager = new AppUserManager(appUserManagerRepository.Object, null);

            await appUserManager.LogoutFromApplication();

            Assert.AreEqual("", appUserManager.GetLoggedAppUserExternalId());
        }

        [TestMethod]
        public async Task AppUserManager_GetLoggedAppUserExternalId_UserLogged()
        {
            var appUserManagerRepository = new Mock<IAppUserRepository>();

            AppUserManager appUserManager = new AppUserManager(appUserManagerRepository.Object, null);

            await appUserManager.EnterToApplication(new AppUser() { ExternalId="abc"});

            Assert.AreEqual("abc", appUserManager.GetLoggedAppUserExternalId());
        }
    }
}
