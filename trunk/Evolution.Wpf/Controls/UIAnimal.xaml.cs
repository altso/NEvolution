namespace Evolution.Wpf.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for UIAnimal.xaml
    /// </summary>
    public partial class UIAnimal : ISelectable
    {
        public static readonly DependencyProperty AnimalProperty =
            DependencyProperty.Register("Animal", typeof(Animal), typeof(UIAnimal), new UIPropertyMetadata(AnimalChangedCallback));

        public static readonly DependencyProperty CanBeSelectedProperty =
            DependencyProperty.Register("CanBeSelected", typeof(bool), typeof(UIAnimal), new UIPropertyMetadata(false));

        public UIAnimal()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> Select;

        public Animal Animal
        {
            get { return (Animal)GetValue(AnimalProperty); }
            set { SetValue(AnimalProperty, value); }
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

        private static void AnimalChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animal = (Animal)e.NewValue;
            animal[typeof(UIAnimal)] = d;
        }

        private void LayoutRootMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.CanBeSelected)
            {
                this.OnSelect(e);
            }
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
    }
}
