using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Skyscraper.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BoolBrushConverter : IValueConverter
    {
        public Brush False { get; set; }
        public Brush True { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Brush)value == True || (Brush)value == False ? (bool?)((Brush)value == True) : null;
        }
    }
}
