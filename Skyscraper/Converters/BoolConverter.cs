using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Skyscraper.Converters
{
    class BoolConverter : IValueConverter
    {
        public object True { get; set; }
        public object False { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return (bool)value ? this.True : this.False;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == this.True || value == this.False ? value == this.True : DependencyProperty.UnsetValue;
        }
    }
}
