using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Models.Interface
{
    public interface IUserConvertible
    {
        void fromUser(User user);
    }
}
