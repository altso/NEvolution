namespace Evolution.TraitActions
{
    using Evolution.Traits;

    public class HibernationAbilityAction : TraitAction<HibernationAbility>
    {
        public HibernationAbilityAction(HibernationAbility hibernationAbility)
            : base(hibernationAbility)
        {
        }

        public override void Execute(GameContext context)
        {
            var food = new ExtraFoodSource();
            while (this.Trait.Animal.Food.IsNotEnough)
            {
                this.Trait.Animal.Food.TakeFrom(food);
            }

            this.Trait.Tap(context);
        }
    }
}