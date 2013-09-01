namespace Evolution.Wpf.Controls
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class FoodTokenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BankToken)
            {
                return Application.Current.Resources["evolution-banktoken"];
            }

            if (value is ExtraToken)
            {
                return Application.Current.Resources["evolution-extratoken"];
            }

            if (value is FatToken)
            {
                return Application.Current.Resources["evolution-fattoken"];
            }

            throw new ArgumentOutOfRangeException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}