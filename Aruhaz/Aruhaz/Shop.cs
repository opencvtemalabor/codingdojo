using System;
using System.Collections.Generic;

namespace Aruhaz
{
    class Shop
    {
        private List<CartProcess> discounts = new List<CartProcess>();
        private BasePrice basePrice = new BasePrice();
        private Cart cart = new Cart();
        SuperShopDiscount superShop = new SuperShopDiscount();
        private Dictionary<int, int> UserSuperShopPoints = new Dictionary<int, int>();

        public int Total(string cartString = "", int SuperShopUser = -1)
        {
             var cartStringWithoutId =
                 SuperShopUser == -1 ? ExtractSupershopUserId(cartString, out SuperShopUser) : cartString;

            cart.SetCartString(cartStringWithoutId);
            basePrice.ApplyCart(cart);
            discounts.ForEach(i => i.ApplyCart(cart));
            if (SuperShopUser != -1)
                superShop.ApplyCart(cart, SuperShopUser);

            return cart.GetTotal();
        }

        public String ExtractSupershopUserId(String cartString, out int userId)
        {
            userId = -1;
            String cartStringWithoutId = "";
            foreach (char c in cartString)
            {
                if (Char.IsNumber(c))
                    userId = (int) Char.GetNumericValue(c);
                else
                    cartStringWithoutId += c;
            }

            return cartStringWithoutId;
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
            discounts.Add(new AmountDiscount(product, amount, discount));
        }

        public void RegisterSuperShopUser(int ID)
        {
            superShop.AddSuperShopUser(ID);
        }

        public double GetUserSuperShop(int ID)
        {
            return superShop.GetUserSuperShopAmount(ID);
        }

        public void RegisterComboDiscount(string comboOfProducts, int priceOfCombo, bool isClubOnly = false)
        {
            int
                discount = 0; //mivel a combodiscount igazából nem azt várja, hogy mennyi az ára az adott termékcsoportnak együtt, hanem, hogy mennyivel lesz olcsóbb, ezért ki kell számolni ezt, hogy kívülről a termékek közös árát lehessen megadni
            //Kikeressük hogy volt-e már ilyen combodiscountunk, mert ha volt akkor a total függvényben az is levonódik az árból,ami miatt a discount értékünk "elromlik" viszont felhasználhatjuk hogy már kiszámoltuk a levonást.
            for (int i = 0; i < discounts.Count; i++)
            {
                if (discounts[i].isSame(new ComboDiscount(comboOfProducts, 0, isClubOnly)))
                    discount = ((ComboDiscount) discounts[i]).PriceOfCombo;
            }

            if (discount == 0)
            {
                discount = priceOfCombo -
                           Total(comboOfProducts); //ez egy negatív számként mondja meg, hogy mennyivel lesz kevesebb a termékek ára, ha együtt vannak 
            }

            discounts.Add(new ComboDiscount(comboOfProducts, discount, isClubOnly));
        }
    }
}