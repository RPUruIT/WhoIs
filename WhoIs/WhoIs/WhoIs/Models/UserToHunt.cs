using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Models
{
    public class UserToHunt : EntityBase
    {

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
            get { return "ic_default_image.png"; }
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

        public override bool Equals(object obj)
        {
            UserToHunt userToCompare = obj as UserToHunt;
            if (userToCompare==null)
                return false;

            return ExternalId.Equals(userToCompare.ExternalId);
        }
        public override int GetHashCode()
        {
            return this.ExternalId.GetHashCode();
        }
    }
}
