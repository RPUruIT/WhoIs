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
using WhoIs.Repositories.Interface;
using Unity.Lifetime;

namespace WhoIs.Droid
{
    public class AndroidDependencyContainer
    {
        public static void Initialize(IUnityContainer container)
        {
            container.RegisterSingleton<IConnectionHelper, ConnectionHelper>();
        }
    }
}