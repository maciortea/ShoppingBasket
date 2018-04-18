using System.Collections.Generic;

namespace ApplicationCore.PriceRules
{
    public interface IDiscountRule
    {
        decimal CalculateDiscount(IReadOnlyList<BasketItem> basketItems);
    }
}
