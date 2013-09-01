namespace Evolution.Traits
{
    public sealed class Burrowing : Trait, ISurvival
    {
        public bool CanBeEatenBy(Animal animal)
        {
            return this.Animal.Food.IsNotEnough;
        }
    }
}