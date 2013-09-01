namespace Evolution.TraitActions
{
    using Evolution.Traits;

    public class CarnivorousAction : TraitAction<Carnivorous>
    {
        public CarnivorousAction(Carnivorous carnivorous, Animal targetAnimal)
            : base(carnivorous)
        {
            this.TargetAnimal = targetAnimal;
        }

        public Animal TargetAnimal { get; private set; }

        public override void Execute(GameContext context)
        {
            this.Trait.Animal.Food.TakeFrom(context.Game.ExtraFood);
            if (this.Trait.Animal.Food.HasRoomForMore)
            {
                this.Trait.Animal.Food.TakeFrom(context.Game.ExtraFood);
            }

            this.TargetAnimal.Die();
            this.Trait.Tap(context);
        }

        public override string ToString()
        {
            return string.Format("{0}'s {1} ate {2}'s {3}.", Trait.Animal.Player.Name, Trait.Animal.Name, this.TargetAnimal.Player.Name, this.TargetAnimal.Name);
        }
    }
}