using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Services.Interface;
using Unity;
using Newtonsoft.Json;

namespace WhoIs.Managers
{
    public class AppUserManager : IAppUserManager
    {
        public async Task<List<AppUser>> GetUsersFromService()
        {
            IService service = DependencyContainer.Container.Resolve<IService>();
            string allUsers = await service.GetUsers();
            List<AppUser> userForApplication = JsonConvert.DeserializeObject<List<AppUser>>(allUsers)
                                                .Where(u => !u.Deleted).ToList();

            return userForApplication;
        }

        public Task<AppUser> GetLoggedUser()
        {
            throw new NotImplementedException();
        }
    }
}
