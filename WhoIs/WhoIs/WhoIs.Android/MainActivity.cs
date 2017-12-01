using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Media;
using Xamarin.Forms;
using Android.Media;
using Android.Graphics;
using System.IO;

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

            var picker = new MediaPicker(activity);
            var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
            {
                Name = name + ".jpg",
                Directory = "WhoIsUsersHunted/" + folder
            });

            activity.StartActivityForResult(intent, 1);
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            if (resultCode.Equals(Result.Canceled))
                return;

            var mediaFile = await data.GetMediaFileExtraAsync(Forms.Context);
            //try
            //{
            //    Bitmap bitmap = Bitmap.CreateBitmap(64, 64, Bitmap.Config.Argb8888);

            //    await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, 1, mediaFile.GetStream());

            //    await ThumbnailUtils.ExtractThumbnailAsync(bitmap, 64, 64);
            //}
            //catch (Exception ex)
            //{

            //}
            MessagingCenter.Send<IPictureTaker, string>(this, "pictureTaken", mediaFile.Path);
        }
    }
}

