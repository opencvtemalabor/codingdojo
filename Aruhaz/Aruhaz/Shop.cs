using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aruhaz
{
    class Shop
    {
       
        private List<CartProcess> countDiscounts = new List<CartProcess>();
        private List<CartProcess> comboDiscounts = new List<CartProcess>();
        private List<CartProcess> amountDiscounts = new List<CartProcess>();
        private BasePrice basePrice = new BasePrice();

        public int Total(string cartString = "")
        {

            Cart cart = new Cart(cartString);
            basePrice.ApplyCart(cart);
            countDiscounts.ForEach(i => i.ApplyCart(cart));
            comboDiscounts.ForEach(i => i.ApplyCart(cart));
            amountDiscounts.ForEach(i => i.ApplyCart(cart));
            return (int)Math.Round(cart.GetTotal());

        }

        internal void RegisterClubMember(char v)
        {
            // TODO: implement
        }

        internal void RegisterCountDiscount(char productName, int payCount, int aquireCount)
        {
            countDiscounts.Add(new CountDiscount(productName, payCount, aquireCount));
        }

        public void Register(char product, int productPrice)
        {
            basePrice[product] = productPrice;
        }    

        public void RegisterAmountDiscount(char product, int amount, double discount)
        {
           
            amountDiscounts.Add(new AmountDiscount(product,amount,discount));
        }

        public void RegisterComboDiscount(string comboOfProducts, int priceOfCombo)
        {
            comboDiscounts.Add(new ComboDiscount(comboOfProducts, priceOfCombo));
        }
    }
}
