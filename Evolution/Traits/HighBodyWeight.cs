namespace Evolution.Traits
{
    public sealed class HighBodyWeight : Trait, ISurvival
    {
        public override int FoodRequires
        {
            get { return 1; }
        }

        public bool CanBeEatenBy(Animal animal)
        {
            return animal.HasTrait<HighBodyWeight>();
        }
    }
}