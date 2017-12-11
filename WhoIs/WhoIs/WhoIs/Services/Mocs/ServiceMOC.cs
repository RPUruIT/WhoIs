using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Services.Interface;

namespace WhoIs.Services.Mocs
{
    public class ServiceMOC : IService
    {
        public async Task<List<User>> GetUsers()
        {
            var assembly = typeof(Service).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WhoIs.Services.Mocs.OfflineResponses.users.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            List<User> users = await Task.Run(()=>JsonConvert.DeserializeObject<List<User>>(text));

            return users;
        }
    }
}
