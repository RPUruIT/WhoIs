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

        public string Title { get; } = "Who is?";

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
            try
            {
                IList<UserToHunt> usersToHunt = null;
                if (navigationData != null)
                {
                    IList<User> users = navigationData as IList<User>;
                    usersToHunt = await _userToHuntManager.GetSpecificUsersFromUsers(users);
                }
                else
                {
                    usersToHunt = await _userToHuntManager.GetSpecificUsersFromService();
                    
                }

                //UsersToHunt = usersToHunt;
            }
            catch(Exception ex)
            {

            }

        }
    }
}
