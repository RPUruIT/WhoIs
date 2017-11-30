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
    public class AppUserManager:IAppUserManager
    {
        IAppUserRepository _appUserRepository;
        IUserManager _userManager;

        public AppUserManager(IAppUserRepository appUserRepository,IUserManager userManager)   
        {
            _appUserRepository = appUserRepository;
            _userManager = userManager;
        }

        public async Task<List<AppUser>> GetSpecificUsersFromService()
        {
            List<User> users = await _userManager.GetUsersFromService();

            return await GetSpecificUsersFromUsers(users);
        }

        public async Task<List<AppUser>> GetSpecificUsersFromUsers(List<User> users)
        {
            await Task.Delay(1);

            List<AppUser> appUsers = users.Where(u => !u.Deleted)
                                                     .Select(u =>
                                                             new AppUser()
                                                             {
                                                                 ExternalId =u.ExternalId,
                                                                 Name=u.Name
                                                             }).ToList();
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
