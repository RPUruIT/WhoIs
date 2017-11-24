using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Services.Interface;
using Unity;

namespace WhoIs.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {

        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
            IService service1 = DependencyContainer.Container.Resolve<IService>();
            service1.GetUsers();
        }

    }
}
