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
            container.RegisterSingleton<IDatabase<UserToHunt>, Database<UserToHunt>>(new InjectionConstructor(Constants.DB_NAME));
            container.RegisterSingleton<IDatabase<AppUser>, Database<AppUser>>(new InjectionConstructor(Constants.DB_NAME));

            container.RegisterSingleton<IUserToHuntRepository, UserToHuntRepository>();
            container.RegisterSingleton<IAppUserRepository, AppUserRepository>();
        }

    }
}
