namespace Evolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class Deck : MetaObject, IEnumerable<Card>, INotifyCollectionChanged
    {
        private readonly List<Card> list = new List<Card>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count
        {
            get { return this.list.Count; }
        }

        protected List<Card> List
        {
            get { return this.list; }
        }

        public Card this[int index]
        {
            get { return this.list[index]; }
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int IndexOf(Card card)
        {
            return this.list.IndexOf(card);
        }

        protected internal void Add(Card card)
        {
            this.list.Add(card);
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, card));
        }

        protected internal void Remove(Card card)
        {
            int index = this.list.IndexOf(card);
            this.list.Remove(card);
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, card, index));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var collectionChanged = this.CollectionChanged;
            if (collectionChanged != null)
            {
                collectionChanged(this, e);
            }
        }
    }
}