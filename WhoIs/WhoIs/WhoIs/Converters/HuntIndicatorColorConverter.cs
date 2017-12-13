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
    public class HuntIndicatorColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Red;
            if (value != null) { 
                string indicator = value as string;
                string[] indicatorSplited = indicator.Split('/');

                if (indicatorSplited.Length == 2)
                {
                    double countHunted = Int64.Parse(indicatorSplited[0]);
                    double totalCount = Int64.Parse(indicatorSplited[1]);

                    double percentaje = countHunted / totalCount;

                    color = percentaje < 0.3 ? 
                        Constants.HUNTERINDICATOR_COLOR_RED : percentaje < 0.7 ? 
                        Constants.HUNTERINDICATOR_COLOR_YELLOW : Constants.HUNTERINDICATOR_COLOR_GREEN;
                }
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
