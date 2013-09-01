namespace Evolution.Traits
{
    using Evolution.TraitActions;

    public sealed class Running : TapableTrait, ITransformer
    {
        public Running()
            : base(0, 1)
        {
        }

        public bool CanTransform(TraitContext context)
        {
            var carnivorous = context.TraitAction as CarnivorousAction;
            if (carnivorous != null)
            {
                return !IsTapped && carnivorous.TargetAnimal == Animal;
            }

            return false;
        }

        public TraitAction Transform(TraitContext context)
        {
            this.Tap(context);

            if (context.Game.Random.Next(1, 7) > 3)
            {
                return new RunningAction(this, ((CarnivorousAction)context.TraitAction).Trait);
            }

            return context.TraitAction;
        }
    }
}