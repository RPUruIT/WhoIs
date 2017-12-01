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
using System.Collections.ObjectModel;
using System.IO;
using WhoIs.ViewModels.Helper;

namespace WhoIs.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private string _appUserLogged;
        public string AppUserLogged
        {
            get { return _appUserLogged; }
            set { SetPropertyValue(ref _appUserLogged, value); }
        }

        public string HomeTitle { get; } = "UruITers";

        private string _huntIndicator;
        public string HuntIndicator
        {
            get { return _huntIndicator; }
            set { SetPropertyValue(ref _huntIndicator, value); }
        }

        private IUserToHuntManager _userToHuntManager;
        private IAppUserManager _appUserManager;

        private ObservableCollection<UserToHunt> _usersToHunt;
        public ObservableCollection<UserToHunt> UsersToHunt
        {
            get { return _usersToHunt; }
            set { SetPropertyValue(ref _usersToHunt, value); }
        }


        private UserToHunt _listSelectedItem;
        public UserToHunt ListSelectedItem
        {
            get { return _listSelectedItem; }
            set
            {
                if (value != null)
                    UserToHuntSelected(value);
            }
        }

        public HomeViewModel(IUserToHuntManager userToHuntManager, IAppUserManager appUserManager)
        {
            _userToHuntManager = userToHuntManager;
            _appUserManager = appUserManager;
         }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                AppUser appUser = await _appUserManager.GetAndSetLoggedAppUser();//THIS BETTER TO BE IN NAVIGATION PAGE BEFORE LOAD HOMEVIEW BUT IT THROWS AN EXCEPTION
                AppUserLogged = appUser.Name;

                List<UserToHunt> usersToHunt = await _userToHuntManager.GetUsersToHunt(navigationData as List<User>);

                UsersToHunt = new ObservableCollection<UserToHunt>();
                foreach (UserToHunt user in usersToHunt)
                    UsersToHunt.Add(user);

                await UpdateHuntIndicator();
            }
            catch (Exception ex)
            {

            }

        }

        private async Task UpdateHuntIndicator()
        {
            int totalUsersToHunt = await _userToHuntManager.GetCountUsersToHunt();
            int countUsersHunted = await _userToHuntManager.GetCountUsersHunted();
            HuntIndicator = countUsersHunted + "/" + totalUsersToHunt;
        }

        public async void UserToHuntSelected(UserToHunt userToHunt)
        {
            if (!userToHunt.HasImage())
            {
                IPictureTaker pictureTake = DependencyService.Get<IPictureTaker>();
                string appUserExternalId = await _appUserManager.GetLoggedAppUserExternalId();
                
                MessagingCenter.Subscribe<IPictureTaker, string[]>(this, "pictureTaken", async (s, imageFiles) =>
                {
                    MessagingCenter.Unsubscribe<IPictureTaker, string[]>(this, "pictureTaken");

                    UserToHunt userToHuntToConfirm = new UserToHunt(userToHunt);
                    userToHuntToConfirm.ImgPath = imageFiles[0];
                    userToHuntToConfirm.ImgThumbnailPath = imageFiles[1];
                    userToHuntToConfirm.HunterId = appUserExternalId;

                    UserHuntedDetailsViewParameter param =
                    new UserHuntedDetailsViewParameter() { UserToHunt = userToHuntToConfirm, SeeDetails = false };

                    await _navigationService.NavigateToAsync<UserHuntedDetailsViewModel>(param);
                    
                    //TODO, JUST FOR TEST
                    //await _userToHuntManager.HuntUser(userToHunt);
                    //await UpdateHuntIndicator();
                });

                pictureTake.SnapPic(appUserExternalId, userToHunt.Name);

            }
            else
            {

            }
        }


    }
}
