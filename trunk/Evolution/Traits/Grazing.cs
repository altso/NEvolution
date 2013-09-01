namespace Evolution.Traits
{
    using Evolution.TraitActions;

    public sealed class Grazing : Trait, IUsable
    {
        public bool CanUse(GameContext context)
        {
            return context.Game.BankFood.Count > 0;
        }

        public TraitAction Use(GameContext context)
        {
            return new GrazingAction(this);
        }
    }
}