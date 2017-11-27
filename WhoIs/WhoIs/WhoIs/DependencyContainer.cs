using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace WhoIs
{
    public class DependencyContainer
    {
        public static IUnityContainer Container { get; private set; }

        public static void Initialize()
        {
            Container = new UnityContainer();
        }

        public static void InitializeCore()
        {
            DependencyContainer.Initialize();
            Services.ServiceContainer.InitializeServices(DependencyContainer.Container);
            Managers.ManagerContainer.InitializeServices(DependencyContainer.Container);
            ViewModels.ViewModelContainer.InitializeViewModels(DependencyContainer.Container);
        }
    }
}
