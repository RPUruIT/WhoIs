using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Services.Interface;
using Unity;
using WhoIs.Models;
using Newtonsoft.Json;

namespace WhoIs.Managers
{
    public class UserHuntedManager:IUserHuntedManager
    {
        public async Task<List<UserToHunt>> GetUsersFromService()
        {
            IService service = DependencyContainer.Container.Resolve<IService>();
            string allUsers = await service.GetUsers();

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
