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
using WhoIs.Repositories.Interface;

namespace WhoIs.Managers
{
    public class AppUserManager:UserManager<AppUser>,IAppUserManager
    {
        IAppUserRepository _appUserRepository;

        public AppUserManager(IService service, IAppUserRepository appUserRepository):base(service)
        {
            _appUserRepository = appUserRepository;
        }

        public async override Task<IList<AppUser>> GetSpecificUsersFromUsers(IList<User> users)
        {
            await Task.Delay(1);

            List<AppUser> appUsers = users.Where(u => !u.Deleted)
                                                     .Select(u =>
                                                             new AppUser(u)).ToList();
            return appUsers;
        }

        public async Task<AppUser> GetLoggedAppUser()
        {
            AppUser appUser = await _appUserRepository.GetLoggedUser();

            return appUser;
        }

        public async Task EnterToApplication(AppUser appUser)
        {
            await _appUserRepository.SaveAppUser(appUser);
            await SetLoggedUser(appUser);
        }

        public async Task SetLoggedUser(AppUser user)
        {
            await Task.Delay(1);
            App.AppUser = user;
        }
    }
}
