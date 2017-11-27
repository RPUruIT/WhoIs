using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using WhoIs.Configs;
using WhoIs.Models;
using WhoIs.Repositories.Interface;

namespace WhoIs.Repositories
{
    public class RepositoryContainer
    {

        public static void InitializeServices(IUnityContainer container)
        {
            container.RegisterType<IDatabase<UserToHunt>, Database<UserToHunt>>(new ContainerControlledLifetimeManager(), new InjectionConstructor(Constants.DB_NAME));
            container.RegisterType<IUserToHuntRepository, UserToHuntRepository>(new ContainerControlledLifetimeManager());

            container.RegisterType<IDatabase<AppUser>, Database<AppUser>>(new ContainerControlledLifetimeManager(), new InjectionConstructor(Constants.DB_NAME));
            container.RegisterType<IAppUserRepository, AppUserRepository>(new ContainerControlledLifetimeManager());
        }

    }
}
