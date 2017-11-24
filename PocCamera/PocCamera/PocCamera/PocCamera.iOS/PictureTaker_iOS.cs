using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using Xamarin.Media;
using Xamarin.Forms;

namespace PocCamera.iOS
{
    public class PictureTaker_iOS : IPictureTaker
    {
        public async void SnapPic()
        {
            var picker = new MediaPicker();

            var mediaFile = await picker.PickPhotoAsync();

        }
    }
}