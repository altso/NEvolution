namespace Evolution.Moves
{
    public abstract class PlayerMove : Move
    {
        protected PlayerMove(Player player)
        {
            this.Player = player;
        }

        protected Player Player { get; set; }

        public override string ToString()
        {
            return string.Format("{0} did something unknown.", this.Player.Name);
        }
    }
}