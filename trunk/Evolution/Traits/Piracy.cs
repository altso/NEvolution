namespace Evolution.Traits
{
    using System.Collections.Generic;
    using System.Linq;
    using Evolution.TraitActions;

    public sealed class Piracy : TapableTrait, IUsable
    {
        public Piracy()
            : base(1, 0)
        {
        }

        public bool CanUse(GameContext context)
        {
            return !this.IsTapped && this.Animal.Food.HasRoomForMore && this.GetTargetAnimals(context).Any();
        }

        public TraitAction Use(GameContext context)
        {
            var animals = this.GetTargetAnimals(context);
            return new PiracyAction(this, Animal.Player.Brain.SelectAnimal(animals, false));
        }

        private IEnumerable<Animal> GetTargetAnimals(GameContext context)
        {
            return context.Game.Animals.Where(a => a != this.Animal && a.Food.Count > 0 && a.Food.Count < a.Food.Required);
        }
    }
}