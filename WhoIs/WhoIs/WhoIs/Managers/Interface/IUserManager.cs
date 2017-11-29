using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Models.Interface;

namespace WhoIs.Managers.Interface
{
    public interface IUserManager<T> where T:IUserConvertible
    { 
        Task<IList<User>> GetUsersFromService();

        Task<IList<T>> GetSpecificUsersFromService();
        Task<IList<T>> GetSpecificUsersFromUsers(IList<User> users);
    }
}
