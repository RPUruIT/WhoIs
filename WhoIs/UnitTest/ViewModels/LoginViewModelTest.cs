using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhoIs.ViewModels;
using System.Collections.Generic;
using WhoIs.Models;
using WhoIs.Services.Interface;

namespace UnitTest.ViewModels
{
    [TestClass]
    public class LoginViewModelTest
    {

        [TestMethod]
        public async Task LoginViewModel_Initialize_UsersNull()
        {

            var navigationServiceMock = new Mock<INavigationService>();
            var userManagerMock = new Mock<IUserManager>();
            var appUserManagerMock = new Mock<IAppUserManager>();


            userManagerMock.Setup(x => x.GetUsersFromService()).ReturnsAsync((List<User>)null);

            var loginViewModel = new LoginViewModel(navigationServiceMock.Object, 
                                                    userManagerMock.Object, 
                                                    appUserManagerMock.Object);

            await loginViewModel.InitializeAsync(null);

        }

        [TestMethod]
        public async Task LoginViewModel_Initialize_UsersEmpty()
        {

            var navigationServiceMock = new Mock<INavigationService>();
            var userManagerMock = new Mock<IUserManager>();
            var appUserManagerMock = new Mock<IAppUserManager>();


            userManagerMock.Setup(x => x.GetUsersFromService()).ReturnsAsync(new List<User>());

            var loginViewModel = new LoginViewModel(navigationServiceMock.Object,
                                                    userManagerMock.Object,
                                                    appUserManagerMock.Object);

            await loginViewModel.InitializeAsync(null);
        }

    }
}
