using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Models.Interface;
using WhoIs.Services.Interface;

namespace WhoIs.Managers
{
    public abstract class UserManager<T> : IUserManager<T> where T :IUserConvertible 
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

        public async Task<IList<T>> GetSpecificUsersFromService()
        {
            IList<User> allUsers = await GetUsersFromService();


            return await GetSpecificUsersFromUsers(allUsers);
        }

        public abstract Task<IList<T>> GetSpecificUsersFromUsers(IList<User> users);
    }
}
