using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using WhoIs.Configs;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Services.Interface;
using Xamarin.Forms;

namespace WhoIs.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        private IUserManager _userManager;
        private IAppUserManager _appUserManager;

        public string IconSource { get; } = ResourcesName.IMG_APP_ICON;
        public string AppName { get;} = Constants.APP_NAME;
        public string UsersPlaceholder { get; } = "Usuario";
        public string BtnEnter { get; } = "Ingresar";
        public string TextFooterLeft { get; } = "http://uruit.com";
        public string TextFooterRight { get; } = "@People Care";

        private List<User> _users;

        private List<AppUser> _appUsers;
        public List<AppUser> AppUsers
        {
            get { return _appUsers; }
            set { SetPropertyValue(ref _appUsers, value); }
        }

        private AppUser _appUserSelected;
        public AppUser AppUserSelected
        {
            get { return _appUserSelected; }
            set { SetPropertyValue(ref _appUserSelected, value); }
        }

        public ICommand CmdEnterToApplication { get; set; }

        public LoginViewModel(INavigationService navigationService, IUserManager userManager,IAppUserManager appUserManager):base(navigationService)
        {
            _userManager = userManager;
            _appUserManager = appUserManager;
            CmdEnterToApplication = new Command(async () => await EnterToApplication());
        }

        public override async Task InitializeAsync(object navigationData)
        {
            List<User> users = await _userManager.GetUsersFromService();
            List<AppUser> appUsers = await _appUserManager.GetSpecificUsersFromUsers(users);

            AppUsers = appUsers;
            _users = users;

        }

        public async Task EnterToApplication()
        {
            if (AppUserSelected!=null)
            {                
                await _appUserManager.EnterToApplication(AppUserSelected);

                await _navigationService.NavigateToAsync<HomeViewModel>(_users);

            }
            //TODO implement  a message if no user selected
        }

        
    }
}
