using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Managers.Interface
{
    public interface IAppUserManager:IUserManager<AppUser>
    {
        Task<AppUser> GetLoggedAppUser();
        Task EnterToApplication(AppUser appUser);
        Task SetLoggedUser(AppUser user);


    }
}
