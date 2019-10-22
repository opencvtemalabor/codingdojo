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

        public void RegisterComboDiscount(string comboOfProducts, int priceOfCombo, bool isClubOnly = false)
        {
            //mivel a combodiscount igazából nem azt várja, hogy mennyi az ára az adott termékcsoportnak együtt, hanem, hogy mennyivel lesz olcsóbb, ezért ki kell számolni ezt, hogy kívülről a termékek közös árát lehessen megadni
            int discount = priceOfCombo - Total(comboOfProducts);   //ez egy negatív számként mondja meg, hogy mennyivel lesz kevesebb a termékek ára, ha együtt vannak

            discounts.Add(new ComboDiscount(comboOfProducts, discount, isClubOnly));
        }
    }
}
