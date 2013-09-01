namespace Evolution
{
    public class BankStorage : FoodStorage
    {
        public void Renew(int count)
        {
            InnerCollection.Clear();
            for (int i = 0; i < count; i++)
            {
                InnerCollection.Add(new BankToken());
            }
        }
    }
}