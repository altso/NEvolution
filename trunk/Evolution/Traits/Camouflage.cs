namespace Evolution.Traits
{
    public sealed class Camouflage : Trait, ISurvival
    {
        public bool CanBeEatenBy(Animal animal)
        {
            return animal.HasTrait<SharpVision>();
        }
    }
}