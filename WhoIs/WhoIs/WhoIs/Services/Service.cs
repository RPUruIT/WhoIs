using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Configs;
using WhoIs.Models;
using WhoIs.Services.Interface;


namespace WhoIs.Services
{
    public class Service : IService
    {
        HttpClient client;

        public Service()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> users;

            try
            {
                var uri = new Uri(Constants.URL_SERVICE_GET_USER);
                var jsonUsers = await client.GetStringAsync(uri);
                users = JsonConvert.DeserializeObject<List<User>>(jsonUsers);
            }
            catch(Exception ex)
            {
                users = null;
            }

            return users;
        }
    }
}
