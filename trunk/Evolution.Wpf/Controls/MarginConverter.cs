namespace Evolution.Wpf.Controls
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class MarginConverter : IMultiValueConverter
    {
        public double VerticalSpacing { get; set; }

        public double HorizontalSpacing { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var deck = values[1] as Deck;
            var card = values[0] as Card;
            if (deck != null && card != null)
            {
                int index = deck.IndexOf(card);
                return new Thickness(index * this.HorizontalSpacing, 0, 0, index * this.VerticalSpacing);
            }

            return new Thickness();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}