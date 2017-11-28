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
        Task<Object[]> GetUsersFromService();

        Task<AppUser> GetLoggedUser();

        Task EnterToApplication(AppUser appUser);
    }
}
