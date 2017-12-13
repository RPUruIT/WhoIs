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
            app = ConfigureApp.Android
                .DeviceSerial("LGH811f5864ad3")
                .DeviceSerial("5203756cee7d8413")
                .ApkFile(@"C:\Proyectos\WhoIs\WhoIs\WhoIs\WhoIs.Android\bin\Release\WhoIs.Android.apk")
                .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void Login()
        {
            app.Tap(x => x.Class("EditText"));
            app.Tap(x => x.Id("text1"));
            app.Tap(x => x.Text("Ingresar"));
        }

        //This test use a moc which return a image taken with the app 
        //by Andres Calaveri
        //To Andres Calaveri
        [Test]
        public void TakePicture()
        {
            Login();

            Task.Delay(2000).GetAwaiter().GetResult();

            app.ScrollDownTo("Marcelo Lopez");
            app.Tap(x => x.Text("Marcelo Lopez"));
            app.EnterText(x => x.Class("EditorEditText"), "ufuffufufu");
            app.Back();
            app.Tap(x => x.Text("Confirmar"));

            Task.Delay(2000).GetAwaiter().GetResult();

            app.ScrollDownTo("Iang Yim");
            app.Tap(x => x.Text("Iang Yim"));
            app.EnterText(x => x.Class("EditorEditText"), "ufuffufufu");
            app.Back();
            app.Tap(x => x.Text("Confirmar"));

            Task.Delay(2000).GetAwaiter().GetResult();

            app.ScrollDownTo("Matias Delgado");
            app.Tap(x => x.Text("Matias Delgado"));
            app.EnterText(x => x.Class("EditorEditText"), "ufuffufufu");
            app.Back();
            app.Tap(x => x.Text("Confirmar"));
        }

    }
}

