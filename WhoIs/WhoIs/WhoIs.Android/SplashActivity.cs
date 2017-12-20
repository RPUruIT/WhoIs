using System;

using Android.App;

using Android.OS;
using Xamarin.Forms;

using System.Threading.Tasks;

using Android.Content;

[assembly: Dependency(typeof(WhoIs.Droid.SplashActivity))]
namespace WhoIs.Droid
{
    [Activity(Label = "WhoIs", MainLauncher = true, Theme = "@style/MyTheme.Splash", Icon = "@drawable/icon", NoHistory = true)]
    public class SplashActivity : Activity
    {

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {

            await Task.Delay(4000); // Simulate a bit of startup work.
                
            StartActivity(new Intent(this, typeof(MainActivity)));
  
        }

        public override void OnBackPressed() { }
    }
}