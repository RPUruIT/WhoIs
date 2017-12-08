using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Repositories.Interface;

namespace WhoIs.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        IDatabase<AppUser> _database;

        public AppUserRepository(IDatabase<AppUser> database)
        {
            _database = database;
        }

        public async Task<AppUser> GetLoggedUser()
        {
            return await _database.GetFirst();
        }

        public async Task<bool> IsUserLogged()
        {
            return await _database.GetCount() > 0;
        }

        public async Task SaveAppUser(AppUser appUser)
        {
            await _database.Insert(appUser);
        }
    }
}
