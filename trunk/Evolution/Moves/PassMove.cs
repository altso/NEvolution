namespace Evolution.Moves
{
    public class PassMove : DevelopmentMove
    {
        public PassMove(Player player)
            : base(player)
        {
        }

        public override void Execute(GameContext context)
        {
            Player.IsPass = true;
        }

        public override string ToString()
        {
            return string.Format("{0} passed.", Player.Name);
        }
    }
}