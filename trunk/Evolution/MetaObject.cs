namespace Evolution
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using JetBrains.Annotations;

    /// <summary>
    /// Base type for extendable objects.
    /// </summary>
    public abstract class MetaObject : INotifyPropertyChanged
    {
        private readonly Dictionary<object, object> meta = new Dictionary<object, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        [CanBeNull]
        public virtual object this[object key]
        {
            get 
            { 
                object item;
                return this.meta.TryGetValue(key, out item) ? item : null;
            }

            set
            {
                this.meta[key] = value;
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, e);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}