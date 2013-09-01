namespace Evolution.Moves
{
    using System;

    public class ThrowDicesMove : Move
    {
        public override void Execute(GameContext context)
        {
            int diceCount, addition;
            switch (context.Game.Players.Count)
            {
                case 2:
                    diceCount = 1;
                    addition = 2;
                    break;
                case 3:
                    diceCount = 2;
                    addition = 0;
                    break;
                case 4:
                    diceCount = 2;
                    addition = 2;
                    break;
                case 5:
                    diceCount = 3;
                    addition = 2;
                    break;
                case 6:
                    diceCount = 3;
                    addition = 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            int count = 0;
            for (int i = 0; i < diceCount; i++)
            {
                count += context.Game.Random.Next(1, 7) + addition;
            }

            context.Game.BankFood.Renew(count);
        }

        public override string ToString()
        {
            return "Food bank was determined.";
        }
    }
}