namespace Evolution.Wpf.Controls
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Traits;

    public class UsableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var usable = value as IUsable;
            return usable != null ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}