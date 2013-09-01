namespace Evolution.Traits
{
    public interface IAttachable
    {
        bool CanAttach(TraitContext context);

        void Attach(TraitContext context);
    }
}