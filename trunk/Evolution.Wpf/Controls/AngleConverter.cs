namespace Evolution.Wpf.Controls
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class AngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dualCard = value as DualCard;
            if (dualCard != null)
            {
                return dualCard.Trait != dualCard.Trait1 ? 180 : 0;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}