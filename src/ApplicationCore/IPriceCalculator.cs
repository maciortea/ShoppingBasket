using System.Collections.Generic;

namespace ApplicationCore
{
    public interface IPriceCalculator
    {
        decimal CalculatePrice(IReadOnlyList<BasketItem> basketItems);
    }
}
