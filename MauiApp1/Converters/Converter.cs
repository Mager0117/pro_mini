using System;
using System.Globalization;


namespace MauiApp1.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                if (parameter is string paramStr && int.TryParse(paramStr, out int minCount))
                {
                    return count > minCount;
                }
                return count > 0; // ถ้าไม่มี `parameter` ให้ถือว่า minCount = 0
            }
            return false; // คืนค่า false ถ้า value ไม่ใช่ int
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
