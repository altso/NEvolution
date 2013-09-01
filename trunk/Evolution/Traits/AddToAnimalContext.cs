namespace Evolution.Traits
{
    public class AddToAnimalContext : GameContext
    {
        public AddToAnimalContext(Game game, Animal animal, Player player)
            : base(game)
        {
            this.Animal = animal;
            this.Player = player;
        }

        public Animal Animal { get; private set; }

        public Player Player { get; private set; }
    }
}