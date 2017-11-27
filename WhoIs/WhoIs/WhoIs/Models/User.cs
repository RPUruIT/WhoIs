using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Models
{
    public class User:EntityModel
    {
        private string _name;

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _username;

        [JsonProperty("username")]
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _mail;

        [JsonProperty("mail")]
        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        private bool _deleted;

        [JsonProperty("deleted")]
        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

    }
}
