namespace Evolution.Wpf.Controls
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Evolution.Traits;

    public class TapableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var trait = value as TapableTrait;
            return trait != null && trait.IsTapped ? 15 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}