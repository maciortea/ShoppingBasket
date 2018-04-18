namespace ApplicationCore
{
    public class ProductBuilder
    {
        private readonly Product _product;

        public ProductBuilder()
        {
            _product = new Product();
        }

        public Product Build()
        {
            return _product;
        }

        public ProductBuilder SetProductType(ProductType productType)
        {
            _product.ProductType = productType;
            return this;
        }

        public ProductBuilder SetPrice(decimal price)
        {
            _product.Price = price;
            return this;
        }

        public static Product CreateDefaultProduct()
        {
            return new ProductBuilder()
                .SetProductType(ProductType.NullProduct)
                .Build();
        }

        public static Product CreateButter()
        {
            return new ProductBuilder()
                .SetProductType(ProductType.Butter)
                .SetPrice(PriceFor.Butter)
                .Build();
        }

        public static Product CreateBread()
        {
            return new ProductBuilder()
                .SetProductType(ProductType.Bread)
                .SetPrice(PriceFor.Bread)
                .Build();
        }

        public static Product CreateMilk()
        {
            return new ProductBuilder()
                .SetProductType(ProductType.Milk)
                .SetPrice(PriceFor.Milk)
                .Build();
        }
    }
}
