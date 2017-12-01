﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Services.Interface;
using Unity;
using WhoIs.Models;
using Newtonsoft.Json;
using WhoIs.Repositories.Interface;
using System;

namespace WhoIs.Managers
{
    public class UserToHuntManager:IUserToHuntManager
    {

        private IUserToHuntRepository _userHuntedRepository;
        private IUserManager _userManager;
        private IAppUserManager _appUserManager;

        private int _usersToHunt;
        private int _usersHunted;

        public UserToHuntManager(IUserToHuntRepository userToHuntRepository, IUserManager userManager, IAppUserManager appUserManager)
        {
            _userHuntedRepository = userToHuntRepository;
            _userManager = userManager;
            _appUserManager = appUserManager;
        }

        /// <summary>
        ///  This method set _usersToHunt and _usersHunted and return the list of users to hunt
        /// </summary>
        public async Task<List<UserToHunt>> GetUsersToHunt(List<User> users)
        {
            List<UserToHunt> usersToHunt = null;

            if (users != null)
                usersToHunt = await this.GetSpecificUsersFromUsers(users);
            else
                usersToHunt = await this.GetSpecificUsersFromService();

            List<UserToHunt> usersHunted = await this.GetHuntedUsers();

            usersToHunt = MergeUsersToHuntWithUsersHunted(usersToHunt, usersHunted);

            _usersToHunt = usersToHunt.Count;
            _usersHunted = usersHunted.Count;

            return usersToHunt;
        }

        public async Task<int> GetCountUsersToHunt()
        {
            await Task.Delay(1);
            return _usersToHunt;
        }

        public async Task<int> GetCountUsersHunted()
        {
            await Task.Delay(1);
            return _usersHunted;
        }

        public async Task HuntUser(UserToHunt userToHunt)
        {
            await _userHuntedRepository.HuntUser(userToHunt);
        }

        private async Task<List<UserToHunt>> GetSpecificUsersFromService()
        {
            List<User> users = await _userManager.GetUsersFromService();

            return await GetSpecificUsersFromUsers(users);
        }

        private async Task<List<UserToHunt>> GetSpecificUsersFromUsers(List<User> users)
        {
            await Task.Delay(1);

            List<UserToHunt> appUsers = users.Select(u =>
                                                        new UserToHunt() {
                                                            ExternalId = u.ExternalId,
                                                            Name = u.Name,
                                                            Email = u.Email,
                                                                 
                                                        }).ToList();
            return appUsers;
        }

        private async Task<List<UserToHunt>> GetHuntedUsers()
        {
            string appUserExternalId = await _appUserManager.GetLoggedAppUserExternalId();
            return await _userHuntedRepository.GetHuntedUsers(appUserExternalId);
        }

        private List<UserToHunt> MergeUsersToHuntWithUsersHunted(List<UserToHunt> usersToHunt, List<UserToHunt> usersHunted)
        {
            List<UserToHunt> allUsersToHunt = usersHunted;

            usersToHunt.RemoveAll(u => allUsersToHunt.Contains(u));
            allUsersToHunt.AddRange(usersToHunt);

            return allUsersToHunt;
        }

       
    }
}
