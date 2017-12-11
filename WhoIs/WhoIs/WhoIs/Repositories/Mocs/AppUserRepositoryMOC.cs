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
        AppUser _appUser;

        public AppUserRepositoryMOC()
        {
            //when is user logged
            //_appUser = new AppUser() { Id = 1, ExternalId = "abc1", Name = "Marcelo Lopez" };

        }
        public Task DeleteAppUser(AppUser appUser)
        {
            _appUser = null;
            return null;
        }

        public Task<AppUser> GetLoggedUser()
        {
            
            return Task.Run(() => _appUser);
        }

        public Task<bool> IsUserLogged()
        {
            
            return Task.Run(() => _appUser!=null);
        }

        public Task SaveAppUser(AppUser appUser)
        {
            _appUser = appUser;
            return null;
        }
    }
}
