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
        public UserToHunt()
        {

        }

        public UserToHunt(UserToHunt userToHunt)
        {
            this.Id = userToHunt.Id;
            this.ExternalId = userToHunt.ExternalId;
            this.Name = userToHunt.Name;
            this.Email = userToHunt.Email;
            this.ImgPath = userToHunt.ImgPath;
            this.ImgThumbnailPath = userToHunt.ImgThumbnailPath;
            this.Comments = userToHunt.Comments;
            this.HunterId = userToHunt.HunterId;
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

        private string _imgThumbnailPath;

        public string ImgThumbnailPath
        {
            get { return _imgThumbnailPath; }
            set { _imgThumbnailPath = value; }
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

        public bool IsHunted
        {
            get { return !String.IsNullOrEmpty(this.ImgPath); }
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
