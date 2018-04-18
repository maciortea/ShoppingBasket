namespace ApplicationCore.PriceRules
{
    public interface IPriceRule
    {
        bool CanCalculate(BasketItem basketItem);
        decimal CalculatePrice(BasketItem basketItem);
    }
}
