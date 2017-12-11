using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;

namespace WhoIs.iOS
{
    public class PictureTaker_iOS : IPictureTaker
    {
        public async void SnapPic(string folder,string name)
        {
            /*var picker = new MediaPicker();
            
            var mediaFile = await picker.PickPhotoAsync();
            string imageFile = mediaFile.Path;

            string thumbnailImageFile = "";//TODO implement resize for iOS

            string[] imgFiles = { imageFile, thumbnailImageFile };
            MessagingCenter.Send<IPictureTaker, string[]>(this, "pictureTaken", imgFiles);*/
        }
    }
}