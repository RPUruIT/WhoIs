using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Managers.Interface
{
    public interface IUserToHuntManager
    {
        Task<List<UserToHunt>> GetSpecificUsersFromService();
        Task<List<UserToHunt>> GetSpecificUsersFromUsers(List<User> users);
        Task<List<UserToHunt>> GetHuntedUsers();
        Task<List<UserToHunt>> GetUsersToHunt(List<User> users);
        Task<int> GetCountUsersHunted();
    }
}
