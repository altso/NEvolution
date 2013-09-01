namespace Evolution.Traits
{
    public abstract class PairwiseTrait : Trait
    {
        public Animal PairedAnimal { get; set; }

        public override bool CanBeAddedToAnimal(AddToAnimalContext context)
        {
            // todo: not supported
            return false;
        }
    }
}