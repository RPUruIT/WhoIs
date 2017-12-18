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
            if (users == null)
                users = new List<User>();

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
            _appUser = appUser;//the appUser brings the Id value updated

        }

        public async Task LogoutFromApplication()
        {
            await _appUserRepository.DeleteAppUser(_appUser);
            _appUser = null;
        }

        public async Task<bool> IsUserLogged()
        {
            return _appUser != null || await _appUserRepository.IsUserLogged();
        }

        public async Task<AppUser> GetAndSetLoggedAppUser()
        {
            if (_appUser == null)
            {
                _appUser = await _appUserRepository.GetLoggedUser();
            }

            return _appUser;
        }

        public string GetLoggedAppUserExternalId()
        {
            string externalId = _appUser != null ? _appUser.ExternalId : "";
            return externalId;
        }
    }
}
