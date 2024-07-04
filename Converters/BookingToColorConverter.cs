using SkipManagement.Model;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SkipManagement.Converters
{
    public class BookingToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Booking booking = (Booking)value;
            if (booking.Status != null && booking.Status.StatusName.ToLower().Equals("cancelled")) return new SolidColorBrush(Colors.Yellow);
            if (booking.Status != null && booking.Status.StatusName.ToLower().Equals("done")) return new SolidColorBrush(Colors.Green);
            if (booking.Countdown is int _int && _int <= 1) return new SolidColorBrush(Colors.Red);
            return new SolidColorBrush(Colors.Blue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
