using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aruhaz
{
    class Shop
    {
        protected int price = 0;
        private Dictionary<char, int> Prices = new Dictionary<char, int>();
        private Dictionary<char, string> Discounts = new Dictionary<char, string>();
        private Dictionary<string, double> AmountDiscount = new Dictionary<string, double>();
        
        public int Total(string param = "")
        {
            if (param == "")
            {
                return 0;
            }
            List<char> discountedItems = new List<char>();
            List<double> discPrice = new List<double>();
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
            //Searching if there are possible Amount discounts in the input.
            foreach(var item in AmountDiscount)
            {
                string[] parsedKey = item.Key.Split(" ");
                if (param.Contains(parsedKey[0][0]) && CountProductOccurrence(parsedKey[0][0], param) >= Convert.ToInt32(parsedKey[1]))
                {
                    discountedItems.Add(parsedKey[0][0]);
                    discPrice.Add(item.Value);
                }
            }
            foreach (char item in param)
            {
                if (discountedItems.Contains(item))
                    price +=(int)( Prices[item] * discPrice[discountedItems.FindIndex(find=>find==item)]);
                else
                    price += Prices[item];
            }
            return price;
        }

        public void RegisterAmountDiscount(char product, int amount, double discount)
        {
            AmountDiscount.Add($"{product} {amount}", discount);
        }

        public void Register(char product, int productPrice)
        {
            Prices[product] = productPrice;
        }

        public void RegisterCountDiscount(char product, int paidFor, int obtainedAmount)
        {
            Discounts[product] = $"{paidFor} {obtainedAmount}";
        }

        public int CountProductOccurrence(char product, string param)
        {
            return param.ToCharArray().Where(c => c == product).Count();
        }
    }
}
