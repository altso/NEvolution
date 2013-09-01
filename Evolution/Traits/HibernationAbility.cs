namespace Evolution.Traits
{
    using Evolution.TraitActions;

    public sealed class HibernationAbility : TapableTrait, IUsable
    {
        public HibernationAbility()
            : base(2, 0)
        {
        }

        public bool CanUse(GameContext context)
        {
            return !this.IsTapped && context.Game.Dealer.Count > 0;
        }

        public TraitAction Use(GameContext context)
        {
            return new HibernationAbilityAction(this);
        }
    }
}