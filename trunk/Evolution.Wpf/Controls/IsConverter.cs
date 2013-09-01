namespace Evolution.Wpf.Controls
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class IsConverter : IValueConverter
    {
        public Type Type { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Type.IsInstanceOfType(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}