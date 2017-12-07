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
using System.Threading.Tasks;
using WhoIs.Configs;
using Android.Graphics;
using Android.Media;

namespace WhoIs.Droid.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string> ResizeImage(string filePath)
        {

            int size = Constants.THUMBNAIL_SIZE;

            int lastIndex = filePath.LastIndexOf('.');
            string name = filePath.Substring(0, lastIndex);
            string extension = filePath.Substring(lastIndex + 1);

            string thumbnailImageFile = name + size + "x" + size + "." + extension;

            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InPreferredConfig = Bitmap.Config.Argb8888;
            Bitmap bitmap = await BitmapFactory.DecodeFileAsync(filePath, options);
            bitmap = await ThumbnailUtils.ExtractThumbnailAsync(bitmap, size, size);

            using (var os = new System.IO.FileStream(thumbnailImageFile, System.IO.FileMode.Create))
            {
                await bitmap.CompressAsync(Bitmap.CompressFormat.Png, 100, os);
            }


            return thumbnailImageFile;
        }
    }
}