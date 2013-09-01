namespace Evolution.Traits
{
    using System.Linq;
    using Evolution.TraitActions;

    public sealed class TailLoss : TapableTrait, ITransformer
    {
        public TailLoss()
            : base(0, 1)
        {
        }

        public bool CanTransform(TraitContext context)
        {
            var carnivorous = context.TraitAction as CarnivorousAction;
            if (carnivorous != null)
            {
                return !IsTapped && carnivorous.TargetAnimal == Animal && Animal.Count > 1;
            }

            return false;
        }

        public TraitAction Transform(TraitContext context)
        {
            var tailTrait = Animal.Player.Brain.SelectTrait(Animal.Traits.Skip(1), false);
            return new TailLossAction(this, tailTrait, (Carnivorous)context.TraitAction.Trait);
        }
    }
}