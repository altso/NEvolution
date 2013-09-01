namespace Evolution
{
    public class GameContext
    {
        public GameContext(Game game)
        {
            this.Game = game;
        }

        public Game Game { get; private set; }
    }
}