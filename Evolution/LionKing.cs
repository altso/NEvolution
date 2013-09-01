namespace Evolution
{
    using System.Linq;
    using Evolution.TraitActions;
    using Evolution.Traits;
    using Moves;

    public class LionKing : Player
    {
        protected override DevelopmentMove Develop(GameContext context)
        {
            var notCarnivorousAnimal = this.Animals.FirstOrDefault(a => !a.HasTrait<Carnivorous>());
            if (notCarnivorousAnimal != null)
            {
                var carnivorousCard = this.Hand.Select(AsCardWithTrait<Carnivorous>).FirstOrDefault(c => c != null);
                if (carnivorousCard != null)
                {
                    return new AddCardMove(this, carnivorousCard, notCarnivorousAnimal);
                }
            }

            var notCarnivorousCard = this.Hand.FirstOrDefault(NotCarnivorous);
            if (notCarnivorousCard != null)
            {
                int carnivorousCount = this.Hand.Select(AsCardWithTrait<Carnivorous>).Count(c => c != null);
                int notCarnivorousCount = this.Hand.Count(NotCarnivorous);
                if (notCarnivorousCount > carnivorousCount)
                {
                    var animals = context.Game.Animals.Where(a => notCarnivorousCard.Trait.CanBeAddedToAnimal(new AddToAnimalContext(context.Game, a, this)));
                    var animal = this.Brain.SelectAnimal(animals, true);
                    if (animal != null)
                    {
                        return new AddCardMove(this, notCarnivorousCard, animal);
                    }
                }

                return new NewAnimalMove(this, notCarnivorousCard);
            }

            return base.Develop(context);
        }

        protected override FeedingMove Feed(GameContext context)
        {
            var herbivore = this.Animals.FirstOrDefault(a => a.Food.IsNotEnough && !a.HasTrait<Carnivorous>());
            if (herbivore != null)
            {
                if (herbivore.EatFromBank.CanUse(context))
                {
                    return new TraitsMove(this, herbivore.EatFromBank.Use(context));
                }
            }

            var carnivore = this.Animals.FirstOrDefault(a => a.Food.IsNotEnough && a.HasTrait<Carnivorous>() && a.Traits.OfType<Carnivorous>().Single().CanUse(context));
            if (carnivore != null)
            {
                var target = context.Game.Players.Where(p => p != this).SelectMany(p => p.Animals).FirstOrDefault(a => a.CanBeEatenBy(carnivore));
                if (target == null)
                {
                    if (carnivore.EatFromBank.CanUse(context))
                    {
                        return new TraitsMove(this, carnivore.EatFromBank.Use(context));
                    }

                    target = this.Animals.Where(a => a != carnivore && (!a.HasTrait<Carnivorous>() || a.Food.IsNotEnough)).LastOrDefault(a => a.CanBeEatenBy(carnivore));
                }

                if (target != null)
                {
                    var trait = carnivore.Select(c => c.Trait).OfType<Carnivorous>().Single();
                    return new TraitsMove(this, new CarnivorousAction(trait, target));
                }
            }

            return base.Feed(context);
        }

        private static bool NotCarnivorous(Card card)
        {
            if (card.Trait is Carnivorous)
            {
                return false;
            }

            var dualCard = card as DualCard;
            if (dualCard != null)
            {
                if (dualCard.Trait2 is Carnivorous)
                {
                    return false;
                }
            }

            return true;
        }

        private static Card AsCardWithTrait<TTrait>(Card card)
        {
            if (card.Trait is TTrait)
            {
                return card;
            }

            var dualCard = card as DualCard;
            if (dualCard != null)
            {
                if (dualCard.Trait2 is TTrait)
                {
                    dualCard.Turn();
                    return dualCard;
                }
            }

            return null;
        }
    }
}