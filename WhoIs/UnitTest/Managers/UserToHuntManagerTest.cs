using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhoIs;
using WhoIs.Managers;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Repositories.Interface;

namespace UnitTest.Managers
{
    [TestClass]
    public class UserToHuntManagerTest {

        //When GetUsersToHunt is called with null, it MUST go to the service to bring the users
        [TestMethod]
        public async Task UserToHuntManager_GetUsersToHunt_NullParam()
        {
            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();
            var userManagerMock = new Mock<IUserManager>();
            var appUserManagerMock = new Mock<IAppUserManager>();

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(new List<UserToHunt>());

            userManagerMock.Setup(x => x.GetUsersFromService()).ReturnsAsync(new List<User>());

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager = new UserToHuntManager(
                                            userToHuntRepositoryMock.Object,
                                            userManagerMock.Object, appUserManagerMock.Object);

            //param null
            List<User> users = null;
            List<UserToHunt> usersToHunt = await userToHuntManager.GetUsersToHunt(users);

            userManagerMock.Verify(x => x.GetUsersFromService(), Times.Once);
        }

        //When GetUsersToHunt is called with users (not null), it MUST NOT go to the service to bring the users
        [TestMethod]
        public async Task UserToHuntManager_GetUsersToHunt_NotNullParam()
        {
            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();
            var userManagerMock = new Mock<IUserManager>();
            var appUserManagerMock = new Mock<IAppUserManager>();

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(new List<UserToHunt>());

            userManagerMock.Setup(x => x.GetUsersFromService()).ReturnsAsync(new List<User>());

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager = new UserToHuntManager(
                                            userToHuntRepositoryMock.Object,
                                            userManagerMock.Object, appUserManagerMock.Object);

            List<UserToHunt> usersToHunt = await userToHuntManager.GetUsersToHunt(new List<User>());

            userManagerMock.Verify(x => x.GetUsersFromService(), Times.Exactly(0));
        }

        //This method GetUsersToHuntUpdated MUST return and updated list of usersToHunt order by [hunted][no hunted yet]
        [TestMethod]
        public async Task UserToHuntManager_GetUsersToHuntUpdated_Order()
        {

            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();
            var appUserManagerMock = new Mock<IAppUserManager>();

            List<UserToHunt> usersHunted = new List<UserToHunt>()
            { new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc2",ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc5",ImgPath="img.png"}};

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(usersHunted);

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager = 
                new UserToHuntManager(userToHuntRepositoryMock.Object, null, appUserManagerMock.Object);

            List<UserToHunt> usersToHunt = new List<UserToHunt>()
             { new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
               new UserToHunt() { ExternalId="abc2",ImgPath=""},
               new UserToHunt() { ExternalId="abc3",ImgPath=""},
               new UserToHunt() { ExternalId="abc4",ImgPath=""},
               new UserToHunt() { ExternalId="abc5",ImgPath=""}};

            List<UserToHunt> usersToHuntResult = await userToHuntManager.GetUsersToHuntUpdated(usersToHunt);

            //check the three first userToHunt are already hunted
            for (var i = 0; i < 3; i++)
            {
                Assert.IsTrue(usersToHuntResult[i].HasImage());
            }
            //check the others are not hunted yet
            for (var i = 3; i < 5; i++)
            {
                Assert.IsFalse(usersToHuntResult[i].HasImage());
            }


        }


    }
}
