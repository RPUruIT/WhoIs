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
        IUserManager _userManager;

        public HomeViewModel(INavigationService navigationService, IUserManager userManager) : base(navigationService)
        {
            _userManager = userManager;
        }
    }
}
