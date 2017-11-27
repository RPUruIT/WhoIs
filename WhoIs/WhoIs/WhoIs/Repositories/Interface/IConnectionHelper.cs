using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIs.Repositories.Interface
{
    public interface IConnectionHelper
    {
        SQLiteConnection GetConnection(string path);
    }
}
