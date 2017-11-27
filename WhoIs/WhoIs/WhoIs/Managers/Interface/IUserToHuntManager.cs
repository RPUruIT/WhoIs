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
        Task<List<UserToHunt>> GetUsersFromService();

        List<UserToHunt> GetUsersFromJson(string jsonUsers);
    }
}
