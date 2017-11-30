using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using WhoIs.Managers.Interface;
using WhoIs.Models;

namespace WhoIs.Managers
{
    public class ManagerContainer
    {
        public static void InitializeServices(IUnityContainer container)
        {
            container.RegisterType<IUserManager, UserManager>(new ContainerControlledLifetimeManager());

            container.RegisterType<IUserToHuntManager, UserToHuntManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAppUserManager, AppUserManager>(new ContainerControlledLifetimeManager());
        }
    }
}
