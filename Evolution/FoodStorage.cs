namespace Evolution
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    public class FoodStorage : ICollection<FoodToken>, INotifyPropertyChanged, INotifyCollectionChanged, IFoodSource
    {
        public FoodStorage()
            : this(Enumerable.Empty<FoodToken>())
        {
        }

        public FoodStorage(IEnumerable<FoodToken> tokens)
        {
            this.InnerCollection = new ObservableCollection<FoodToken>(tokens);
            this.InnerCollection.CollectionChanged += (_, e) => this.OnCollectionChanged(e);
            ((INotifyPropertyChanged)this.InnerCollection).PropertyChanged += (_, e) => this.OnPropertyChanged(e);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Count
        {
            get { return this.InnerCollection.Count; }
        }

        bool ICollection<FoodToken>.IsReadOnly
        {
            get { return true; }
        }

        protected ObservableCollection<FoodToken> InnerCollection { get; private set; }

        public virtual FoodToken TakeOne()
        {
            if (this.InnerCollection.Count <= 0)
            {
                throw new InvalidOperationException();
            }

            var food = this.InnerCollection[this.InnerCollection.Count - 1];
            this.InnerCollection.RemoveAt(this.InnerCollection.Count - 1);
            return food;
        }

        public IEnumerator<FoodToken> GetEnumerator()
        {
            return this.InnerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        void ICollection<FoodToken>.Add(FoodToken item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            this.InnerCollection.Clear();
        }

        bool ICollection<FoodToken>.Contains(FoodToken item)
        {
            throw new NotSupportedException();
        }

        void ICollection<FoodToken>.CopyTo(FoodToken[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        bool ICollection<FoodToken>.Remove(FoodToken item)
        {
            throw new NotSupportedException();
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = this.CollectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}