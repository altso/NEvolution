namespace Evolution.Wpf.Controls
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ZIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var deck = values[1] as Deck;
            var card = values[0] as Card;
            if (deck != null && card != null)
            {
                return -deck.IndexOf(card);
            }

            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}