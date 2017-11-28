using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Services.Interface;

namespace WhoIs.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        IUserToHuntManager _userToHuntManager;

        private IList<UserToHunt> _usersToHunt;
        public IList<UserToHunt> UsersToHunt
        {
            get { return _usersToHunt; }
            set { SetPropertyValue(ref _usersToHunt, value); }
        }

        public HomeViewModel(IUserToHuntManager userToHuntManager)
        {
            _userToHuntManager = userToHuntManager;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                string jsonUsers = navigationData as string;
                UsersToHunt = _userToHuntManager.GetUsersFromJson(jsonUsers);
            }
            else
            {
                UsersToHunt =  await _userToHuntManager.GetUsersFromService();
            }
        }
    }
}
