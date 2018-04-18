using ApplicationCore.Common;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore
{
    public class Basket
    {
        private List<BasketItem> _basketItems;
        private readonly IPriceCalculator _priceCalculator;

        public Basket(IPriceCalculator priceCalculator)
        {
            Contract.Require(priceCalculator != null, "Price calculator is required");

            _basketItems = new List<BasketItem>();
            _priceCalculator = priceCalculator;
        }

        public IReadOnlyList<BasketItem> BasketItems
        {
            get { return _basketItems; }
        }

        public void AddProductToBasket(Product product, int quantity)
        {
            Contract.Require(product != null, "Product is required");
            Contract.Require(quantity > 0, "Quantity must be greater than 0");

            var singleProduct = _basketItems.SingleOrDefault(x => x.Product.ProductType == product.ProductType);
            if (singleProduct == null)
            {
                singleProduct = BasketItem.Create(product, quantity);
                _basketItems.Add(singleProduct);
            }
            else
            {
                singleProduct.SetQuantity(singleProduct.Quantity + quantity);
            }
        }

        public decimal GetTotalPrice()
        {
            return _priceCalculator.CalculatePrice(_basketItems);
        }
    }
}
