namespace Evolution.Wpf.Controls
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BooleanToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value as bool? ?? false;
            return b ? 1d : 0d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}