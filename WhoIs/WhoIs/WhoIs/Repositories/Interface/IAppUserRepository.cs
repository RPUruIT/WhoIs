using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Repositories.Interface
{
    public interface IAppUserRepository
    {
        Task DeleteAppUser(AppUser appUser);

        Task SaveAppUser(AppUser appUser);

        Task<AppUser> GetLoggedUser();

        Task<bool> IsUserLogged();
    }
}
