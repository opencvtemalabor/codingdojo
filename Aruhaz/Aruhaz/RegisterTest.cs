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
            Assert.Equal(30, price);
        }

        [Fact]
        public void AmountDiscountWorks()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.RegisterAmountDiscount('A', 5, 0.9);

            var price = shop.Total("AAAAA");

            Assert.Equal(45, price);
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

        [Fact]
        public void MixCountAndComboDiscountsTogether()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.Register('B', 20);
            shop.RegisterCountDiscount('A', 2, 3);
            shop.RegisterComboDiscount("AAB", 35);

            var price = shop.Total("AAAABBBB");
            Assert.Equal(100, price);

        }

        [Fact]
        public void MixCountAndAmountDiscountsTogether()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.Register('B', 20);
            shop.RegisterCountDiscount('A', 2, 3);
            shop.RegisterAmountDiscount('B', 3, 0.9);

            var price = shop.Total("AAAABBBB");
            Assert.Equal(102, price);
        }
        
        [Fact]
        public void TestClubMemberDiscount()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.Register('B', 30);

            //klubtag regisztrálása
            shop.RegisterClubMember('t');

            var price = shop.Total("ABBAt");

            Assert.Equal(72, price);
        }

        [Fact]
        public void TestClubOnlyComboDiscount()
        {
            var shop = new Shop();
            shop.Register('A', 10);
            shop.Register('B', 20);
            shop.Register('C', 30);

            shop.RegisterComboDiscount("ABBC", 60, true);

            shop.RegisterClubMember('t');

            var priceForClub = shop.Total("ABBBCt");
            Assert.Equal(72, priceForClub);         // ABBC=60 + B=20 * 0.9, mert clubmember

            priceForClub = shop.Total("ABBCCACBBt");    //ABBC=60 * 2 + C=30 * 0.9, mert clubmember
            Assert.Equal(135, priceForClub);

            var priceNotForClub = shop.Total("ABBBC");
            Assert.Equal(100, priceNotForClub);     // 10 + 3*20 + 30

            shop.RegisterComboDiscount("ABBC", 60);

            priceNotForClub = shop.Total("ABBBC");  //ABBC=60 + B=20
            Assert.Equal(80, priceNotForClub);
        }

        [Fact]
        public void TestConcurrentDiscounts()
        {
            Shop shop = new Shop();
            shop.Register('A', 10);
            shop.Register('B', 20);
            shop.Register('C', 30);

            shop.RegisterComboDiscount("ABC", 40, true);
            shop.RegisterComboDiscount("ABC", 40);

            var priceForAnyone = shop.Total("ABCABCCC");
            Assert.Equal(140, priceForAnyone);

            priceForAnyone = shop.Total("ABCABCCCt");
            Assert.Equal(126, priceForAnyone);
        }
    }
}
