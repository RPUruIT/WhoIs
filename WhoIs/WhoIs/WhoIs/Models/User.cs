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
        private int _name;

        [JsonProperty("name")]
        public int Name
        {
            get { return _name; }
            set { _name = value; }
        }


    }
}
