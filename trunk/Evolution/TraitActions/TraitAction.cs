namespace Evolution.TraitActions
{
    using System.Diagnostics.CodeAnalysis;
    using Evolution.Traits;

    public abstract class TraitAction : MetaObject
    {
        protected TraitAction(Trait trait)
        {
            this.Trait = trait;
        }

        public Trait Trait { get; private set; }

        public abstract void Execute(GameContext context);

        public override string ToString()
        {
            return string.Format("{0}'s {1} used {2}.", Trait.Animal.Player.Name, Trait.Animal.Name, Trait.Name);
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public abstract class TraitAction<T> : TraitAction
        where T : Trait
    {
        protected TraitAction(T trait)
            : base(trait)
        {
        }

        public new T Trait
        {
            get { return (T)base.Trait; }
        }
    }
}