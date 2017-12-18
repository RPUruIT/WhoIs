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

        public string ImgMagnifier { get; } = ResourcesName.IMG_MAGNIFIER;

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

        private List<UserToHunt> _usersToHunt;
        private ObservableCollection<UserToHuntGroup> _usersToHuntGrouped;
        public ObservableCollection<UserToHuntGroup> UsersToHuntGrouped
        {
            get { return _usersToHuntGrouped; }
            set { SetPropertyValue(ref _usersToHuntGrouped, value); }
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

        public HomeViewModel(INavigationService navigationService,IUserToHuntManager userToHuntManager, IAppUserManager appUserManager):base(navigationService)
        {
            _userToHuntManager = userToHuntManager;
            _appUserManager = appUserManager;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsLoading = true;
            AppUser appUser = await _appUserManager.GetAndSetLoggedAppUser();
            AppUserLogged = appUser.Name;
            CmdLogout = new Command(async () => await Logout());
            List<UserToHunt> usersToHunt = await _userToHuntManager.GetUsersToHunt(navigationData as List<User>);
            await LoadUsersToHunt(usersToHunt);
            IsInitialized = true;
            IsLoading = false;
        }

        private async Task LoadUsersToHunt(List<UserToHunt> usersToHunt)
        {
            _usersToHunt = usersToHunt;
            List<UserToHuntGroup> groupedToList = await CreateGroupedUsersToHuntCollection(usersToHunt);
            UsersToHuntGrouped = null; 
            if (groupedToList != null)
            {
                UsersToHuntGrouped= new ObservableCollection<UserToHuntGroup>();
                foreach (UserToHuntGroup usersToHuntList in groupedToList)
                    if(usersToHuntList.Count>0)
                        UsersToHuntGrouped.Add(usersToHuntList);
            }
            UpdateHuntIndicator();
        }

        private async Task<List<UserToHuntGroup>> CreateGroupedUsersToHuntCollection(List<UserToHunt> usersToHunt)
        {
            int countUsersHunted = _userToHuntManager.GetCountUsersHunted();
            List<UserToHunt> usersHunted = await Task.Run(()=> usersToHunt.GetRange(0, countUsersHunted));
            List<UserToHunt> usersNotHuntedAlready = await Task.Run(() => usersToHunt.GetRange
                                                (countUsersHunted, usersToHunt.Count - countUsersHunted));

            List<UserToHuntGroup> groupedList = await Task.Run(()=>
                new List<UserToHuntGroup>() {new UserToHuntGroup(usersHunted) {Name="CAPTURADOS" },
                                             new UserToHuntGroup(usersNotHuntedAlready) {Name="PENDIENTES" }});

            return groupedList;
        }

        public async Task Logout()
        {
            bool accepted = await DisplayAlert(Constants.APP_NAME,
                                "Está seguro que desea salir?",
                                "Aceptar", "Cancelar");

            if (accepted)
            {
                IsInitialized = false;
                UsersToHuntGrouped = null;
                await _appUserManager.LogoutFromApplication();
                await _navigationService.NavigateToAsync<LoginViewModel>();

            }

        }

        public override async Task Refresh()
        {
            IsLoading = true;
            List<UserToHunt> updatedUsersToHunt = await _userToHuntManager.GetUsersToHuntUpdated(_usersToHunt);
            await LoadUsersToHunt(updatedUsersToHunt);
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

            if (!userToHunt.IsHunted)
            {
                IPictureTaker pictureTake = DependencyContainer.Container.Resolve<IPictureTaker>();

                string appUserExternalId = _appUserManager.GetLoggedAppUserExternalId();
                
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
