using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Services.Interface;

namespace WhoIs.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }
    }
}
