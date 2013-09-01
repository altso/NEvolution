namespace Evolution
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Evolution.Traits;

    public class Animal : Deck
    {
        public Animal(Player player, Card baseCard)
        {
            this.Player = player;
            this.Food = new AnimalStorage(this);
            this.Food.CollectionChanged += (_, __) => this.OnPropertyChanged("Food");
            baseCard.MoveTo(this);
        }

        public string Name { get; set; }

        public Player Player { get; private set; }

        public IEnumerable<Trait> Traits
        {
            get { return this.Select(c => c.Trait); }
        }

        public AnimalStorage Food { get; private set; }

        public EatFromBank EatFromBank
        {
            get { return (EatFromBank)this.First().Trait; }
        }

        public void Die()
        {
            while (this.Count > 0)
            {
                this[this.Count - 1].MoveTo(this.Player.Discard);
            }

            this.Player.Animals.Remove(this);
        }

        public void Reset()
        {
            this.Food.Reset();
        }

        public bool HasTrait<T>()
            where T : Trait
        {
            return this.Any(c => c.Trait is T);
        }

        public override string ToString()
        {
            return string.Format("[{0}]", string.Join(string.Empty, this.Skip(1).Select(c => c.Trait.ToString())));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            this.OnPropertyChanged("FoodNeeded");
        }
    }
}