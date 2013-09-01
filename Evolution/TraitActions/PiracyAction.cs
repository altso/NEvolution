namespace Evolution.TraitActions
{
    using Evolution.Traits;

    public sealed class PiracyAction : TraitAction<Piracy>
    {
        public PiracyAction(Piracy piracy, Animal targetAnimal)
            : base(piracy)
        {
            this.TargetAnimal = targetAnimal;
        }

        public Animal TargetAnimal { get; private set; }

        public override void Execute(GameContext context)
        {
            this.Trait.Animal.Food.TakeFrom(this.TargetAnimal.Food);
            this.Trait.Tap(context);
        }
    }
}