using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using WhoIs.Repositories.Interface;
using System.IO;


namespace WhoIs.Droid
{
    public class ConnectionHelper : IConnectionHelper
    {
        public SQLiteConnection GetConnection(string path)
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Directory.CreateDirectory(folder);
            string fileFolder = Path.Combine(folder, path);

            return new SQLiteConnection(fileFolder);
        }
    }
}