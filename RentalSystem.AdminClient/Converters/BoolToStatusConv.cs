using System.Globalization;
using System.Windows.Data;

namespace RentalSystem.AdminClient.Converters
{
    /// <summary>
    /// Converts a boolean value into a human-readable user status string.
    /// </summary>
    /// <remarks>
    /// This converter is intended for use in XAML bindings where a boolean flag
    /// (typically <c>IsBanned</c>) must be displayed as a meaningful status label.
    ///
    /// True  → "BANNED"
    /// False → "ACTIVE"
    ///
    /// The conversion is one-way only; ConvertBack is intentionally not implemented
    /// because UI status text should not be edited to change application state.
    /// </remarks>
    public class BoolToStatusConv : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value into a status string.
        /// </summary>
        /// <param name="value">The source value from the binding (expected to be bool).</param>
        /// <param name="targetType">The target binding type (ignored).</param>
        /// <param name="parameter">Optional parameter (unused).</param>
        /// <param name="culture">Culture information (unused).</param>
        /// <returns>
        /// "BANNED" if the value is true; otherwise "ACTIVE".
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool banned = (bool)value;
            return banned ? "BANNED" : "ACTIVE";
        }

        /// <summary>
        /// Reverse conversion is not supported.
        /// </summary>
        /// <remarks>
        /// UI elements should not attempt to modify the underlying boolean value
        /// by editing the displayed status string.
        /// </remarks>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}