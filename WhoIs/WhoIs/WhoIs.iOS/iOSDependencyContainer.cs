using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Unity;
using Unity.Lifetime;
using WhoIs.Repositories.Interface;

namespace WhoIs.iOS
{
    public class iOSDependencyContainer
    {

        public static void Initialize(IUnityContainer container)
        {
            container.RegisterType<IConnectionHelper, ConnectionHelper>(new ContainerControlledLifetimeManager());
        }
    }
}