namespace Evolution.Traits
{
    using System.Linq;
    using Evolution.TraitActions;

    public sealed class FatTissue : Trait, IUsable
    {
        public bool CanUse(GameContext context)
        {
            return this.Animal.Food.OfType<FatToken>().Any() && this.Animal.Food.IsNotEnough;
        }

        public TraitAction Use(GameContext context)
        {
            return new FatTissueAction(this);
        }

        public override bool CanBeAddedToAnimal(AddToAnimalContext context)
        {
            var player = context.Game.Players.Single(p => p.Hand.Contains(this.Card));
            return context.Player == player;
        }
    }
}