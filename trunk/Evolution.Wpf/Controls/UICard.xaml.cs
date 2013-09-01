namespace Evolution.Wpf.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for UICard.xaml
    /// </summary>
    public partial class UICard : ISelectable
    {
        public static readonly DependencyProperty CardProperty =
            DependencyProperty.Register("Card", typeof(Card), typeof(UICard), new UIPropertyMetadata(CardChangedCallback));

        public static readonly DependencyProperty IsDisplayedProperty =
            DependencyProperty.Register("IsDisplayed", typeof(bool), typeof(UICard), new UIPropertyMetadata(false, IsDisplayedChangedCallback));

        public static readonly DependencyProperty CanBeSelectedProperty =
            DependencyProperty.Register("CanBeSelected", typeof(bool), typeof(UICard), new UIPropertyMetadata(false));

        public UICard()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> Select;

        public Card Card
        {
            get { return (Card)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); }
        }

        public bool IsDisplayed
        {
            get { return (bool)GetValue(IsDisplayedProperty); }
            set { SetValue(IsDisplayedProperty, value); }
        }

        public bool CanBeSelected
        {
            get { return (bool)this.GetValue(CanBeSelectedProperty); }
            set { this.SetValue(CanBeSelectedProperty, value); }
        }

        protected virtual void OnSelect(EventArgs e)
        {
            var handler = this.Select;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private static void CardChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = (Card)e.NewValue;
            card[typeof(UICard)] = d;
        }

        // todo: rewrite as value converter
        private static void IsDisplayedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = (UICard)d;

            string key = "evolution";

            if (card.Card != null && (bool)e.NewValue)
            {
                key = string.Format("evolution-{0}", card.Card.Trait.GetType().Name.ToLowerInvariant());

                var dualCard = card.Card as DualCard;
                if (dualCard != null)
                {
                    key = string.Format("evolution-{0}-{1}", dualCard.Trait1.GetType().Name.ToLowerInvariant(), dualCard.Trait2.GetType().Name.ToLowerInvariant());
                }
            }

            card.FaceImage.Source = (ImageSource)(Application.Current.Resources[key] ?? Application.Current.Resources["any"]);
        }

        private void GridMouseEnter(object sender, MouseEventArgs e)
        {
            if (this.CanBeSelected)
            {
                HoverBorder.Visibility = Visibility.Visible;
            }
        }

        private void GridMouseLeave(object sender, MouseEventArgs e)
        {
            HoverBorder.Visibility = Visibility.Collapsed;
        }

        private void FlipClick(object sender, RoutedEventArgs e)
        {
            this.IsDisplayed = !this.IsDisplayed;
        }

        private void TurnClick(object sender, RoutedEventArgs e)
        {
            var dualCard = this.Card as DualCard;
            if (dualCard != null)
            {
                dualCard.Turn();
                FaceImageRotateTransform.Angle = (FaceImageRotateTransform.Angle + 180) % 360;
            }
        }

        private void LayoutRootMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.CanBeSelected)
            {
                this.OnSelect(e);
            }
        }
    }
}
