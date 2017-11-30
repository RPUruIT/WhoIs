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
        IUserToHuntRepository _userHuntedRepository;
        IUserManager _userManager;

        public UserToHuntManager(IUserToHuntRepository userHuntedRepository, IUserManager userManager)
        {
            _userHuntedRepository = userHuntedRepository;
            _userManager = userManager;
        }

        public async Task<List<UserToHunt>> GetHuntedUsers()
        {
            return await _userHuntedRepository.GetHuntedUsers();
        }

        public async Task<List<UserToHunt>> GetSpecificUsersFromService()
        {
            List<User> users = await _userManager.GetUsersFromService();

            return await GetSpecificUsersFromUsers(users);
        }

        public async Task<List<UserToHunt>> GetSpecificUsersFromUsers(List<User> users)
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

        public async Task<List<UserToHunt>> GetUsersToHunt(List<User> users)
        {
            List<UserToHunt> usersToHunt = null;

            if (users != null)
                usersToHunt= await this.GetSpecificUsersFromUsers(users);
            else
                usersToHunt = await this.GetSpecificUsersFromService();

            List<UserToHunt> usersHunted = await this.GetHuntedUsers();

            return MergeUsersToHuntWithUsersHunted(usersToHunt, usersHunted);
        }

        private List<UserToHunt> MergeUsersToHuntWithUsersHunted(List<UserToHunt> usersToHunt, List<UserToHunt> usersHunted)
        {
            usersToHunt.RemoveAll(u => usersHunted.Contains(u));
            usersHunted.AddRange(usersToHunt);

            return usersToHunt;
        }
    }
}
