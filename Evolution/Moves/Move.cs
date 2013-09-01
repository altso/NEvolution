namespace Evolution.Moves
{
    public abstract class Move : MetaObject
    {
        public abstract void Execute(GameContext context);
    }
}