using System;
using System.Globalization;
using System.Windows.Data;

namespace GoogleOptionsApi
{
    public class ExpirationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var e = (Expiration) value;
            var date = new DateTime(e.y.To<int>(), e.m.To<int>(), e.d.To<int>());
            return date.ToString("MMM dd, yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}