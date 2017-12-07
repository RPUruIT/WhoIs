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
using Java.IO;
using static WhoIs.Droid.MainActivity;

namespace WhoIs.Droid.Helpers
{
    public static class FileHelper
    {
        public static void CreateFolderAtExternalStorage(string folder)
        {
            PicturesFiles._picturesDir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                   Android.OS.Environment.DirectoryPictures), folder);
            CreateIfNotExist(PicturesFiles._picturesDir);
        }

        public static File GetFolderInsideFolder(File parentFolder,string childFolder)
        {
            File dir = new File(parentFolder, childFolder);
            CreateIfNotExist(dir);
            return dir;
        }

        public static void CreateIfNotExist(File dir)
        {
            if (!dir.Exists())
            {
                dir.Mkdirs();
            }
        }
    }
}