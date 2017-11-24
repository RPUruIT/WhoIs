using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using WhoIs.Services.Interface;

namespace WhoIs.Services
{
    public class ServiceContainer
    {
        public static void InitializeServices(IUnityContainer container)
        {
            container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());
        }
    }
}
