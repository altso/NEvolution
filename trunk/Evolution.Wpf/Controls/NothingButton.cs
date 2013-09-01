namespace Evolution.Wpf.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class NothingButton : Button, ISelectable
    {
        public NothingButton()
        {
            this.Click += (_, e) =>
            {
                if (this.CanBeSelected)
                {
                    this.OnSelect(e);
                }
            };
        }

        public event EventHandler<EventArgs> Select;

        public bool CanBeSelected
        {
            get { return this.Visibility == Visibility.Visible; }
            set { this.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        protected virtual void OnSelect(EventArgs e)
        {
            var handler = this.Select;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}