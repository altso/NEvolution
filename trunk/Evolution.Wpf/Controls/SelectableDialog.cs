namespace Evolution.Wpf.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Threading;
    using JetBrains.Annotations;

    public class SelectableDialog
    {
        public event EventHandler<EventArgs> Selecting;
        
        public event EventHandler<EventArgs> Selected;

        [CanBeNull]
        public TSelectable Select<TSelectable>(IEnumerable<TSelectable> elements, ISelectable cancelElement = null)
            where TSelectable : class, ISelectable
        {
            this.OnSelecting(EventArgs.Empty);

            var list = elements.ToList();
            if (list.Count == 0)
            {
                this.OnSelected(EventArgs.Empty);
                return null;
            }

            var dispatcherFrame = new DispatcherFrame();
            TSelectable result = null;
            EventHandler<EventArgs> select =
                (sender, e) =>
                {
                    dispatcherFrame.Continue = false;
                    result = (TSelectable)sender;
                };

            foreach (var element in list)
            {
                element.CanBeSelected = true;
                element.Select += select;
            }

            EventHandler<EventArgs> cancel =
                (sender, e) =>
                {
                    dispatcherFrame.Continue = false;
                    result = null;
                };
            if (cancelElement != null)
            {
                cancelElement.CanBeSelected = true;
                cancelElement.Select += cancel;
            }

            Dispatcher.PushFrame(dispatcherFrame);

            foreach (var element in list)
            {
                element.CanBeSelected = false;
                element.Select -= select;
            }

            if (cancelElement != null)
            {
                cancelElement.CanBeSelected = false;
                cancelElement.Select -= cancel;
            }

            this.OnSelected(EventArgs.Empty);

            return result;
        }

        protected virtual void OnSelecting(EventArgs e)
        {
            EventHandler<EventArgs> handler = this.Selecting;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSelected(EventArgs e)
        {
            EventHandler<EventArgs> handler = this.Selected;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}