namespace Evolution
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PlayerCollection : Collection<Player>
    {
        private int currentPlayerIndex;

        public Player Current
        {
            get { return this[this.currentPlayerIndex]; }
        }

        public IEnumerator<Player> EnumerateFrom(Player player)
        {
            int index = this.IndexOf(player);
            if (index < 0)
            {
                throw new InvalidOperationException();
            }

            for (int i = 0; i < this.Count; i++)
            {
                yield return this[(i + index) % this.Count];
            }
        }

        public new IEnumerator<Player> GetEnumerator()
        {
            if (this.Current != null)
            {
                return this.EnumerateFrom(this.Current);
            }

            return base.GetEnumerator();
        }

        public void Next()
        {
            int index = this.IndexOf(this.Current);
            this.currentPlayerIndex = (index + 1) % this.Count;
        }

        public void SetCurrentPlayerIndex(int index)
        {
            this.currentPlayerIndex = index % this.Count;
        }
    }
}