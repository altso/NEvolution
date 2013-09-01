namespace Evolution.Wpf
{
    using System.Linq;
    using Moves;
    using Traits;

    public class WpfPlayer : Player
    {
        public WpfPlayer(Game game, MainWindow host)
            : base(host)
        {
        }

        protected override DevelopmentMove Develop(GameContext context)
        {
            if (this.Hand.Count != 0)
            {
                var card = this.Brain.SelectCard(this.Hand, true);
                var animal = card != null ? this.Brain.SelectAnimal(context.Game.Animals.Where(a => card.Trait.CanBeAddedToAnimal(new AddToAnimalContext(context.Game, a, this))), true) : null;

                if (card != null && animal != null)
                {
                    return new AddCardMove(this, card, animal);
                }

                if (card != null)
                {
                    return new NewAnimalMove(this, card);
                }
            }

            return new PassMove(this);
        }

        protected override FeedingMove Feed(GameContext context)
        {
            var usableTraits = this.Animals.SelectMany(a => a.Traits).OfType<IUsable>().Where(t => t.CanUse(context)).ToList();

            if (usableTraits.Any())
            {
                var trait = this.Brain.SelectTrait(usableTraits.Select(t => t as Trait), true) as IUsable;
                if (trait != null)
                {
                    return new TraitsMove(this, trait.Use(context));
                }
            }

            return new SkipMove(this);
        }
    }
}