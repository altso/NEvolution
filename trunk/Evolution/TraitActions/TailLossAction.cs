namespace Evolution.TraitActions
{
    using Evolution.Traits;

    public class TailLossAction : TraitAction<TailLoss>
    {
        public TailLossAction(TailLoss trait, Trait tailTrait, Carnivorous carnivorous)
            : base(trait)
        {
            this.TailTrait = tailTrait;
            this.Carnivorous = carnivorous;
        }

        public Trait TailTrait { get; private set; }

        public Carnivorous Carnivorous { get; private set; }

        public override void Execute(GameContext context)
        {
            this.Trait.Tap(context);
            this.TailTrait.Card.MoveTo(this.Trait.Animal.Player.Discard);
            this.Carnivorous.Animal.Food.TakeFrom(context.Game.ExtraFood);
            this.Carnivorous.Tap(context);
        }
    }
}