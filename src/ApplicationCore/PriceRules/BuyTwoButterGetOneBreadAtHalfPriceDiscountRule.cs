using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.PriceRules
{
    public class BuyTwoButterGetOneBreadAtHalfPriceDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(IReadOnlyList<BasketItem> basketItems)
        {
            decimal discount = 0m;
            var butter = basketItems.SingleOrDefault(x => x.Product.ProductType == ProductType.Butter);
            var bread = basketItems.SingleOrDefault(x => x.Product.ProductType == ProductType.Bread);
            if (butter != null && bread != null)
            {
                var discountsForBreadCount = butter.Quantity / 2;
                if (discountsForBreadCount > 0)
                {
                    for (var i = 0; i < bread.Quantity && discountsForBreadCount > 0; i++)
                    {
                        discount += bread.Product.Price / 2;
                        discountsForBreadCount--;
                    }
                }
            }
            return discount;
        }
    }
}
