namespace Evolution.Traits
{
    using Evolution.TraitActions;

    public interface ITransformer
    {
        bool CanTransform(TraitContext context);

        TraitAction Transform(TraitContext context);
    }
}