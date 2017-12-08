using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace WhoIs.ViewModels
{
    public class ViewModelContainer
    {
        public static void InitializeViewModels(IUnityContainer container)
        {
            container.RegisterType<UserHuntedDetailsViewModel>();
            container.RegisterType<HomeViewModel>();
            container.RegisterType<LoginViewModel>();
        }
    }
}
