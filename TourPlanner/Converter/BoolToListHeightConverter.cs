using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TourPlanner.Converter
{
    public class BoolToListHeightConverter : IValueConverter
    {
        //
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null) return (bool) value ? 100 : 0;

            if (parameter is string)
            {
                //bool parameter is inverting the result
                if (bool.TryParse((string)parameter, out bool b))
                {
                    return !(bool)value ? 100 : 0;
                }
                //int parameter sets the height
                if (int.TryParse((string)parameter, out int i))
                {
                    return (bool)value ? i : 0;
                }
            }

            return (bool)value ? 100 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int) value > 0;
        }
    }
}
