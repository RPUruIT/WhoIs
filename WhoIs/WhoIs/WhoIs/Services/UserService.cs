using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Configs;
using WhoIs.Models;
using WhoIs.Services.Interface;

namespace WhoIs.Services
{
    public class UserService : IUserService
    {
        IRequestProvider _requestProvider;
        public UserService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        public async Task<List<User>> GetAll()
        {
            return await _requestProvider.GetAsync<List<User>>(Constants.URL_SERVICE_GET_USER);
        }
    }
}
