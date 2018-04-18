namespace ApplicationCore.PriceRules
{
    public class BuyThreeGetOneFreePriceRule : IPriceRule
    {
        public bool CanCalculate(BasketItem basketItem)
        {
            return basketItem.Product.ProductType == ProductType.Milk;
        }

        public decimal CalculatePrice(BasketItem basketItem)
        {
            decimal price = basketItem.Quantity * basketItem.Product.Price;
            var setsOfFour = basketItem.Quantity / 4;
            price -= setsOfFour * basketItem.Product.Price;
            return price;
        }
    }
}
