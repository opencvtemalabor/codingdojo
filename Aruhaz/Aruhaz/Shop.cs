using System;
using System.Collections.Generic;
using System.Text;

namespace Aruhaz
{
    class Shop
    {
        protected int price = 0;
        private Dictionary<char, int> Prices = new Dictionary<char, int>();
        private Dictionary<char, string> Discounts = new Dictionary<char, string>();
        
        public int Total(string param = "")
        {
            if (param == "")
            {
                return 0;
            }
            foreach (var discount in Discounts)
            {
                if (param.Contains(discount.Key))
                {
                    string[] discountDetails = discount.Value.Split(' ');
                    int paidFor = Convert.ToInt32(discountDetails[0]);
                    int obtainedAmount = Convert.ToInt32(discountDetails[1]);
                    int amount = CountProductOccurrence(discount.Key, param);
                    if (amount >= obtainedAmount)
                    {
                        int countedAmount = amount / obtainedAmount;
                        price += countedAmount * paidFor * Prices[discount.Key];
                        int i = 0;
                        while (i < countedAmount * obtainedAmount)
                        {
                            param = param.Remove(param.IndexOf(discount.Key), 1);
                            i++;
                        }
                    }
                }
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

        public void RegisterCountDiscount(char product, int paidFor, int obtainedAmount)
        {
            string formatString = paidFor + " " + obtainedAmount;
            Discounts[product] = formatString;
        }

        public int CountProductOccurrence(char product, string param)
        {
            int count = 0;
            foreach (char c in param)
            {
                if (c == product)
                    count++;
            }
            return count;
        }
    }
}
