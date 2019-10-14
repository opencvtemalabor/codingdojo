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
            Assert.Equal( 10, price);
        }

        [Fact]
        public void CountDiscountWorks()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.RegisterCountDiscount('A', 3, 4);

            var price = shop.Total("AAAA");

            Assert.Equal( 30,price);
        }

        [Fact]
        public void AmountDiscountWorks()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.RegisterAmountDiscount('A', 5, 0.9);

            var price = shop.Total("AAAAA");

            Assert.Equal(45,price);
        }
        
        [Fact]
        public void ComboDiscountWorks()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.Register('B', 20);
            shop.Register('C', 50);
            shop.RegisterComboDiscount("ABC", 60);

            var price = shop.Total("CAAAABB");

            Assert.Equal(110, price);
        }
    }
}
