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
        private IUserToHuntManager _userToHuntManager;

        private IList<UserToHunt> _usersToHunt;
        public IList<UserToHunt> UsersToHunt
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

                UsersToHunt = usersToHunt;
            }
            catch(Exception ex)
            {

            }

        }

        public void UserToHuntSelected(UserToHunt userToHunt)
        {
            //TODO https://blog.wislon.io/posts/2017/04/11/xamforms-listview-selected-colour
            //await Task.Delay(1);
        }
    }
}
