using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Services.Interface;
using Xamarin.Forms;
using Unity;
using WhoIs.Models;

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
            // Handle when your app starts
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
