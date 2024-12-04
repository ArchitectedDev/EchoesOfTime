using System.Globalization;
using System.Windows.Data;

namespace EchoesOfTime.Converters;

public class IsActiveToToolTipConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isActive)
        {
            return isActive ? "Stop Tracking" : "Start Tracking";
        }

        return "Start Tracking"; // Default value
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException(); // One-way binding only
    }
}
