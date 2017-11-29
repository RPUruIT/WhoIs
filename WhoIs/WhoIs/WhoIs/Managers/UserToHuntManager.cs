using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Services.Interface;
using Unity;
using WhoIs.Models;
using Newtonsoft.Json;
using WhoIs.Repositories.Interface;
using System;

namespace WhoIs.Managers
{
    public class UserToHuntManager:UserManager<UserToHunt>,IUserToHuntManager
    {
        IUserToHuntRepository _userHuntedRepository;

        public UserToHuntManager(IService service,IUserToHuntRepository userHuntedRepository):base(service)
        {
            _userHuntedRepository = userHuntedRepository;
        }

        public async override Task<IList<UserToHunt>> GetSpecificUsersFromUsers(IList<User> users)
        {
            await Task.Delay(1);

            List<UserToHunt> appUsers = users.Where(u => !u.Deleted)
                                                     .Select(u =>
                                                             new UserToHunt(u)).ToList();
            return appUsers;
        }
    
    }
}
