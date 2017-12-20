using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Services.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
    }
}
