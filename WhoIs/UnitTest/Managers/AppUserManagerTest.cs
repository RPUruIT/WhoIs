using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using WhoIs;
using WhoIs.Managers.Interface;
using WhoIs.Models;

namespace UnitTest.Managers
{
    [TestClass]
    public class AppUserManagerTest
    {

        IAppUserManager _appUserManager;

        public AppUserManagerTest()
        {
            _appUserManager = DependencyContainer.Container.Resolve<IAppUserManager>();
        }

        [TestMethod]
        public async Task AppUserManager_Login_Sucess()
        {
            AppUser appUser = new AppUser() {ExternalId = "abc1", Name = "Marcelo Lopez" };

            await _appUserManager.EnterToApplication(appUser);
            appUser = await _appUserManager.GetAndSetLoggedAppUser();

            Assert.IsTrue(await _appUserManager.IsUserLogged());
            Assert.IsNotNull(appUser);
        }
    }
}
