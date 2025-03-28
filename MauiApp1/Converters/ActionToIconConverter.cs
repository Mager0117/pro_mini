using System.Globalization;
using MauiApp1.Model;

namespace MauiApp1.Converters
{
    public class ActionToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string action)
            {
                return action.ToLower() switch
                {
                    "enrolled" => "enrolled_icon.png",
                    "dropped" => "dropped_icon.png",
                    _ => "default_icon.png"
                };
            }
            return "default_icon.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}