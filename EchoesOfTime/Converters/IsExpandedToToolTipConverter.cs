using System.Globalization;
using System.Windows.Data;

namespace EchoesOfTime.Converters;

public class IsExpandedToToolTipConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isExpanded)
        {
            return isExpanded ? "Collapse" : "Expand";
        }

        return "Expand"; // Default value
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException(); // One-way binding only
    }
}
