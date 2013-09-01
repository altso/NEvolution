namespace Evolution.TraitActions
{
    using Evolution.Traits;

    public class RunningAction : TraitAction<Running>
    {
        public RunningAction(Running trait, Carnivorous carnivorous)
            : base(trait)
        {
            this.Carnivorous = carnivorous;
        }

        public Carnivorous Carnivorous { get; private set; }

        public override void Execute(GameContext context)
        {
            Carnivorous.Tap(context);
        }
    }
}