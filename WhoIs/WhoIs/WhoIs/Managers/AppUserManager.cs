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

        private AppUser _appUser;

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
            List<AppUser> appUsers = await Task.Run(() => users.Select(u =>
                                                   new AppUser()
                                                   {
                                                       ExternalId = u.ExternalId,
                                                       Name = u.Name
                                                   }).ToList());
            return appUsers;
        }

        public async Task EnterToApplication(AppUser appUser)
        {
            await _appUserRepository.SaveAppUser(appUser);
            _appUser = appUser;
        }

        public async Task<bool> IsUserLogged()
        {
            return _appUser != null || await _appUserRepository.IsUserLogged();
        }

        public async Task<AppUser> GetAndSetLoggedAppUser()
        {
            if (_appUser == null)
            {
                AppUser appUser = await _appUserRepository.GetLoggedUser();
                _appUser = appUser;
            }

            return _appUser;
        }

        public string GetLoggedAppUserExternalId()
        {
            return _appUser.ExternalId;
        }
    }
}
