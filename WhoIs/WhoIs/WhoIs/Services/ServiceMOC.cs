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
        public async Task<string> GetUsers()
        {
            await Task.Run(null);
            string users = JsonConvert.SerializeObject(@"/OfflineResponses/users.json");

            return users;
        }
    }
}
