using System.Globalization;
using System.Windows.Data;

namespace EchoesOfTime.Converters;

public class IsActiveToContentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isActive)
        {
            return isActive ? "◼" : "▶"; // ◼ for active, ▶ for inactive
        }

        return "▶"; // Default value
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException(); // One-way binding only
    }
}
