using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Services.Interface;

namespace WhoIs.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {

        public string IconSource { get; } = "ic_uruit.png";
        public string AppName { get;} = "Who Is?";
        public string UsersPlaceholder { get; } = "Usuario";
        public string BtnEnter { get; } = "Ingresar";
        public string TextFooterLeft { get; } = "http://uruit.com";
        public string TextFooterRigth { get; } = "@People Care";

        private IList<AppUser> _appUsers;
        public IList<AppUser> AppUsers
        {
            get { return _appUsers; }
            set { SetPropertyValue(ref _appUsers, value); }
        }



        public override async Task InitializeAsync(object navigationData)
        {
            IList<AppUser> appUsers = await _appUserManager.GetUsersFromService();
            AppUsers = appUsers.OrderBy(u => u.Name).ToList();
        }

    }
}
