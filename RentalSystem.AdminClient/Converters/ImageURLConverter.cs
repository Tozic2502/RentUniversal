using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace RentalSystem.AdminClient.Converters
{
    public class ImageUrlConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string url || string.IsNullOrWhiteSpace(url))
                return null;

            // Absolute URL für WPF bauen
            var fullUrl = url.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? url
                : $"http://localhost:8080{url}";

            return new BitmapImage(new Uri(fullUrl, UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ❗ Wird nicht benötigt → sauber abbrechen
            throw new NotSupportedException();
        }
    }
}