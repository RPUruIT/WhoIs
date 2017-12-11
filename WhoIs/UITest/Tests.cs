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

        [Test]
        public void TakePicture()
        {
            Login();

            app.ScrollDownTo("Iang Yim");
            app.Tap(x => x.Text("Iang Yim"));
            app.EnterText(x => x.Class("EditorEditText"), "ufuffufufu");
            app.Back();
            app.Tap(x => x.Text("Confirmar"));

            app.ScrollDownTo("Marcelo Lopez");
            app.Tap(x => x.Text("Marcelo Lopez"));
            app.EnterText(x => x.Class("EditorEditText"), "ufuffufufu");
            app.Back();
            app.Tap(x => x.Text("Confirmar"));
        }

    }
}

