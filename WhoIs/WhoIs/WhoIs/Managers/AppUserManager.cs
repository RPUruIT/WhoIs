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

namespace WhoIs.Managers
{
    public class AppUserManager : IAppUserManager
    {

        IService _service;
        IAppUserManager _appUserManager;

        public AppUserManager(IService service, IAppUserManager appUserManager)
        {
            _service = service;
            _appUserManager = appUserManager;
        }

        public async Task<List<AppUser>> GetUsersFromService()
        {
            string allUsers = await _service.GetUsers();
            List<AppUser> userForApplication = JsonConvert.DeserializeObject<List<AppUser>>(allUsers)
                                                .Where(u => !u.Deleted).ToList();

            return userForApplication;
        }

        public async Task<AppUser> GetLoggedUser()
        {
            AppUser appUser = await _appUserManager.GetLoggedUser();

            return appUser;
        }
    }
}
