using ApplicationCore.Common;

namespace ApplicationCore
{
    public class Product
    {
        private decimal _price;

        public ProductType ProductType { get; set; }

        public decimal Price
        {
            get => _price;
            set
            {
                Contract.Require(value > 0m, "Price must be greater than 0");
                _price = value;
            }
        }
    }
}
