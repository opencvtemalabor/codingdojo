using System;
using Xunit;


namespace Aruhaz
{
    public class RegisterTest
    {
        [Fact]
        public void RegisteredProductStays()
        {
            //arrange
            var shop = new Shop();
            shop.Register('A', 10);

            //act
            var price = shop.Total("A");
            
            //assert
            Assert.Equal(price, 10);
        }

        [Fact]
        public void CountDiscountWorks()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.RegisterCountDiscount('A', 3, 4);

            var price = shop.Total("AAAA");

            Assert.Equal(price, 30);
        }

        [Fact]
        public void AmountDiscountWorks()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.RegisterAmountDiscount('A', 5, 0.9);

            var price = shop.Total("AAAAA");

            Assert.Equal(price, 45);
        }
    }
}