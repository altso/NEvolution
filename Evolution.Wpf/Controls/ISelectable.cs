namespace Evolution.Wpf.Controls
{
    using System;

    public interface ISelectable
    {
        event EventHandler<EventArgs> Select;

        bool CanBeSelected { get; set; }
    }
}