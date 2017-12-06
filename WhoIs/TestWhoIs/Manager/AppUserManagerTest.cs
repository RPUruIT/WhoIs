﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public async void GetUsers_Filter_Deleted()
        {
            IUserManager userManager = DependencyContainer.Container.Resolve<IUserManager>();

            int usersCount = (await userManager.GetUsersFromService()).Count;
            Assert.IsTrue(usersCount.Equals(3));

        }

    }
}
