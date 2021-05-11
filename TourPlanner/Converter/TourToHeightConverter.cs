using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TourPlanner.Models;

namespace TourPlanner.Converter
{
    public class TourToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                if (parameter is string)
                {
                    //bool parameter is inverting the result
                    if (bool.TryParse((string)parameter, out bool b))
                    {
                        return (!(value == null || string.IsNullOrWhiteSpace(((Tour)value).Name))) ? 0 : 500;
                    }
                }
            }
            return (value == null || string.IsNullOrWhiteSpace(((Tour) value).Name)) ? 0 : 500;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
