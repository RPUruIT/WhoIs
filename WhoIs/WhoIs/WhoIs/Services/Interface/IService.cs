using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Services.Interface
{
    public interface IService
    {
        Task<List<User>> GetUsers();
    }
}
