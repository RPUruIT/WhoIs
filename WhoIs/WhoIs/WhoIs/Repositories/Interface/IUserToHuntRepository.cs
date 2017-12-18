using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Repositories.Interface
{
    public interface IUserToHuntRepository
    {
        Task<List<UserToHunt>> GetHuntedUsers(string appUserExternalId);
        Task<int> InsertHuntedUser(UserToHunt userToHunt);
    }
}
