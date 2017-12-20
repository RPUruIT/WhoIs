using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Models
{
    public class UserToHuntGroup: ObservableCollection<UserToHunt>
    {
        public string Name { get; set; }

        public UserToHuntGroup(List<UserToHunt> users)
        {
            if(users!=null)
                foreach(UserToHunt userToHunt in users)
                    Items.Add(userToHunt);
        }

    }
}
