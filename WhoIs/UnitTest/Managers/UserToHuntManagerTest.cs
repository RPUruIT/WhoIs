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
using System.Linq;

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


            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager = new UserToHuntManager(
                                            userToHuntRepositoryMock.Object,
                                            userManagerMock.Object, appUserManagerMock.Object);

            List<UserToHunt> usersToHunt = await userToHuntManager.GetUsersToHunt(new List<User>());

            userManagerMock.Verify(x => x.GetUsersFromService(), Times.Exactly(0));
        }

        //This test lets test GetSpecificUsersFromService and GetSpecificUsersFromUsers
        [TestMethod]
        public async Task UserToHuntManager_GetUsersToHunt_CheckResultCount()
        {
            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();
            var userManagerMock = new Mock<IUserManager>();
            var appUserManagerMock = new Mock<IAppUserManager>();


            List<UserToHunt> usersHunted = new List<UserToHunt>()
            { new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc2",ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc5",ImgPath="img.png"}};

            List<User> usersReturnedByUserManagerFromService = new List<User> 
            {  new User() { ExternalId = "abc1"},
               new User() { ExternalId = "abc2"},
               new User() { ExternalId = "abc3"},
               new User() { ExternalId = "abc4"},
               new User() { ExternalId = "abc5"} };

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(usersHunted);

            userManagerMock.Setup(x => x.GetUsersFromService()).ReturnsAsync(usersReturnedByUserManagerFromService);

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager = new UserToHuntManager(
                                            userToHuntRepositoryMock.Object,
                                            userManagerMock.Object, appUserManagerMock.Object);

            //param null
            List<User> users = null;
            List<UserToHunt> usersToHunt = await userToHuntManager.GetUsersToHunt(users);

            Assert.AreEqual(5, usersToHunt.Count);

        }

        [TestMethod]
        public async Task UserToHuntManager_GetUsersToHuntUpdated_NullParams()
        {

            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();
            var appUserManagerMock = new Mock<IAppUserManager>();

            List<UserToHunt> usersHunted = null;
            List<UserToHunt> usersToHunt = null;

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(usersHunted);

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager =
                new UserToHuntManager(userToHuntRepositoryMock.Object, null, appUserManagerMock.Object);
  

            List<UserToHunt> usersToHuntResult = await userToHuntManager.GetUsersToHuntUpdated(usersToHunt);

            Assert.IsNotNull(usersToHuntResult);
            Assert.IsTrue(usersToHuntResult.Count == 0);
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


            List<UserToHunt> usersToHunt = new List<UserToHunt>()
             { new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
               new UserToHunt() { ExternalId="abc2",ImgPath=""},
               new UserToHunt() { ExternalId="abc3",ImgPath=""},
               new UserToHunt() { ExternalId="abc4",ImgPath=""},
               new UserToHunt() { ExternalId="abc5",ImgPath=""}};

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(usersHunted);

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager =
                new UserToHuntManager(userToHuntRepositoryMock.Object, null, appUserManagerMock.Object);


            List<UserToHunt> usersToHuntResult = await userToHuntManager.GetUsersToHuntUpdated(usersToHunt);

            //check the three first userToHunt are already hunted
            for (var i = 0; i < 3; i++)
            {
                Assert.IsTrue(usersToHuntResult[i].IsHunted);
            }
            //check the others are not hunted yet
            for (var i = 3; i < 5; i++)
            {
                Assert.IsFalse(usersToHuntResult[i].IsHunted);
            }


        }

        //This method GetUsersToHuntUpdated MUST return and updated list of usersToHunt
        //order by [hunted][no hunted yet] both ordered by name
        [TestMethod]
        public async Task UserToHuntManager_GetUsersToHuntUpdated_OrderIndividualListsByName()
        {

            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();
            var appUserManagerMock = new Mock<IAppUserManager>();

            List<UserToHunt> usersHunted = new List<UserToHunt>()
            { new UserToHunt() { ExternalId="abc1",ImgPath="img.png",Name="Z"},
              new UserToHunt() { ExternalId="abc2",ImgPath="img.png",Name="X"},
              new UserToHunt() { ExternalId="abc5",ImgPath="img.png",Name="A"}};


            List<UserToHunt> usersToHunt = new List<UserToHunt>()
             { new UserToHunt() { ExternalId="abc1",ImgPath="img.png",Name="Z"},
               new UserToHunt() { ExternalId="abc2",ImgPath="img.png",Name="X"},
               new UserToHunt() { ExternalId="abc3",ImgPath="",Name="B"},
               new UserToHunt() { ExternalId="abc4",ImgPath="",Name="C"},
               new UserToHunt() { ExternalId="abc5",ImgPath="img.png",Name="A"}};

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(usersHunted);

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager =
                new UserToHuntManager(userToHuntRepositoryMock.Object, null, appUserManagerMock.Object);


            List<UserToHunt> usersToHuntResult = await userToHuntManager.GetUsersToHuntUpdated(usersToHunt);

            //hunted ordered by name
            Assert.AreEqual(usersToHuntResult[0].Name, "A");
            Assert.AreEqual(usersToHuntResult[1].Name, "X");
            Assert.AreEqual(usersToHuntResult[2].Name, "Z");

            //not hunted already orderd by name
            Assert.AreEqual(usersToHuntResult[3].Name, "B");
            Assert.AreEqual(usersToHuntResult[4].Name, "C");


        }

        [TestMethod]
        public async Task UserToHuntManager_GetUsersToHuntUpdated_CountUpdated()
        {

            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();
            var appUserManagerMock = new Mock<IAppUserManager>();

            List<UserToHunt> usersHunted = new List<UserToHunt>()
            { new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc2",ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc5",ImgPath="img.png"}};


            List<UserToHunt> usersToHunt = new List<UserToHunt>()
             { new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
               new UserToHunt() { ExternalId="abc2",ImgPath=""},
               new UserToHunt() { ExternalId="abc3",ImgPath=""},
               new UserToHunt() { ExternalId="abc4",ImgPath=""},
               new UserToHunt() { ExternalId="abc5",ImgPath=""}};

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(usersHunted);

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager =
                new UserToHuntManager(userToHuntRepositoryMock.Object, null, appUserManagerMock.Object);


            await userToHuntManager.GetUsersToHuntUpdated(usersToHunt);

            Assert.AreEqual(3, userToHuntManager.GetCountUsersHunted());
            Assert.AreEqual(5, userToHuntManager.GetCountUsersToHunt());

        }

        //This method GetUsersToHuntUpdated return and updated list of usersToHunt, 
        //in which all users already hunted MUST be included despite they aren´t in the original list because they were removed
        [TestMethod]
        public async Task UserToHuntManager_GetUsersToHuntUpdated_UsersRemovedFromOriginalList()
        {

            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();
            var appUserManagerMock = new Mock<IAppUserManager>();

            string external_id_removed = "abc2";

            //user abc2 is hunted
            List<UserToHunt> usersHunted = new List<UserToHunt>()
            { new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
              new UserToHunt() { ExternalId=external_id_removed,ImgPath="img.png"},
              new UserToHunt() { ExternalId="abc5",ImgPath="img.png"}};

            //user abc2 is not in the original list
            List<UserToHunt> usersToHunt = new List<UserToHunt>()
             { new UserToHunt() { ExternalId="abc1",ImgPath="img.png"},
               new UserToHunt() { ExternalId="abc3",ImgPath=""},
               new UserToHunt() { ExternalId="abc4",ImgPath=""},
               new UserToHunt() { ExternalId="abc5",ImgPath=""}};

            userToHuntRepositoryMock.Setup(x => x.GetHuntedUsers(It.IsAny<string>()))
                                            .ReturnsAsync(usersHunted);

            appUserManagerMock.Setup(x => x.GetLoggedAppUserExternalId()).Returns("abc");

            UserToHuntManager userToHuntManager =
                new UserToHuntManager(userToHuntRepositoryMock.Object, null, appUserManagerMock.Object);


            List<UserToHunt> usersToHuntResult = await userToHuntManager.GetUsersToHuntUpdated(usersToHunt);

            //check abc2 is in the list 
            Assert.AreEqual(1, usersToHuntResult.Where(u => u.ExternalId == external_id_removed).Count());


        }


        [TestMethod]
        public async Task UserToHuntManager_HuntUser_CheckCountHunted()
        {
            var userToHuntRepositoryMock = new Mock<IUserToHuntRepository>();

            UserToHuntManager userToHuntManager =
                new UserToHuntManager(userToHuntRepositoryMock.Object, null, null);

            int countHuntedBeforeHunt = userToHuntManager.GetCountUsersHunted();

            await userToHuntManager.HuntUser(new UserToHunt());

            int countHuntedAfterHunt = userToHuntManager.GetCountUsersHunted();

            Assert.AreEqual(countHuntedBeforeHunt + 1, countHuntedAfterHunt);
        }
    }
}
