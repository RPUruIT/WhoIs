using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;
using WhoIs.Repositories.Interface;
using System.IO;

namespace WhoIs.iOS
{
    public class ConnectionHelper : IConnectionHelper
    {
        public SQLiteConnection GetConnection(string path)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string fileFolder = Path.Combine(libFolder, path);

            return new SQLiteConnection(fileFolder);
        }
    }
}