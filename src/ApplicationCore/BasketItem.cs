using ApplicationCore.Common;

namespace ApplicationCore
{
    public class BasketItem
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        private BasketItem()
        {
            Product = ProductBuilder.CreateDefaultProduct();
        }

        public static BasketItem Create(Product product, int quantity)
        {
            Contract.Require(product != null, "Product is required");
            Contract.Require(quantity > 0, "Quantity must be greater than 0");

            return new BasketItem { Product = product, Quantity = quantity };
        }

        public void SetQuantity(int quantity)
        {
            Contract.Require(quantity > 0, "Quantity must be greater than 0");

            Quantity = quantity;
        }
    }
}
