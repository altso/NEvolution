namespace Evolution
{
    using System.Diagnostics.CodeAnalysis;

    public abstract class FoodToken
    {
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class BankToken : FoodToken
    {
    }

    public sealed class ExtraToken : FoodToken
    {
    }

    public sealed class FatToken : FoodToken
    {
    }
}