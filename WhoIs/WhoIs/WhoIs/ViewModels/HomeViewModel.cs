﻿using System;
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
using WhoIs.Helper;

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
            set { _totalUsersToHunt = value; HuntIndicator = ""; }
        }
        private int _countUsersHunted;
        public int CountUsersHunted
        {
            get { return _countUsersHunted; }
            set { _countUsersHunted = value; HuntIndicator = ""; }
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
                await _appUserManager.GetAndSetLoggedAppUser();//THIS BETTER TO BE IN NAVIGATION PAGE BEFORE LOAD HOMEVIEW BUT IT THROWS AN EXCEPTION
                List<UserToHunt> usersToHunt = await _userToHuntManager.GetUsersToHunt(navigationData as List<User>);

                UsersToHunt = new ObservableCollection<UserToHunt>();
                foreach (UserToHunt user in usersToHunt)
                    UsersToHunt.Add(user);

                TotalUsersToHunt = await _userToHuntManager.GetCountUsersToHunt();
                CountUsersHunted = await _userToHuntManager.GetCountUsersHunted();
            }
            catch (Exception ex)
            {

            }

        }

        public async void UserToHuntSelected(UserToHunt userToHunt)
        {
            if (!userToHunt.HasImage())
            {
                IPictureTaker pictureTake = DependencyService.Get<IPictureTaker>();
                string appUserExternalId = await _appUserManager.GetLoggedAppUserExternalId();
                
                MessagingCenter.Subscribe<IPictureTaker, string[]>(this, "pictureTaken", (s, imageFiles) =>
                {
                    MessagingCenter.Unsubscribe<IPictureTaker, string[]>(this, "pictureTaken");
                    userToHunt.ImgPath = imageFiles[0];
                    userToHunt.ImgThumbnailPath = imageFiles[1];
                    userToHunt.HunterId = appUserExternalId;
                    _userToHuntManager.HuntUser(userToHunt);
                    
                });

                pictureTake.SnapPic(appUserExternalId, userToHunt.Name);

            }
            else
            {

            }
        }


    }
}
