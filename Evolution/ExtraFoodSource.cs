namespace Evolution
{
    public class ExtraFoodSource : IFoodSource
    {
        public FoodToken TakeOne()
        {
            return new ExtraToken();
        }
    }
}