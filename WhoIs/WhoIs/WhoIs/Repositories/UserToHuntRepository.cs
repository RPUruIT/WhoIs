using System;
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

        public async Task<List<UserToHunt>> GetHuntedUsers(string appUserExternalId)
        {
            List<UserToHunt> usersToHunt = await _database.GetAll();
            List<UserToHunt> usersToHuntFilterd = usersToHunt.
                                                Where(u => u.HunterId.Equals(appUserExternalId)).ToList();
            return usersToHuntFilterd;
        }

        public async Task<int> InsertHuntedUser(UserToHunt userToHunt)
        {
            return await _database.Insert(userToHunt);
        }
    }
}
