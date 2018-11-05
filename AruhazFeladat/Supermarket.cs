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

            // Kristof: ez olyan, mint lentebb a AddAllItemsToPayForTwo,
            //  a Supermarketnek miért van ilyen "default ár" funkciója?
            //  Ha csak a teszeléshez kell, akkor ne itt legyen, hanem a tesztekben.
            //  A Supermarket ne tudjon teszt esetek részleteiről.
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
        // Kristof: itt két üres sor volt egymás után... (nem ad profi megjelenést)


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
        // Kristof: itt két üres sor volt egymás után... (nem ad profi megjelenést)


        public void RegisterDiscount(IDiscount discount)
        {
            discounts.Add(discount);
            discounts.Sort((a, b) => -a.Priority().CompareTo(b.Priority()));
        }

        //Returns the discount from the registered discounts that matches the requirements 
        public IDiscount FindBundle(BundleDiscount bundleDiscount)
        {
            // Kristof: ezt egy Linq-es kifejezéssel szebb lenne lekérni...
            //  bundleDiscount.SingleOrDefault(d => ......... );
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
            // Kristof: ez kicsit bedrótozott funkció. Ha csak a tesztekhez kell, akkor
            //  jobb, ha a tesztek végzik ezt el és a Supermarket nem tud ilyen
            //  alapértelmezett árakról... Ez csak a tesztekre tartozik.
            for (int i = 0; i < 26; i++)
            {
                PayForTwoDiscount tempDiscount = new PayForTwoDiscount((char)('A' + i), products);
                if (FindBundle(tempDiscount) == null)
                    discounts.Add(tempDiscount);
            }
        }

        internal int FinalPrice(string v)
        {
            int value = (int) Eval(v);
            int loyaltyPoints = GetLoyaltyPoints(v);
            value -= loyaltyPoints / 10;
            return value;
        }

        //Removes a "Pay for two get three" discount
        public void RemoveFromPayForTwo(char c)
        {
            discounts.Remove(FindBundle(new PayForTwoDiscount(c, products)));
        }
    }
}