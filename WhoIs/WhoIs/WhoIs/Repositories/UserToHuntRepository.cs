﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Repositories.Interface;

namespace WhoIs.Repositories
{
    public class UserToHuntRepository:IUserToHuntRepository
    {

        IDatabase<UserToHunt> _database;

        public UserToHuntRepository(IDatabase<UserToHunt> database)
        {
            _database = database;
        }

    }
}
