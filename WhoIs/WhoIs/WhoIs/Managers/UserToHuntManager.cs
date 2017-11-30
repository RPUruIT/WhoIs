using System.Collections.Generic;
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

        private int _usersToHunt;
        private int _usersHunted;

        public UserToHuntManager(IUserToHuntRepository userToHuntRepository, IUserManager userManager)
        {
            _userHuntedRepository = userToHuntRepository;
            _userManager = userManager;
        }

        /// <summary>
        ///  This method set UsersToHunt and UsersHunted and return the list of users to hunt
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
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
            return await _userHuntedRepository.GetHuntedUsers();
        }

        private List<UserToHunt> MergeUsersToHuntWithUsersHunted(List<UserToHunt> usersToHunt, List<UserToHunt> usersHunted)
        {
            usersToHunt.RemoveAll(u => usersHunted.Contains(u));
            usersHunted.AddRange(usersToHunt);

            return usersToHunt;
        }


    }
}
