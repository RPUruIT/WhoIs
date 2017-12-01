using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Models
{
    public class EntityBase
    {
        private int _id;

        [PrimaryKey, AutoIncrement]
        [JsonIgnore]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
