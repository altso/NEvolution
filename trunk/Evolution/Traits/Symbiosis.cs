namespace Evolution.Traits
{
    public sealed class Symbiosis : PairwiseTrait, ISurvival
    {
        public bool CanBeEatenBy(Animal animal)
        {
            return false;
        }
    }
}