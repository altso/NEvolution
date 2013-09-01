namespace Evolution.TraitActions
{
    using Evolution.Traits;

    public class EatFromBankAction : TraitAction
    {
        public EatFromBankAction(Trait trait)
            : base(trait)
        {
        }

        public override void Execute(GameContext context)
        {
            this.Trait.Animal.Food.TakeFrom(context.Game.BankFood);
        }

        public override string ToString()
        {
            return string.Format("{0}'s {1} ate from bank.", Trait.Animal.Player.Name, Trait.Animal.Name);
        }
    }
}