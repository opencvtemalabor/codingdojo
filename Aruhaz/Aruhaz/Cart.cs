using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aruhaz
{
    class Cart : List<CartItem>
    {
        
        public Cart(string cartString = "")
        {
            foreach (var character in cartString)
            {
                if(Char.IsUpper(character)) this.Add(new CartItem(character, 0));
            }
        }
        public double GetTotal()
        {
            double price = 0;
            foreach (var itemInCart in this)
            {
                price += itemInCart.CurrentPrice;
            }

            return price;
        }
    }
}
