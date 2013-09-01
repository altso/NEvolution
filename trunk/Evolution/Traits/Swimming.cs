namespace Evolution.Traits
{
    public sealed class Swimming : Trait, ISurvival
    {
        public bool CanBeEatenBy(Animal animal)
        {
            return animal.HasTrait<Swimming>();
        }
    }
}