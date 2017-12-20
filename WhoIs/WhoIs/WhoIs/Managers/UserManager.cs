using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Services.Interface;

namespace WhoIs.Managers
{
    public class UserManager:IUserManager
    {
        IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<User>> GetUsersFromService()
        {
            List<User> users = await _userService.GetAll();
            users.RemoveAll(u => u.Deleted);
            users=users.OrderBy(u => u.Name).ToList();
            return users;
        }

    }
}
