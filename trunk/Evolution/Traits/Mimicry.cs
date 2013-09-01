namespace Evolution.Traits
{
    using System.Collections.Generic;
    using System.Linq;
    using Evolution.TraitActions;

    public sealed class Mimicry : TapableTrait, ITransformer
    {
        public Mimicry()
            : base(0, 1)
        {
        }

        public bool CanTransform(TraitContext context)
        {
            var carnivorous = context.TraitAction as CarnivorousAction;
            if (carnivorous != null)
            {
                return !IsTapped &&
                       carnivorous.TargetAnimal == Animal &&
                       this.GetTargetAnimals(carnivorous).Any();
            }

            return false;
        }

        public TraitAction Transform(TraitContext context)
        {
            this.Tap(context);

            var carnivorous = (CarnivorousAction)context.TraitAction;

            return new CarnivorousAction(carnivorous.Trait, Animal.Player.Brain.SelectAnimal(this.GetTargetAnimals(carnivorous), false));
        }

        private IEnumerable<Animal> GetTargetAnimals(CarnivorousAction carnivorous)
        {
            return Animal.Player.Animals.Where(a => a != Animal && a.CanBeEatenBy(carnivorous.Trait.Animal));
        }
    }
}