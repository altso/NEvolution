namespace Evolution.TraitActions
{
    using Evolution.Traits;

    public class FatTissueAction : TraitAction
    {
        public FatTissueAction(FatTissue fatTissue)
            : base(fatTissue)
        {
        }

        public override void Execute(GameContext context)
        {
            this.Trait.Animal.Food.ConvertFat();
        }
    }
}