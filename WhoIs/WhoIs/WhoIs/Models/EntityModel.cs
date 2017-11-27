using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Models
{
    public class EntityModel
    {
        private string _id;

        [JsonProperty("_id")]
        protected string Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
