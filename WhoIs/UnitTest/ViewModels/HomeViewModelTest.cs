using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using WhoIs;
using WhoIs.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhoIs.Services.Interface;
using Moq;
using WhoIs.Managers.Interface;
using WhoIs.Models;

namespace UnitTest.ViewModels
{
    [TestClass]
    public class HomeViewModelTest
    {
        [TestMethod]
        public async Task HomeViewModel_Initialize()
        {
            var navigationServiceMock = new Mock<INavigationService>();
            var userToHuntManagerMock = new Mock<IUserToHuntManager>();
            var appUserManagerMock = new Mock<IAppUserManager>();

            List<UserToHunt> usersHunted = new List<UserToHunt>()
            {
              new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc2",ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc3",ImgPath="img.png"}
            };
            List<UserToHunt> usersToHunt = new List<UserToHunt>();
            usersToHunt.AddRange(usersHunted);
            usersToHunt.Add(new UserToHunt() { ExternalId = "abc4" });

            AppUser userLogged = new AppUser() { Name = "Pelopincho" };

            userToHuntManagerMock.Setup(x => x.GetUsersToHunt(It.IsAny<List<User>>())).ReturnsAsync(usersToHunt);
            userToHuntManagerMock.Setup(x => x.GetCountUsersHunted()).Returns(usersHunted.Count);
            userToHuntManagerMock.Setup(x => x.GetCountUsersToHunt()).Returns(usersToHunt.Count);

            appUserManagerMock.Setup(x => x.GetAndSetLoggedAppUser()).ReturnsAsync(userLogged);

            HomeViewModel homeViewModel = new HomeViewModel(navigationServiceMock.Object,
                                                            userToHuntManagerMock.Object,
                                                            appUserManagerMock.Object);

            await homeViewModel.InitializeAsync(null);

            Assert.AreEqual("Pelopincho", homeViewModel.AppUserLogged);
            Assert.AreEqual(usersHunted.Count+"/"+ usersToHunt.Count, homeViewModel.HuntIndicator);
            Assert.AreEqual(usersToHunt.Count, homeViewModel.UsersToHunt.Count());


        }

    }
}
