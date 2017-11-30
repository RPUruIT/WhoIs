using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using WhoIs.Configs;
using WhoIs.Services.Interface;

namespace WhoIs.Services
{
    public class ServiceContainer
    {
        public static void InitializeServices(IUnityContainer container)
        {
            container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            if (Constants.IS_TEST)
                container.RegisterType<IService, ServiceMOC>(new ContainerControlledLifetimeManager());
            else
                container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());
        }
    }
}
