using ApplicationCore.PriceRules;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly List<IPriceRule> _priceRules;
        private readonly List<IDiscountRule> _discountRules;

        public PriceCalculator()
        {
            // These rules can be loaded from database
            _priceRules = new List<IPriceRule>
            {
                new GeneralPriceRule(),
                new BuyThreeGetOneFreePriceRule()
            };

            _discountRules = new List<IDiscountRule>
            {
                new BuyTwoButterGetOneBreadAtHalfPriceDiscountRule()
            };
        }

        public decimal CalculatePrice(IReadOnlyList<BasketItem> basketItems)
        {
            decimal totalPrice = 0m;
            foreach (var basketItem in basketItems)
            {
                // Calculate price for each item and add it to total price
                totalPrice += CalculatePrice(basketItem);
            }

            // We subtract calculated discount from total price
            totalPrice -= CalculateDiscount(basketItems);

            return totalPrice;
        }

        private decimal CalculatePrice(BasketItem basketItem)
        {
            // Gets the matching price rule
            var priceRule = _priceRules.FirstOrDefault(x => x.CanCalculate(basketItem));
            if (priceRule != null)
            {
                return priceRule.CalculatePrice(basketItem);
            }
            return 0m;
        }

        private decimal CalculateDiscount(IReadOnlyList<BasketItem> basketItems)
        {
            decimal discount = 0m;

            // We calculate discounts separately because multiple discounts can be applied
            foreach (var discountRule in _discountRules)
            {
                discount += discountRule.CalculateDiscount(basketItems);
            }
            return discount;
        }
    }
}
