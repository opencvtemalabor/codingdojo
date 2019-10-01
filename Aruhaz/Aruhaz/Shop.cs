using System;
using System.Collections.Generic;
using System.Text;

namespace Aruhaz
{
    class Shop
    {
        protected int price = 0;
        private Dictionary<char, int> Prices = new Dictionary<char, int>();
        
        public int Total(string param = "")
        {
            if (param == "")
            {
                return 0;
            }
            foreach (char item in param)
            {
                price += Prices[item];
            }
            return price;
        }
        
        public void Register(char product, int productPrice)
        {
            Prices[product] = productPrice;
        }
    }
}
