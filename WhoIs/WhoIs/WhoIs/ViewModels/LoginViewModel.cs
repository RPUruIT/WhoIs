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
        protected IAppUserManager _appUserManager;

        public string IconSource { get; } = "ic_uruit.png";
        public string AppName { get;} = "Who Is?";
        public string UsersPlaceholder { get; } = "Usuario";
        public string BtnEnter { get; } = "Ingresar";
        public string TextFooterLeft { get; } = "http://uruit.com";
        public string TextFooterRigth { get; } = "@People Care";

        private IList<User> _users;

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

        public LoginViewModel(IAppUserManager appUserManager)
        {
            _appUserManager = appUserManager;
            CmdEnterToApplication = new Command(async () => await EnterToApplication());
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IList<User> users = await _appUserManager.GetUsersFromService();
            IList<AppUser> appUsers = await _appUserManager.GetSpecificUsersFromUsers(users);

            appUsers = appUsers.OrderBy(u => u.Name).ToList();

            AppUsers = appUsers;
            _users = users;

        }

        public async Task EnterToApplication()
        {
            if (AppUserSelectedIndex >= 0)
            {
                AppUser appUser = AppUsers[AppUserSelectedIndex];
                await _appUserManager.EnterToApplication(appUser);

                await _navigationService.NavigateToAsync<HomeViewModel>();//_jsonUsers

            }
            //TODO implement  a message if no user selected
        }

        public async Task<bool> IsUserLogged()
        {
            return await _appUserManager.GetLoggedAppUser() != null;
        }
    }
}
