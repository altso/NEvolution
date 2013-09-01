namespace Evolution
{
    using System;
    using Traits;

    public class NewDeck : Deck
    {
        private static readonly Random Random = new Random();

        public void Shuffle()
        {
            for (int i = 0; i < this.List.Count; i++)
            {
                int j = Random.Next(this.List.Count);
                var card = this.List[i];
                this.List[i] = this.List[j];
                this.List[j] = card;
            }
        }

        protected void Add(int count, Func<Trait> trait1, Func<Trait> trait2 = null)
        {
            for (int i = 0; i < count; i++)
            {
                this.Add(trait2 != null
                             ? new DualCard(this, trait1(), trait2())
                             : new Card(this, trait1()));
            }
        }
    }
}