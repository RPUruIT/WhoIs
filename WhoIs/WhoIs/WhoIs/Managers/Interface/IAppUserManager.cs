﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Managers.Interface
{
    public interface IAppUserManager
    {
        Task<List<AppUser>> GetSpecificUsersFromService();
        Task<List<AppUser>> GetSpecificUsersFromUsers(List<User> users);

        Task EnterToApplication(AppUser appUser);
        Task LogoutFromApplication();
        Task<bool> IsUserLogged();
        Task<AppUser> GetAndSetLoggedAppUser();
        Task<string> GetLoggedAppUserExternalId();
        

    }
}
