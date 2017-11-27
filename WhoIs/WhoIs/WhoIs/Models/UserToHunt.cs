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
        protected string ExternalId
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

        private bool _deleted;

        [Ignore]
        [JsonProperty("deleted")]
        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
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

    }
}
