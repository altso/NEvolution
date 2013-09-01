namespace Evolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class GameTest
    {
        [Test]
        public void Next()
        {
            var game = new Game();
            var dealer = new TheOriginOfSpeciesDeck();
            dealer.Shuffle();
            game.Dealer = dealer;
            game.Players.Add(new ZooKeeper());
            game.Players.Add(new Player());
            game.Players.Add(new LionKing());

            var dealerCards = new List<Card>(dealer);

            int i = 0;
            do
            {
                Console.WriteLine("[==== Step {0} ====]", i++);
                Console.WriteLine(game);
                game.Next();

                var missedCards = dealerCards.Except(game.Decks.SelectMany(d => d)).ToList();
                Assert.That(missedCards, Is.Empty);
            }
            while (game.State != GameState.End);
        }
    }
}