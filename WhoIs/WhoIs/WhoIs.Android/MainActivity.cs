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

            CreatePicturesDirectory();

            LoadApplication(new App());
        }

        private void ResolveDependencies()
        {
            DependencyContainer.InitializeCore();
            AndroidDependencyContainer.Initialize(DependencyContainer.Container);
        }

        private void CreatePicturesDirectory()
        {
            PicturesFiles._picturesDir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                   Android.OS.Environment.DirectoryPictures), "WhoIS");
            CreateIfNotExist(PicturesFiles._picturesDir);
        }

        private File GetFolderInsidePictureDirectory(string folder)
        {
            File dir = new File(PicturesFiles._picturesDir, folder);
            CreateIfNotExist(dir);
            return dir;
        }

        private void CreateIfNotExist(File dir) {
            if (!dir.Exists())
            {
                dir.Mkdirs();
            }
        }

        public void SnapPic(string folder,string name)
        {
            var activity = Forms.Context as Activity;
            name +=".jpg";
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            PicturesFiles._file = new File(GetFolderInsidePictureDirectory(folder), name);
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(PicturesFiles._file));
            activity.StartActivityForResult(intent, 1);

        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            if (resultCode.Equals(Result.Canceled))
                return;

            string imageFile = PicturesFiles._file.Path;

            string thumbnailImageFile = await resizeImage(imageFile);

            string[] imgFiles = { imageFile, thumbnailImageFile };

            MessagingCenter.Send<IPictureTaker, string[]>(this, Constants.PICTURE_TAKER_EVENT_NAME, imgFiles);

        }

        private async Task<string> resizeImage(string filePath) {

            int size = Constants.THUMBNAIL_SIZE;

            int lastIndex = filePath.LastIndexOf('.');
            string name = filePath.Substring(0, lastIndex);
            string extension = filePath.Substring(lastIndex + 1);

            string thumbnailImageFile = name + size + "x"+ size + "."+ extension;

            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InPreferredConfig = Bitmap.Config.Argb8888;
            Bitmap bitmap = await BitmapFactory.DecodeFileAsync(filePath, options);
            bitmap = await ThumbnailUtils.ExtractThumbnailAsync(bitmap, size, size);

            using (var os = new System.IO.FileStream(thumbnailImageFile, System.IO.FileMode.Create))
            {
                await bitmap.CompressAsync(Bitmap.CompressFormat.Png, 100, os);
            }
            

            return thumbnailImageFile;
        }

        public static class PicturesFiles
        {
            public static File _file;
            public static File _picturesDir;
        }
    }
}

