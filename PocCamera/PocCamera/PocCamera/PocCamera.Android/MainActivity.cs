using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Media;
using Xamarin.Forms;

[assembly: Dependency(typeof(PocCamera.Droid.MainActivity))]
namespace PocCamera.Droid
{
    [Activity(Label = "PocCamera", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IPictureTaker
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public void SnapPic()
        {
            var activity = Forms.Context as Activity;

            var picker = new MediaPicker(activity);
            var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
            {
                Name = "test.jpg",
                Directory = "WhoIs"
            });

            activity.StartActivityForResult(intent, 1);
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            if (resultCode.Equals(Result.Canceled))
                return;

            var mediaFile = await data.GetMediaFileExtraAsync(Forms.Context);
            System.Diagnostics.Debug.WriteLine(mediaFile.Path);
        }
    }
}

