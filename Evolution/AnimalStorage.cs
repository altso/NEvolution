namespace Evolution
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Evolution.Traits;

    public class AnimalStorage : FoodStorage
    {
        private readonly Animal animal;

        public AnimalStorage(Animal animal)
        {
            this.animal = animal;
            this.animal.CollectionChanged += (_, e) => this.OnFoodRequirementsChanged();
        }

        public int Required
        {
            get { return this.animal.Sum(a => a.Trait.FoodRequires); }
        }

        public int Capacity
        {
            get { return this.Required + this.animal.Select(a => a.Trait).OfType<FatTissue>().Count(); }
        }

        public int NonFatCount
        {
            get { return this.Count(t => !(t is FatToken)); }
        }

        public bool IsNotEnough
        {
            get { return this.NonFatCount < this.Required; }
        }

        public bool HasRoomForMore
        {
            get { return this.Count < this.Capacity; }
        }

        public void TakeFrom(IFoodSource source)
        {
            if (source == this)
            {
                throw new ArgumentException();
            }

            if (this.IsNotEnough)
            {
                this.InnerCollection.Insert(0, source.TakeOne());
            }
            else
            {
                source.TakeOne();
                this.InnerCollection.Add(new FatToken());
            }
        }

        public override FoodToken TakeOne()
        {
            var token = this.InnerCollection.First(t => !(t is FatToken));
            this.InnerCollection.Remove(token);
            return token;
        }

        public void ConvertFat()
        {
            for (int i = 0; i < this.InnerCollection.Count; i++)
            {
                if (this.InnerCollection[i] is FatToken)
                {
                    this.InnerCollection[i] = new ExtraToken();
                    return;
                }
            }
        }

        public void Reset()
        {
            for (int i = this.InnerCollection.Count - 1; i >= 0; i--)
            {
                if (!(this.InnerCollection[i] is FatToken))
                {
                    this.InnerCollection.RemoveAt(i);
                }
            }
        }

        protected virtual void OnFoodRequirementsChanged()
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs("Required"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Capacity"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("IsNotEnough"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("HasRoomForMore"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("NonFatCount"));
        }
    }
}