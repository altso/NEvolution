namespace Evolution.TraitActions
{
    using Evolution.Traits;

    public class GrazingAction : TraitAction
    {
        public GrazingAction(Grazing grazing)
            : base(grazing)
        {
        }

        public override void Execute(GameContext context)
        {
            context.Game.BankFood.TakeOne();
        }

        public override string ToString()
        {
            return string.Format("{0}'s {1} grazed one food.", Trait.Animal.Player.Name, Trait.Animal.Name);
        }
    }
}