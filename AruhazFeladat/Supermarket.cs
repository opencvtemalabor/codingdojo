using System.Collections.Generic;

namespace AruhazFeladat
{
    internal class Supermarket
    {
        // Kristóf: a payForTwo és bundle feature lehet, hogy jobb lenne, ha külön
        //  osztályba kerülne. Akkor gyakorlatilag mindenféle kedvezményeket egy közös
        //  interfészen keresztül be lehetne regisztrálni és használni. Pl.
        //  Supermarket.RegisterDiscount(new BundleDiscount("DEF",3));
        private Dictionary<char, int> products;
        private List<char> payForTwo;
        //Stores product bundles and respective discount amounts
        private Dictionary<string, double> bundles;

        public Supermarket()
        {
            products = new Dictionary<char, int>();
            payForTwo = new List<char>();
            bundles = new Dictionary<string, double>();

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

        internal double PayForTwoDiscounted(double sum, string order)
        {
            int counter = 0;

            for (int i = 0; i < payForTwo.Count; i++)
            {
                for (int j = 0; j < order.Length; j++)
                {
                    if (payForTwo[i] == order[j])
                    {
                        counter++;
                        if (counter == 3)
                        {
                            products.TryGetValue(order[j], out int price);
                            sum -= price;
                            counter = 0;
                        }
                    }
                }
                counter = 0;
            }

            return sum;
        }

        //Every product is worth it's position in the alphabet, eg. "A"=1, "Z"=26, "AB"=3...
        internal double Eval(string order)
        {
            double value = InitialPrize(order);

            //PayForTwo preferálása
            if (payForTwo.Count != 0)
            {
                value = PayForTwoDiscounted(value, order);
            }
            else if (bundles.Count != 0)
            {
                value -= ApplyBundles(order);
            }

            return value;
        }

        internal double ApplyBundles(string order)
        {
            double discount = 0;

            List<char> products = new List<char>(order.ToCharArray());
            List<string> bundleproducts = new List<string>(bundles.Keys);

            for (int i = 0; i < bundleproducts.Count; i++)
            {
                if (HasBundle(order, bundleproducts[i]))
                {
                    discount += bundles[bundleproducts[i]];
                    foreach (char c in bundleproducts[i])
                    {
                        products.Remove(c);
                        int j = order.IndexOf(c);
                        order = order.Remove(j, 1);
                    }
                    i--;
                }
            }
            return discount;
        }

        internal bool HasBundle(string order, string bundle)
        {
            //  Már másodjára kell a tömbör készíteni string-ből, majd abból listát...
            //  Lehet, hogy már eleve jobb lenne listaként tárolni? Folyton konvertálgatunk.
            List<char> products = new List<char>(order.ToCharArray());
            foreach (char c in bundle)
            {
                if (products.Contains(c))
                {
                    products.Remove(c);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public void AddBundle(string bundle, double value)
        {
            bundles.Add(bundle, value);
        }

        public void AddPayForTwo(char item)
        {
            payForTwo.Add(item);
        }

        public void AddAllItemsToPayForTwo()
        {
            for (int i = 0; i < 26; i++)
            {
                payForTwo.Add((char)('A' + i));
            }
        }

        public void RemoveFromPayForTwo(char item)
        {
            payForTwo.Remove(item);
        }
    }
}