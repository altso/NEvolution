namespace Evolution.Traits
{
    using System.Linq;

    public abstract class Trait : MetaObject
    {
        public Animal Animal
        {
            get { return this.Card.Deck as Animal; }
        }

        public Card Card { get; internal set; }

        public virtual string Name
        {
            get { return this.GetType().Name; }
        }

        public virtual int FoodRequires
        {
            get { return 0; }
        }

        public virtual bool CanBeAddedToAnimal(AddToAnimalContext context)
        {
            if (this.Animal != null)
            {
                throw new LogicException();
            }

            return context.Animal.Player == context.Player && context.Animal.All(c => c.Trait.GetType() != this.GetType());
        }

        public override string ToString()
        {
            return this.Name.Substring(0, 1);
        }
    }
}