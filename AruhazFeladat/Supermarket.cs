using System;
using System.Collections.Generic;

namespace AruhazFeladat
{
    internal class Supermarket
    {
        private Dictionary<char, int> products;
        private List<IDiscount> discounts;

        public Supermarket()
        {
            products = new Dictionary<char, int>();
            discounts = new List<IDiscount>();

            for (int i = 0; i < 26; i++)
            {
                products.Add((char)('A' + i), i + 1);
            }
        }

        internal double InitialPrize(string order)
        {
            double sum = 0;
            for (int i = 0; i < order.Length; i++)
            {
                if (char.IsLower(order[i]))
                    continue;

                if (products.TryGetValue(order[i], out int price))
                {
                    sum += price;
                }
            }

            return sum;
        }


        internal double Eval(string order)
        {
            double value = InitialPrize(order);
            List<char> orderList = new List<char>(order);

            foreach (var d in discounts)
            {
                value -= d.CalculateDiscount(orderList, products);
            }

            return value;
        }


        public void RegisterDiscount(IDiscount discount)
        {
            discounts.Add(discount);
            discounts.Sort((a, b) => -a.Priority().CompareTo(b.Priority()));
        }

        //Returns the discount from the registered discounts that matches the requirements 
        public IDiscount FindBundle(BundleDiscount bundleDiscount)
        {
            foreach (var d in discounts)
            {
                if (bundleDiscount.Bundle.Equals(((BundleDiscount)d).Bundle)
                    && bundleDiscount.Discount.Equals(((BundleDiscount)d).Discount))
                    return d;
            }

            return null;
        }

        public int GetLoyaltyPoints(string order)
        {
            return (int)(InitialPrize(order) / 10); // castolás pls
        }

        //Adds a "Pay for two get three" discount for each item
        public void AddAllItemsToPayForTwo()
        {
            for (int i = 0; i < 26; i++)
            {
                PayForTwoDiscount tempDiscount = new PayForTwoDiscount((char)('A' + i), products);
                if (FindBundle(tempDiscount) == null)
                    discounts.Add(tempDiscount);
            }
        }

        internal int FinalPrice(string v)
        {
            return 0;
        }

        //Removes a "Pay for two get three" discount
        public void RemoveFromPayForTwo(char c)
        {
            discounts.Remove(FindBundle(new PayForTwoDiscount(c, products)));
        }
    }
}