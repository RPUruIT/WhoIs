using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PocCamera
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnOpenCamera_Clicked(object sender, EventArgs e)
        {
            IPictureTaker pictureTake = DependencyService.Get<IPictureTaker>();

            pictureTake.SnapPic();

            MessagingCenter.Subscribe<IPictureTaker, string>(this, "pictureTaken",
                                                            (s, arg) =>
                                                            {
                                                                imgTaken.Source = ImageSource.FromFile(arg);
                                                            });
        }
    }
}
