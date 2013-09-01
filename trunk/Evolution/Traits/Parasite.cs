namespace Evolution.Traits
{
    public sealed class Parasite : Trait
    {
        public override int FoodRequires
        {
            get { return 2; }
        }

        public override bool CanBeAddedToAnimal(AddToAnimalContext context)
        {
            return context.Animal.Player != context.Player && !context.Animal.HasTrait<Parasite>();
        }
    }
}