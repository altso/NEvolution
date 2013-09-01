namespace Evolution.Traits
{
    using Evolution.TraitActions;

    public class EatFromBank : Trait, IUsable, ISurvival
    {
        public override int FoodRequires
        {
            get { return 1; }
        }

        public bool CanUse(GameContext context)
        {
            return this.Animal.Food.HasRoomForMore && context.Game.BankFood.Count > 0;
        }

        public TraitAction Use(GameContext context)
        {
            return new EatFromBankAction(this);
        }

        public bool CanBeEatenBy(Animal animal)
        {
            return animal.HasTrait<Swimming>() == this.Animal.HasTrait<Swimming>();
        }
    }
}