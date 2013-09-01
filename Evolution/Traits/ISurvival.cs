namespace Evolution.Traits
{
    using System.Linq;

    public interface ISurvival
    {
        bool CanBeEatenBy(Animal animal);
    }

    public static class SurvivalExtensions
    {
        public static bool CanBeEatenBy(this Animal victim, Animal hunter)
        {
            return victim.Select(c => c.Trait).OfType<ISurvival>().All(a => a.CanBeEatenBy(hunter));
        }
    }
}