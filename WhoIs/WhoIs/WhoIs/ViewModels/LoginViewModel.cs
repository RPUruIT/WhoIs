using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Services.Interface;
using Xamarin.Forms;

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

        private string _jsonUsers;
        private IList<AppUser> _appUsers;
        public IList<AppUser> AppUsers
        {
            get { return _appUsers; }
            set { SetPropertyValue(ref _appUsers, value); }
        }


        private int _appUserSelectedIndex;
        public int AppUserSelectedIndex
        {
            get { return _appUserSelectedIndex; }
            set { SetPropertyValue(ref _appUserSelectedIndex, value); }
        }

        public ICommand CmdEnterToApplication { get; set; }


        public LoginViewModel()
        {
            CmdEnterToApplication = new Command(async () => await EnterToApplication());
        }

        public override async Task InitializeAsync(object navigationData)
        {
            Object[] usersFromService = await _appUserManager.GetAppUsersFromService();
            IList<AppUser> appUsers = usersFromService[0] as IList<AppUser>;
            string jsonUsers = usersFromService[1] as string;

            AppUsers = appUsers.OrderBy(u => u.Name).ToList();
            _jsonUsers = jsonUsers;
        }

        public async Task EnterToApplication()
        {
            if (AppUserSelectedIndex >= 0)
            {
                AppUser appUser = AppUsers[AppUserSelectedIndex];
                await _appUserManager.EnterToApplication(appUser);

                await _navigationService.NavigateToAsync<HomeViewModel>(_jsonUsers);

            }
        }

    }
}
