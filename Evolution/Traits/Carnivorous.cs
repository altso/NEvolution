namespace Evolution.Traits
{
    using System.Collections.Generic;
    using System.Linq;
    using Evolution.TraitActions;

    public sealed class Carnivorous : TapableTrait, IUsable
    {
        public Carnivorous()
            : base(1, 0)
        {
        }

        public override int FoodRequires
        {
            get { return 1; }
        }

        public bool CanUse(GameContext context)
        {
            return !this.IsTapped && this.Animal.Food.HasRoomForMore && this.GetTargetAnimals(context).Any();
        }

        public TraitAction Use(GameContext context)
        {
            var animals = this.GetTargetAnimals(context);
            return new CarnivorousAction(this, this.Animal.Player.Brain.SelectAnimal(animals, false));
        }

        public override bool CanBeAddedToAnimal(AddToAnimalContext context)
        {
            return base.CanBeAddedToAnimal(context) && !context.Animal.HasTrait<Scavanger>();
        }

        private IEnumerable<Animal> GetTargetAnimals(GameContext context)
        {
            return context.Game.Players.SelectMany(p => p.Animals).Where(a => a != this.Animal && a.CanBeEatenBy(this.Animal));
        }
    }
}