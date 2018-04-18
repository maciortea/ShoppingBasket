namespace ApplicationCore.PriceRules
{
    public class GeneralPriceRule : IPriceRule
    {
        public bool CanCalculate(BasketItem basketItem)
        {
            return basketItem.Product.ProductType == ProductType.Butter ||
                basketItem.Product.ProductType == ProductType.Bread;
        }

        public decimal CalculatePrice(BasketItem basketItem)
        {
            return basketItem.Quantity * basketItem.Product.Price;
        }
    }
}
