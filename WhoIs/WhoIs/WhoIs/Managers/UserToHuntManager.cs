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
        IService _service;
        IUserToHuntRepository _userHuntedRepository;

        public UserToHuntManager(IService service,IUserToHuntRepository userHuntedRepository)
        {
            _service = service;
            _userHuntedRepository = userHuntedRepository;
        }

        public async Task<List<UserToHunt>> GetUsersToHuntFromService()
        {
            string allUsers = await _service.GetUsers();

            return await this.GetUsersToHuntFromJson(allUsers);
        }

        public async Task<List<UserToHunt>> GetUsersToHuntFromJson(string jsonUsers)
        {
            List<UserToHunt> userToHunt = null;
            try {
                userToHunt = await Task.Run(() => {
                    return JsonConvert.DeserializeObject<List<UserToHunt>>(jsonUsers)
                                               .Where(u => !u.Deleted).ToList();
                }); 
            }
            catch(Exception ex)
            {

            }
            return userToHunt;
        }

    }
}
