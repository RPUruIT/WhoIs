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
using Unity;
using Unity.Lifetime;
using WhoIs.Configs;
using WhoIs.Droid.Mocs;
using WhoIs.Repositories.Interface;

namespace WhoIs.Droid
{
    public class AndroidDependencyContainer
    {
        public static void Initialize(IUnityContainer container)
        {
            container.RegisterSingleton<IConnectionHelper, ConnectionHelper>();
            if(Constants.IS_TEST)
                container.RegisterSingleton<IPictureTaker, PictureTakerMOC>();
            else
                container.RegisterSingleton<IPictureTaker, MainActivity>();
        }
    }
}