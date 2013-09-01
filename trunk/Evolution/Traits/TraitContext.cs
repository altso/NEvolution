namespace Evolution.Traits
{
    using Evolution.TraitActions;

    public class TraitContext : GameContext
    {
        public TraitContext(Game game, TraitAction traitAction)
            : base(game)
        {
            this.TraitAction = traitAction;
        }

        public TraitAction TraitAction { get; private set; }
    }
}