namespace Evolution
{
    using System;

    public struct GameDate : IComparable<GameDate>, IEquatable<GameDate>
    {
        private int turn;

        public int Turn
        {
            get
            {
                return this.turn;
            }

            set
            {
                this.turn = value;
                this.Round = 1;
            }
        }

        public int Round { get; set; }

        public int CompareTo(GameDate other)
        {
            int result = this.Turn.CompareTo(other.Turn);
            if (result == 0)
            {
                result = this.Round.CompareTo(other.Round);
            }

            return result;
        }

        public bool Equals(GameDate other)
        {
            return other.Turn == this.Turn && other.Round == this.Round;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Turn, this.Round);
        }
    }
}