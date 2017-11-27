﻿using System;
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

        public async Task<List<AppUser>> GetUsersFromService()
        {
            string allUsers = await _service.GetUsers();
            List<AppUser> userForApplication = JsonConvert.DeserializeObject<List<AppUser>>(allUsers)
                                                .Where(u => !u.Deleted).ToList();

            return userForApplication;
        }

        public async Task<AppUser> GetLoggedUser()
        {
            AppUser appUser = await _appUserRepository.GetLoggedUser();

            return appUser;
        }
    }
}
