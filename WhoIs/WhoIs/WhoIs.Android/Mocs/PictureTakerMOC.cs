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
using Xamarin.Forms;
using WhoIs.Configs;

namespace WhoIs.Droid.Mocs
{
    public class PictureTakerMOC : IPictureTaker
    {
        public void SnapPic(string folder, string name)
        {
            string imageFile = "lopez.jpg";

            string thumbnailImageFile = "lopez64x64.jpg";

            string[] imgFiles = { imageFile, thumbnailImageFile };

            MessagingCenter.Send<IPictureTaker, string[]>(this, Constants.PICTURE_TAKER_EVENT_NAME, imgFiles);

        }
    }
}