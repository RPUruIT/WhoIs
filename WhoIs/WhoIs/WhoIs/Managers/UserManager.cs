using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Services.Interface;
using Unity;
using WhoIs.Models;

namespace WhoIs.Managers
{
    public class UserManager:IUserManager
    {
        public async Task<List<User>> GetUsers()
        {
            IService service = DependencyContainer.Container.Resolve<IService>();
            List<User> allUsers = await service.GetUsers();
            List<User> userForApplication = allUsers.Where(u => !u.Deleted).ToList();

            return userForApplication;
        }

    }
}
