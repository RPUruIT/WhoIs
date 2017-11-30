using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Models
{
    public class AppUser : EntityBase
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

        public override string ToString()
        {
            return this.Name;
        }

        
    }
}
