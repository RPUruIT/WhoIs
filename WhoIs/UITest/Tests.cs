using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            // //AppInitializer.StartApp(platform);
            app = ConfigureApp.Android
                .DeviceSerial("LGH811f5864ad3")
                .ApkFile(@"C:\Proyectos\WhoIs\WhoIs\WhoIs\WhoIs.Android\bin\Release\WhoIs.Android.apk")
                .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void NewTest()
        {
            var bounds = app.Query()[0].Rect;

            Task.Delay(4000).GetAwaiter().GetResult();

            app.Tap(x => x.Id("text1"));
            app.EnterText(x => x.Class("EditText"), "A");
            app.Tap(x => x.Text("Ingresar"));


            app.Tap(x => x.Text("Adrian Claveri").Index(2));
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.5f*bounds.Width, 0.9f *bounds.Height);
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.9f * bounds.Width, 0.9f * bounds.Height);
            app.Tap(x => x.Class("EditorEditText"));
            app.EnterText(x => x.Class("EditorEditText"), "nfkkffkkkfkfffff");
            app.Tap(x => x.Text("Confirmar"));

            //-----------------------------

            app.ScrollDownTo("Andres Baez");
            app.Tap(x => x.Text("Andres Baez").Index(1));
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.5f * bounds.Width, 0.9f * bounds.Height);
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.9f * bounds.Width, 0.9f * bounds.Height);
            app.Tap(x => x.Class("EditorEditText"));
            app.EnterText(x => x.Class("EditorEditText"), "hhjjj");
            app.Tap(x => x.Text("Confirmar"));

            //-----------------------------

            app.ScrollDownTo("Colo");
            app.Tap(x => x.Text("Colo").Index(1));
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.5f * bounds.Width, 0.9f * bounds.Height);
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.9f * bounds.Width, 0.9f * bounds.Height);
            app.Tap(x => x.Class("EditorEditText"));
            app.EnterText(x => x.Class("EditorEditText"), "jfkffkkff");
            app.Tap(x => x.Text("Confirmar"));

            //-----------------------------

            app.ScrollDownTo("Gonzalo Barbitta");
            app.Tap(x => x.Text("Gonzalo Barbitta").Index(1));
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.5f * bounds.Width, 0.9f * bounds.Height);
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.9f * bounds.Width, 0.9f * bounds.Height);
            app.Tap(x => x.Class("EditorEditText"));
            app.EnterText(x => x.Class("EditorEditText"), "jfnfjj");
            app.Tap(x => x.Text("Confirmar"));

            //-----------------------------

            app.ScrollDownTo("Joaquin Sosa");
            app.Tap(x => x.Text("Joaquin Sosa"));
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.5f * bounds.Width, 0.9f * bounds.Height);
            Task.Delay(2000).GetAwaiter().GetResult();
            app.TapCoordinates(0.9f * bounds.Width, 0.9f * bounds.Height);
            app.Tap(x => x.Class("EditorEditText"));
            app.EnterText(x => x.Class("EditorEditText"), "nfkfmm");
            app.Tap(x => x.Text("Confirmar"));
        }
    }
}

