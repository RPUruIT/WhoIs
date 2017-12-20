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
        Task<List<UserToHunt>> GetUsersToHunt(List<User> users = null);
        Task<List<UserToHunt>> GetUsersToHuntUpdated(List<UserToHunt> usersToHunt);
        int GetCountUsersToHunt();
        int GetCountUsersHunted();
        Task HuntUser(UserToHunt userToHunt);
        Task<List<UserToHunt>> FilterUserToHuntByName(List<UserToHunt> usersToHunt,string name);

    }
}
