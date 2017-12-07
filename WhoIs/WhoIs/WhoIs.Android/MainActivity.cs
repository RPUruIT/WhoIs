using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Android.Media;
using Android.Graphics;
using Java.IO;
using System.Threading.Tasks;
using WhoIs.Configs;
using Android.Content;
using Android.Provider;
using System.Collections.Generic;
using WhoIs.Droid.Helpers;

[assembly: Dependency(typeof(WhoIs.Droid.MainActivity))]
namespace WhoIs.Droid
{
    [Activity(Label = "WhoIs", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IPictureTaker
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ResolveDependencies();

            FileHelper.CreateFolderAtExternalStorage(Constants.APP_NAME);

            LoadApplication(new App());
        }

        private void ResolveDependencies()
        {
            DependencyContainer.InitializeCore();
            AndroidDependencyContainer.Initialize(DependencyContainer.Container);
        }

        public void SnapPic(string folder,string name)
        {
            var activity = Forms.Context as Activity;
            name +=".jpg";
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            PicturesFiles._file = new File(FileHelper.GetFolderInsideFolder(PicturesFiles._picturesDir,folder), name);
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(PicturesFiles._file));
            activity.StartActivityForResult(intent, 1);

        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            if (resultCode.Equals(Result.Canceled))
                return;

            string imageFile = PicturesFiles._file.Path;

            string thumbnailImageFile = await ImageHelper.ResizeImage(imageFile, Constants.THUMBNAIL_SIZE);

            string[] imgFiles = { imageFile, thumbnailImageFile };

            MessagingCenter.Send<IPictureTaker, string[]>(this, Constants.PICTURE_TAKER_EVENT_NAME, imgFiles);

        }

        public static class PicturesFiles
        {
            public static File _file;
            public static File _picturesDir;
        }
    }
}

