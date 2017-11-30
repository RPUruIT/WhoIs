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

namespace WhoIs.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string HomeTitle { get; } = "UruITers";
        public string HuntIndicator { get; set; } = "0/0";

        private IUserToHuntManager _userToHuntManager;

        private List<UserToHunt> _usersToHunt;
        public List<UserToHunt> UsersToHunt
        {
            get { return _usersToHunt; }
            set { SetPropertyValue(ref _usersToHunt, value); }
        }

        public ICommand UserToHuntSelectedCommand { get; set; }

        public HomeViewModel(IUserToHuntManager userToHuntManager)
        {
            _userToHuntManager = userToHuntManager;
            UserToHuntSelectedCommand = new Command<UserToHunt>(UserToHuntSelected);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                UsersToHunt = await _userToHuntManager.GetUsersToHunt(navigationData as List<User>);
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
