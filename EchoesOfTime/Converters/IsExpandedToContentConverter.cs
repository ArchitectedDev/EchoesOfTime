using System.Globalization;
using System.Windows.Data;

namespace EchoesOfTime.Converters;

internal class IsExpandedToContentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isExpanded)
        {
            return isExpanded ? "⏫" : "⏬"; // ⏫ for expanded, ⏬ for collapsed
        }

        return "⏬"; // Default value
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException(); // One-way binding only
    }
}