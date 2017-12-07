using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.ViewModels.Helper
{
    public class UserHuntedDetailsViewParameter
    {
        public UserToHunt UserToHunt { get; set; }
        public bool SeeDetails { get; set; }
    }
}
