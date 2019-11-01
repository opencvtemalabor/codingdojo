using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aruhaz
{
    class Shop
    {
       
        private List<CartProcess> discounts = new List<CartProcess>();
        private BasePrice basePrice = new BasePrice();
        private Cart cart = new Cart();

        public int Total(string cartString = "")
        {

            cart.SetCartString(cartString);
            basePrice.ApplyCart(cart);
            discounts.ForEach(i => i.ApplyCart(cart));
            return cart.GetTotal();

        }

        internal void RegisterClubMember(char v)
        {
            cart.IsClubMember = true;
        }

        internal void RegisterCountDiscount(char productName, int payCount, int aquireCount)
        {
            discounts.Add(new CountDiscount(productName, payCount, aquireCount));
        }

        public void Register(char product, int productPrice)
        {
            basePrice[product] = productPrice;
        }    

        public void RegisterAmountDiscount(char product, int amount, double discount)
        {
           
            discounts.Add(new AmountDiscount(product,amount,discount));
        }

        public void RegisterSuperShopUser(int ID)
        {
            //TODO
        }

        public int GetUserSuperShop(int ID)
        {
            //TODO
            return 0;
        }

        public void RegisterComboDiscount(string comboOfProducts, int priceOfCombo, bool isClubOnly = false)
        {
            int discount = 0;//mivel a combodiscount igazából nem azt várja, hogy mennyi az ára az adott termékcsoportnak együtt, hanem, hogy mennyivel lesz olcsóbb, ezért ki kell számolni ezt, hogy kívülről a termékek közös árát lehessen megadni
            //Kikeressük hogy volt-e már ilyen combodiscountunk, mert ha volt akkor a total függvényben az is levonódik az árból,ami miatt a discount értékünk "elromlik" viszont felhasználhatjuk hogy már kiszámoltuk a levonást.
            for (int i = 0; i < discounts.Count; i++)
            {
                if (discounts[i].isSame(new ComboDiscount(comboOfProducts, 0, isClubOnly)))
                    discount = ((ComboDiscount)discounts[i]).PriceOfCombo;
            }
            if (discount == 0)
            {
                discount = priceOfCombo - Total(comboOfProducts);//ez egy negatív számként mondja meg, hogy mennyivel lesz kevesebb a termékek ára, ha együtt vannak 
            }
            discounts.Add(new ComboDiscount(comboOfProducts, discount, isClubOnly));
        }
    }
}
