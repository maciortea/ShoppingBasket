using ApplicationCore;
using ApplicationCore.Common;
using System;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class ShoppingBasketUnitTest
    {
        private Basket CreateStubBasket()
        {
            IPriceCalculator priceCalculator = new PriceCalculator();
            return new Basket(priceCalculator);
        }

        [Fact]
        public void CreateBasketWithNullPriceCalculatorShouldThrowContractException()
        {
            Func<Basket> createBasket = () => new Basket(null);

            Assert.Throws<ContractException>(createBasket);
        }

        [Fact]
        public void CreateBasketShouldContainEmptyBasketItems()
        {
            var basket = CreateStubBasket();

            Assert.True(basket.BasketItems.Count == 0);
        }

        [Fact]
        public void AddNullProductShouldThrowContractException()
        {
            var basket = CreateStubBasket();
            Action addProduct = () => basket.AddProductToBasket(null, 1);

            Assert.Throws<ContractException>(addProduct);
        }

        [Fact]
        public void AddProductWithZeroQuantityShouldThrowContractException()
        {
            var basket = CreateStubBasket();
            Action addProduct = () => basket.AddProductToBasket(ProductBuilder.CreateBread(), 0);

            Assert.Throws<ContractException>(addProduct);
        }

        [Fact]
        public void AddNewProductShouldUpdateBasketItemsWithNewProduct()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 1);

            Assert.True(basket.BasketItems.Count == 1);
        }

        [Fact]
        public void AddExistingProductShouldNotModifyBasketItems()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 1);
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 1);

            Assert.True(basket.BasketItems.Count == 1);
        }

        [Fact]
        public void AddExistingProductShouldUpdateQuantityForExistingProduct()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 1);
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 1);

            var product = basket.BasketItems.First();

            Assert.Equal(2, product.Quantity);
        }

        [Fact]
        public void CreateEmptyBasketShouldReturnPriceEqualZero()
        {
            var basket = CreateStubBasket();

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(0m, totalPrice);
        }

        [Fact]
        public void BuyOnlyButterShouldGetButterPriceTimesNoOfButters()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 2);
            
            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(2 * PriceFor.Butter, totalPrice);
        }

        [Fact]
        public void BuyOnlyBreadShouldGetBreadPriceTimesNoOfBreads()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateBread(), 3);

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(3 * PriceFor.Bread, totalPrice);
        }

        [Fact]
        public void BuyTwoButterAndOneBreadShouldGetOneBreadAtHalfPrice()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 2);
            basket.AddProductToBasket(ProductBuilder.CreateBread(), 1);

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(2.1m, totalPrice);
        }

        [Fact]
        public void BuyFiveButterAndThreeBreadShouldGetTwoBreadsAtHalfPrice()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 5);
            basket.AddProductToBasket(ProductBuilder.CreateBread(), 3);

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(6m, totalPrice);
        }

        [Fact]
        public void BuyOneButterAndThreeBreadShouldGetOneButterAndThreeBreadsPrice()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 1);
            basket.AddProductToBasket(ProductBuilder.CreateBread(), 3);

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(PriceFor.Butter + 3 * PriceFor.Bread, totalPrice);
        }

        [Fact]
        public void BuySevenMilkShouldGetOneForFree()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateMilk(), 7);

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(6 * PriceFor.Milk, totalPrice);
        }

        [Fact]
        public void BuyOneBreadOneButterOneMilkShouldGetPriceEqualTwoPointNinetyFive()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateBread(), 1);
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 1);
            basket.AddProductToBasket(ProductBuilder.CreateMilk(), 1);

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(2.95m, totalPrice);
        }

        [Fact]
        public void BuyTwoButterTwoBreadShouldGetPriceEqualThreePointOne()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateBread(), 2);
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 2);

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(3.1m, totalPrice);
        }

        [Fact]
        public void BuyTwoButterOneBreadEightMilkShouldGetPriceEqualNine()
        {
            var basket = CreateStubBasket();
            basket.AddProductToBasket(ProductBuilder.CreateBread(), 1);
            basket.AddProductToBasket(ProductBuilder.CreateButter(), 2);
            basket.AddProductToBasket(ProductBuilder.CreateMilk(), 8);

            decimal totalPrice = basket.GetTotalPrice();

            Assert.Equal(9m, totalPrice);
        }
    }
}
