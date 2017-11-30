using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Media;
using Xamarin.Forms;

[assembly: Dependency(typeof(WhoIs.iOS.PictureTaker_iOS))]
namespace WhoIs.iOS
{
    public class PictureTaker_iOS : IPictureTaker
    {
        public async void SnapPic(string folder,string name)
        {
            var picker = new MediaPicker();
            
            var mediaFile = await picker.PickPhotoAsync();

            MessagingCenter.Send<IPictureTaker, string>(this, "pictureTaken", mediaFile.Path);
        }
    }
}