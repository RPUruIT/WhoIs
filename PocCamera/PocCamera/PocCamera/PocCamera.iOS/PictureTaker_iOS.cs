using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using PocCamera;
using Xamarin.Media;
using Xamarin.Forms;

[assembly: Dependency(typeof(PocCamera.iOS.PictureTaker_iOS))]
namespace PocCamera.iOS
{
    public class PictureTaker_iOS : IPictureTaker
    {
        public async void SnapPic()
        {
            var picker = new MediaPicker();

            var mediaFile = await picker.PickPhotoAsync();
            System.Diagnostics.Debug.WriteLine(mediaFile.Path);

            MessagingCenter.Send<IPictureTaker, string>(this, "pictureTaken", mediaFile.Path);

        }
    }
}