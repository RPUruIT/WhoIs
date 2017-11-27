﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Repositories.Interface
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetLoggedUser();
    }
}
