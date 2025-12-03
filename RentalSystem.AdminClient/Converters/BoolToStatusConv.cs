using System;
using System.Globalization;
using System.Windows.Data;

namespace RentalSystem.AdminClient.Converters
{
    public class BoolToStatusConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool banned = (bool)value;
            return banned ? "BANNED" : "ACTIVE";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}