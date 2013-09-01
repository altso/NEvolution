namespace Evolution
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Traits;

    public class Card : MetaObject
    {
        private readonly Trait trait;
        private readonly Trait beingAnAnimal;

        public Card(Deck deck, Trait trait)
            : this(deck)
        {
            this.trait = trait;
            this.trait.Card = this;
            this.trait.PropertyChanged += (_, __) => OnPropertyChanged("Trait");
        }

        protected Card(Deck deck)
        {
            this.Deck = deck;
            this.beingAnAnimal = new EatFromBank
            {
                Card = this
            };
        }

        public event EventHandler DeckChanged;

        public Deck Deck { get; private set; }

        public virtual bool IsAnimalBase
        {
            get { return this.Deck is Animal && this.Deck.IndexOf(this) == 0; }
        }

        public virtual Trait Trait
        {
            get
            {
                return this.IsAnimalBase
                    ? this.beingAnAnimal
                    : this.trait;
            }
        }

        protected Trait BeingAnAnimal
        {
            get { return this.beingAnAnimal; }
        }

        public void MoveTo(Deck deck)
        {
            this.Deck.Remove(this);
            deck.Add(this);
            this.Deck = deck;
            this.OnDeckChanged(EventArgs.Empty);
        }

        protected virtual void OnDeckChanged(EventArgs e)
        {
            var handler = this.DeckChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class DualCard : Card
    {
        private bool isTurned;

        public DualCard(Deck deck, Trait trait1, Trait trait2)
            : base(deck)
        {
            this.Trait1 = trait1;
            this.Trait2 = trait2;
            this.Trait1.Card = this;
            this.Trait2.Card = this;
            this.Trait1.PropertyChanged += (_, __) => OnPropertyChanged("Trait");
            this.Trait2.PropertyChanged += (_, __) => OnPropertyChanged("Trait");
        }

        public Trait Trait1 { get; private set; }

        public Trait Trait2 { get; private set; }

        public override Trait Trait
        {
            get
            {
                return this.IsAnimalBase
                           ? this.BeingAnAnimal
                           : this.isTurned ? this.Trait2 : this.Trait1;
            }
        }

        public void Turn()
        {
            this.isTurned = !this.isTurned;
            this.OnPropertyChanged("Trait");
        }
    }
}