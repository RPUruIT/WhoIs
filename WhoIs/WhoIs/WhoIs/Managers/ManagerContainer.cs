using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using WhoIs.Managers.Interface;

namespace WhoIs.Managers
{
    public class ManagerContainer
    {
        public static void InitializeServices(IUnityContainer container)
        {
            container.RegisterType<IUserHuntedManager, UserHuntedManager>(new ContainerControlledLifetimeManager());
        }
    }
}
