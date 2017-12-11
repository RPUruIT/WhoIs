using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Repositories.Interface;

namespace WhoIs.Repositories.Mocs
{
    public class UserToHuntRepositoryMOC : IUserToHuntRepository
    {
        public Task<int> GetCountUsersHunted()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserToHunt>> GetHuntedUsers(string appUserExternalId)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertHuntedUser(UserToHunt userToHunt)
        {
            throw new NotImplementedException();
        }
    }
}
