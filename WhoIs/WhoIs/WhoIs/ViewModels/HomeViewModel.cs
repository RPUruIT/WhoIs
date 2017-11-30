using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Services.Interface;
using Xamarin.Forms;
using Unity;

namespace WhoIs.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string HomeTitle { get; } = "UruITers";

        private string _huntIndicator;
        public string HuntIndicator
        {
            get { return CountUsersHunted + "/" + TotalUsersToHunt; }
            set { SetPropertyValue(ref _huntIndicator, value); }
        }


        private int _totalUsersToHunt;
        public int TotalUsersToHunt
        {
            get { return _totalUsersToHunt; }
            set {_totalUsersToHunt = value; HuntIndicator = "";}
        }
        private int _countUsersHunted;
        public int CountUsersHunted
        {
            get { return _countUsersHunted; }
            set { _countUsersHunted = value; HuntIndicator = ""; }
        }


        private IUserToHuntManager _userToHuntManager;
        private IAppUserManager _appUserManager;

        private List<UserToHunt> _usersToHunt;
        public List<UserToHunt> UsersToHunt
        {
            get { return _usersToHunt; }
            set { SetPropertyValue(ref _usersToHunt, value); }
        }

        public ICommand UserToHuntSelectedCommand { get; set; }

        public HomeViewModel(IUserToHuntManager userToHuntManager, IAppUserManager appUserManager)
        {
            _userToHuntManager = userToHuntManager;
            _appUserManager = appUserManager;
            UserToHuntSelectedCommand = new Command<UserToHunt>(UserToHuntSelected);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                await _appUserManager.GetAndSetLoggedAppUser();//THIS BETTER TO BE IN NAVIGATION PAGE BEFORE LOAD HOMEVIEW BUT IT THROWS AN EXCEPTION
                UsersToHunt = await _userToHuntManager.GetUsersToHunt(navigationData as List<User>);
                TotalUsersToHunt = await _userToHuntManager.GetCountUsersToHunt();
                CountUsersHunted = await _userToHuntManager.GetCountUsersHunted();
            }
            catch(Exception ex)
            {

            }

        }

        public void UserToHuntSelected(UserToHunt userToHunt)
        { 
            //await Task.Delay(1);
        }
    
}
}
