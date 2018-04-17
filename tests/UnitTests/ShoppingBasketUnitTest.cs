using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class ShoppingBasketUnitTest
    {
        [Fact]
        public void BuyOnlyButterShouldGetButterPriceTimesNoOfButters()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Butter, Quantity = 2 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(2 * Basket.ButterPrice, totalPrice);
        }

        [Fact]
        public void BuyOnlyBreadShouldGetBreadPriceTimesNoOfBreads()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Bread, Quantity = 3 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(3 * Basket.BreadPrice, totalPrice);
        }

        [Fact]
        public void BuyTwoButterAndOneBreadShouldGetOneBreadAtHalfPrice()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Butter, Quantity = 2 },
                new BasketItem { ProductType = ProductType.Bread, Quantity = 1 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(2.1m, totalPrice);
        }

        [Fact]
        public void BuyFiveButterAndThreeBreadShouldGetTwoBreadsAtHalfPrice()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Butter, Quantity = 3 },
                new BasketItem { ProductType = ProductType.Butter, Quantity = 2 },
                new BasketItem { ProductType = ProductType.Bread, Quantity = 1 },
                new BasketItem { ProductType = ProductType.Bread, Quantity = 2 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(6m, totalPrice);
        }

        [Fact]
        public void BuyOneButterAndThreeBreadShouldGetOneButterAndThreeBreadsPrice()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Butter, Quantity = 1 },
                new BasketItem { ProductType = ProductType.Bread, Quantity = 3 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(Basket.ButterPrice + 3 * Basket.BreadPrice, totalPrice);
        }

        [Fact]
        public void BuySevenMilkShouldGetOneForFree()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Milk, Quantity = 3 },
                new BasketItem { ProductType = ProductType.Milk, Quantity = 4 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(6 * Basket.MilkPrice, totalPrice);
        }

        [Fact]
        public void BuyOneBreadOneButterOneMilkShouldGetPriceEqualTwoPointNinetyFive()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Bread, Quantity = 1 },
                new BasketItem { ProductType = ProductType.Butter, Quantity = 1 },
                new BasketItem { ProductType = ProductType.Milk, Quantity = 1 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(2.95m, totalPrice);
        }

        [Fact]
        public void BuyTwoButterTwoBreadShouldGetPriceEqualThreePointOne()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Bread, Quantity = 2 },
                new BasketItem { ProductType = ProductType.Butter, Quantity = 2 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(3.1m, totalPrice);
        }

        [Fact]
        public void BuyTwoButterOneBreadEightMilkShouldGetPriceEqualNine()
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem { ProductType = ProductType.Butter, Quantity = 2 },
                new BasketItem { ProductType = ProductType.Bread, Quantity = 1 },
                new BasketItem { ProductType = ProductType.Milk, Quantity = 8 }
            };
            var basket = new Basket(basketItems);
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(9m, totalPrice);
        }
    }

    public class BasketItem
    {
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
    }

    public enum ProductType
    {
        Butter,
        Milk,
        Bread
    }

    public class Basket
    {
        public const decimal ButterPrice = 0.8m;
        public const decimal MilkPrice = 1.15m;
        public const decimal BreadPrice = 1m;

        private List<BasketItem> basketItems;

        public Basket(List<BasketItem> basketItems)
        {
            this.basketItems = basketItems;
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0m;
            var butterCount = basketItems.Where(x => x.ProductType == ProductType.Butter).Select(x => x.Quantity).Sum();
            var breadCount = basketItems.Where(x => x.ProductType == ProductType.Bread).Select(x => x.Quantity).Sum();
            var milkCount = basketItems.Where(x => x.ProductType == ProductType.Milk).Select(x => x.Quantity).Sum();

            if (breadCount > 0)
            {
                var discountedBreadsCount = butterCount / 2;
                if (breadCount >= discountedBreadsCount)
                {
                    totalPrice += discountedBreadsCount * BreadPrice / 2;
                    totalPrice += (breadCount - discountedBreadsCount) * BreadPrice;
                }
                else if (breadCount < discountedBreadsCount)
                {
                    totalPrice += breadCount * BreadPrice / 2;
                }
            }

            totalPrice += butterCount * ButterPrice;

            totalPrice += milkCount * MilkPrice;

            var setsOfFourMilks = milkCount / 4;
            totalPrice -= setsOfFourMilks * MilkPrice;

            //basketItems.Add(new BasketItem { ProductType = ProductType.Milk, Quantity = setsOfThreeMilks });

            return totalPrice;
        }
    }
}
