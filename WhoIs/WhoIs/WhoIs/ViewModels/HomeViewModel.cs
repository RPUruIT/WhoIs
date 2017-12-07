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
using WhoIs.Configs;

namespace WhoIs.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        private IUserToHuntManager _userToHuntManager;
        private IAppUserManager _appUserManager;

        private string _appUserLogged;
        public string AppUserLogged
        {
            get { return _appUserLogged; }
            set { SetPropertyValue(ref _appUserLogged, value); }
        }

        public string LogoutIcon { get; } = ResourcesName.IMG_LOGOUT;
        public string HomeTitle { get; } = "UruITers";

        private string _huntIndicator;
        public string HuntIndicator
        {
            get { return _huntIndicator; }
            set { SetPropertyValue(ref _huntIndicator, value); }
        }

        private ICommand _cmdLogout;
        public ICommand CmdLogout
        {
            get { return _cmdLogout; }
            set { SetPropertyValue(ref _cmdLogout, value); }
        }

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
            AppUser appUser = await _appUserManager.GetAndSetLoggedAppUser();
            AppUserLogged = appUser.Name;
            CmdLogout = new Command(async () => await Logout());
            await Refresh();
        }

        public async Task Logout()
        {
            await Task.Delay(1);
            bool accepted = await DisplayAlert(Constants.APP_NAME,
                                "Está seguro que desea salir?",
                                "Aceptar", "Cancelar");

            if (accepted)
            {
                await _appUserManager.LogoutFromApplication();
                await _navigationService.NavigateToAsync<LoginViewModel>();

            }

        }

        public override async Task Refresh()
        {
            IsLoading = true;

            List<UserToHunt> usersToHunt = await _userToHuntManager.GetUsersToHunt();

            UsersToHunt = new ObservableCollection<UserToHunt>();
            foreach (UserToHunt user in usersToHunt)
                UsersToHunt.Add(user);

            UpdateHuntIndicator();

            IsLoading = false;
        }

        private void UpdateHuntIndicator()
        {
            int totalUsersToHunt = _userToHuntManager.GetCountUsersToHunt();
            int countUsersHunted = _userToHuntManager.GetCountUsersHunted();
            HuntIndicator = countUsersHunted + "/" + totalUsersToHunt;
        }

        public async void UserToHuntSelected(UserToHunt userToHunt)
        {

            if (!userToHunt.HasImage())
            {
                IPictureTaker pictureTake = DependencyService.Get<IPictureTaker>();
<<<<<<< HEAD
                string appUserExternalId = _appUserManager.GetLoggedAppUserExternalId();
=======
                string appUserExternalId =  _appUserManager.GetLoggedAppUserExternalId();
>>>>>>> develop
                
                MessagingCenter.Subscribe<IPictureTaker, string[]>(this, Constants.PICTURE_TAKER_EVENT_NAME, async (s, imageFiles) =>
                {
                    await PictureTakenCompleted(appUserExternalId,userToHunt,imageFiles);

                });

                pictureTake.SnapPic(appUserExternalId, userToHunt.Name);

            }
            else
            {
                await NavigateToDetails(userToHunt, true);
            }
        }

        private async Task PictureTakenCompleted(string appUserExternalId,UserToHunt userToHunt, string[] imageFiles)
        {
            MessagingCenter.Unsubscribe<IPictureTaker, string[]>(this, Constants.PICTURE_TAKER_EVENT_NAME);

            UserToHunt userToHuntToConfirm = new UserToHunt(userToHunt);
            userToHuntToConfirm.ImgPath = imageFiles[0];
            userToHuntToConfirm.ImgThumbnailPath = imageFiles[1];
            userToHuntToConfirm.HunterId = appUserExternalId;

            await NavigateToDetails(userToHuntToConfirm, false);
        }

        private async Task NavigateToDetails(UserToHunt userToHunt, bool seeDetails)
        {
            UserHuntedDetailsViewParameter param =
                  new UserHuntedDetailsViewParameter() { UserToHunt = userToHunt, SeeDetails = seeDetails };
            await _navigationService.NavigateToAsync<UserHuntedDetailsViewModel>(param);
        }


    }
}
