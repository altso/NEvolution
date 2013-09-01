namespace Evolution.Moves
{
    using System.Linq;

    public class DealCardsMove : Move
    {
        public override void Execute(GameContext context)
        {
            foreach (var player in context.Game.Players)
            {
                int count = player.Animals.Count == 0 && player.Hand.Count == 0 ? 6 : player.Animals.Count + 1;
                for (int i = 0; i < count; i++)
                {
                    if (context.Game.Dealer.Count > 0)
                    {
                        context.Game.Dealer.First().MoveTo(player.Hand);
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Cards were dealt.";
        }
    }
}