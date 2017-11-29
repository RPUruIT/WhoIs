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
        IService _service;

        public UserManager(IService service)
        {
            _service = service;
        }

        public async Task<IList<User>> GetUsersFromService()
        {
            IList<User> users = await _service.GetUsers();

            return users;
        }

    }
}
