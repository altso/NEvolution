namespace Evolution.Traits
{
    using Evolution.TraitActions;

    public sealed class Scavanger : Trait, IAttachable
    {
        public bool CanAttach(TraitContext context)
        {
            var carnivorous = context.TraitAction as CarnivorousAction;
            if (carnivorous != null)
            {
                return carnivorous.TargetAnimal.Player == Animal.Player &&
                    Animal.Food.HasRoomForMore &&
                    context.TraitAction[typeof(Poisonous)] == null;
            }

            return false;
        }

        public void Attach(TraitContext context)
        {
            context.TraitAction[typeof(Poisonous)] = new object();
            Animal.Food.TakeFrom(context.Game.ExtraFood);
        }

        public override bool CanBeAddedToAnimal(AddToAnimalContext context)
        {
            return base.CanBeAddedToAnimal(context) && !context.Animal.HasTrait<Carnivorous>();
        }
    }
}