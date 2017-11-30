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

        public async Task<IList<UserToHunt>> GetSpecificUsersFromService()
        {
            IList<User> users = await _userManager.GetUsersFromService();

            return await GetSpecificUsersFromUsers(users);
        }

        public async Task<IList<UserToHunt>> GetSpecificUsersFromUsers(IList<User> users)
        {
            await Task.Delay(1);

            List<UserToHunt> appUsers = users.Where(u => !u.Deleted)
                                                     .Select(u =>
                                                             new UserToHunt() {
                                                                 ExternalId = u.ExternalId,
                                                                 Name = u.Name,
                                                                 Email = u.Email,
                                                                 
                                                             }).ToList();
            return appUsers;
        }


    }
}
