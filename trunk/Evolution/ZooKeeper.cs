namespace Evolution
{
    using System.Linq;
    using Moves;

    /// <summary>
    /// Creates animals as many as possible.
    /// </summary>
    public class ZooKeeper : Player
    {
        protected override DevelopmentMove Develop(GameContext context)
        {
            if (this.Hand.Any())
            {
                return new NewAnimalMove(this, this.Hand.First());
            }

            return base.Develop(context);
        }

        protected override FeedingMove Feed(GameContext context)
        {
            var animal = this.Animals.FirstOrDefault(a => a.Food.IsNotEnough);
            if (animal != null && animal.EatFromBank.CanUse(context))
            {
                return new TraitsMove(this, animal.EatFromBank.Use(context));
            }

            return base.Feed(context);
        }
    }
}