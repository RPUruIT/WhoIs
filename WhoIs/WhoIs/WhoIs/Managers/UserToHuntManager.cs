using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Services.Interface;
using Unity;
using WhoIs.Models;
using Newtonsoft.Json;
using WhoIs.Repositories.Interface;

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

        public async Task<List<UserToHunt>> GetUsersFromService()
        {
            string allUsers = await _service.GetUsers();

            return this.GetUsersFromJson(allUsers);
        }

        public List<UserToHunt> GetUsersFromJson(string jsonUsers)
        {

            List<UserToHunt> userForApplication = JsonConvert.DeserializeObject<List<UserToHunt>>(jsonUsers)
                                                .Where(u => !u.Deleted).ToList();

            return userForApplication;
        }

    }
}
