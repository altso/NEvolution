namespace Evolution.Moves
{
    public class AddCardMove : DevelopmentMove
    {
        public AddCardMove(Player player, Card card, Animal animal)
            : base(player)
        {
            this.Card = card;
            this.Animal = animal;
        }

        public Card Card { get; private set; }

        public Animal Animal { get; private set; }

        public override void Execute(GameContext context)
        {
            this.Card.MoveTo(Animal);
        }

        public override string ToString()
        {
            return string.Format("{0} added {1} to {2}.", Player.Name, Card.Trait.Name, Animal.Name);
        }
    }
}