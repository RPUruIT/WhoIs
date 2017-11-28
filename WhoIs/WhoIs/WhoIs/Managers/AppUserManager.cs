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
    public class AppUserManager : IAppUserManager
    {

        IService _service;
        IAppUserRepository _appUserRepository;

        public AppUserManager(IService service, IAppUserRepository appUserRepository)
        {
            _service = service;
            _appUserRepository = appUserRepository;
        }

        public async Task<Object[]> GetAppUsersFromService()
        {
            string allUsers = await _service.GetUsers();
            List<AppUser> userForApplication = JsonConvert.DeserializeObject<List<AppUser>>(allUsers)
                                                .Where(u => !u.Deleted).ToList();
       
            return new Object[2] { userForApplication, allUsers };
        }

        public async Task<AppUser> GetLoggedAppUser()
        {
            AppUser appUser = await _appUserRepository.GetLoggedUser();

            return appUser;
        }

        public async Task EnterToApplication(AppUser appUser)
        {
            await _appUserRepository.SaveAppUser(appUser);
        }
    }
}
