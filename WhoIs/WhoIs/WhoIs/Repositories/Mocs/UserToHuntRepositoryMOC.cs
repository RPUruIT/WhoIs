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
        List<UserToHunt> _huntedUsers;
        public UserToHuntRepositoryMOC()
        {
            _huntedUsers = new List<UserToHunt>();
            _huntedUsers.Add(CreateUserHunted(1, "abc2", "Iang Yim"));
        }

        private UserToHunt CreateUserHunted(int id, string externalId,string nombre)
        {
            UserToHunt userHunted_1 = new UserToHunt();
            userHunted_1.Id = id;
            userHunted_1.ExternalId = externalId;
            userHunted_1.Name = nombre;
            userHunted_1.Email = "xxxx@gmail.com";
            userHunted_1.Comments = "Comentario";         
            userHunted_1.ImgPath = "";
            userHunted_1.ImgThumbnailPath = "";
            userHunted_1.HunterId = "abc1";
            return userHunted_1;
        }

        public Task<int> GetCountUsersHunted()
        {
            return Task.Run(() => _huntedUsers.Count);
        }

        public Task<List<UserToHunt>> GetHuntedUsers(string appUserExternalId)
        {
            return Task.Run(() => _huntedUsers);
        }

        public Task<int> InsertHuntedUser(UserToHunt userToHunt)
        {

            _huntedUsers.Add(userToHunt);
            return Task.Run(() => 1);
        }
    }
}
