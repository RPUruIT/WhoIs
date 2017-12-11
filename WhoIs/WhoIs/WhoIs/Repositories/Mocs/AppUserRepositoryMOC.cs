using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Repositories.Interface;

namespace WhoIs.Repositories.Mocs
{
    public class AppUserRepositoryMOC : IAppUserRepository
    {
        public Task DeleteAppUser(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetLoggedUser()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserLogged()
        {
            throw new NotImplementedException();
        }

        public Task SaveAppUser(AppUser appUser)
        {
            throw new NotImplementedException();
        }
    }
}
