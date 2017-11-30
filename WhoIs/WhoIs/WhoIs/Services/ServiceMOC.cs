using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Services.Interface;

namespace WhoIs.Services
{
    public class ServiceMOC : IService
    {
        public async Task<List<User>> GetUsers()
        {
            await Task.Delay(1);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(@"/OfflineResponses/users.json");

            return users;
        }
    }
}
