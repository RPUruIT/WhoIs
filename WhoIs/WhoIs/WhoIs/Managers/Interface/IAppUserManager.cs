using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Managers.Interface
{
    public interface IAppUserManager
    {
        Task<IList<AppUser>> GetSpecificUsersFromService();
        Task<IList<AppUser>> GetSpecificUsersFromUsers(IList<User> users);

        Task<AppUser> GetLoggedAppUser();
        Task EnterToApplication(AppUser appUser);
        Task SetLoggedUser(AppUser user);

    }
}
