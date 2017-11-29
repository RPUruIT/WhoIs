using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models.Interface;

namespace WhoIs.Models
{
    public class UserToHunt : EntityBase, IUserConvertible
    {
        public UserToHunt()
        {

        }

        public UserToHunt(User user)
        {
            fromUser(user);
        }
        public void fromUser(User user)
        {
            this.ExternalId = user.ExternalId;
            this.Name = user.Name;
            this.Email = user.Email;
        }

        private string _externaId;
        [JsonProperty("_id")]
        public string ExternalId
        {
            get { return _externaId; }
            set { _externaId = value; }
        }

        private string _name;
        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _email;
        [JsonProperty("mail")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _imgPath;
        public string ImgPath
        {
            get { return _imgPath; }
            set { _imgPath = value; }
        }

        private string _comments;
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        private string _hunterId;

        public string HunterId
        {
            get { return _hunterId; }
            set { _hunterId = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
