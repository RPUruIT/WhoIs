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
            container.RegisterSingleton<INavigationService, NavigationService>();
            if (Constants.IS_TEST)
                container.RegisterSingleton<IService, ServiceMOC>();
            else
                container.RegisterSingleton<IService, Service>();
        }
    }
}
