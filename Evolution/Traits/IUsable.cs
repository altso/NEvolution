namespace Evolution.Traits
{
    using Evolution.TraitActions;

    public interface IUsable
    {
        bool CanUse(GameContext context);

        TraitAction Use(GameContext context);
    }
}