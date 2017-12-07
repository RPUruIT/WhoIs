using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Configs;
using Xamarin.Forms;

namespace WhoIs.Converters
{
    public class UserToHuntImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imgPath = value as string;
            return String.IsNullOrEmpty(imgPath) ? ResourcesName.IMG_USER : imgPath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
