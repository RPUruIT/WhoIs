using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Repositories.Interface;

namespace WhoIs.Repositories.Mocs
{
    public class UserToHuntRepositoryMOC : IUserToHuntRepository
    {
        List<UserToHunt> _huntedUsers;//this list has huntedUsers from many Appusers
        public UserToHuntRepositoryMOC()
        {
            _huntedUsers = new List<UserToHunt>();
        }

        private UserToHunt CreateUserHunted(int id, string externalId,string name)
        {
            UserToHunt userHunted = new UserToHunt();
            userHunted.Id = id;
            userHunted.ExternalId = externalId;
            userHunted.Name = name;
            userHunted.Email = "xxxx@gmail.com";
            userHunted.Comments = "Comentario";
            userHunted.ImgPath = "";
            userHunted.ImgThumbnailPath = "";
            userHunted.HunterId = "abc1";
            return userHunted;
        }

        public Task<List<UserToHunt>> GetHuntedUsers(string appUserExternalId)
        {
            List<UserToHunt> usersToHunt = _huntedUsers.Where(u => u.HunterId == appUserExternalId).ToList();
            return Task.Run(() => usersToHunt);
        }

        public Task<int> InsertHuntedUser(UserToHunt userToHunt)
        {
            _huntedUsers.Add(userToHunt);
            return Task.Run(() => 1);
        }
    }
}
