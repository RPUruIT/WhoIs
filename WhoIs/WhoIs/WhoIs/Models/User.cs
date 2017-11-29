using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Models
{
    public class User
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

        private bool _deleted;

        [JsonProperty("deleted")]
        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

    }
}
