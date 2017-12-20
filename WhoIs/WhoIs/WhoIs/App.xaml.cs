using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Services.Interface;
using Xamarin.Forms;
using Unity;
using WhoIs.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace WhoIs
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitNavigation();
            
        }

        private Task InitNavigation()
        {
            var navigationService = DependencyContainer.Container.Resolve<INavigationService>();
            return navigationService.InitializeAsync();

        }

        protected override void OnStart()
        {
            AppCenter.Start("android=16a452a5-debd-400e-9b47-c171438ea93a;" + "uwp={Your UWP App secret here};" +
                   "ios={Your iOS App secret here}",
                   typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
