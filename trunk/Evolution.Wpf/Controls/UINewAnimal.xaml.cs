namespace Evolution.Wpf.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for UINewAnimal.xaml
    /// </summary>
    public partial class UINewAnimal : ISelectable
    {
        public static readonly DependencyProperty CanBeSelectedProperty =
            DependencyProperty.Register("CanBeSelected", typeof(bool), typeof(UINewAnimal), new UIPropertyMetadata(false));

        public UINewAnimal()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> Select;

        public bool CanBeSelected
        {
            get { return (bool)this.GetValue(CanBeSelectedProperty); }
            set { this.SetValue(CanBeSelectedProperty, value); }
        }

        public void OnSelect(EventArgs e)
        {
            EventHandler<EventArgs> handler = this.Select;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void LayoutRootMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.OnSelect(e);
        }
    }
}
