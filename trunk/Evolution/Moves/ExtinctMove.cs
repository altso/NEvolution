namespace Evolution.Moves
{
    using System.Linq;

    public class ExtinctMove : Move
    {
        public override void Execute(GameContext context)
        {
            var animals = context.Game.Players.SelectMany(p => p.Animals).ToList();
            foreach (var animal in animals)
            {
                if (animal.Food.IsNotEnough)
                {
                    animal.Die();
                }
            }
        }

        public override string ToString()
        {
            return "Unfed animals died.";
        }
    }
}