using System.Collections.Generic;

namespace AruhazFeladat
{
    internal class Supermarket
    {
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
                payForTwo.Add((char)('A' + i));
            }

            bundles.Add("ABC", 1);
        }

        internal double InitialPrize(string v)
        {
            double sum = 0;
            for (int i = 0; i < v.Length; i++)
            {
                if (char.IsLower(v[i]))
                    continue;
                int price;
                if (products.TryGetValue(v[i], out price))
                {
                    sum += price;
                }
            }

            return sum;
        }

        internal double PayForTwoDiscounted(double sum, string v)
        {
            int counter = 0;
            for (int i = 0; i < payForTwo.Count; i++)
            {
                for (int j = 0; j < v.Length; j++)
                {
                    if (payForTwo[i] == v[j] && !v[j].Equals('A'))
                    {
                        counter++;
                        if (counter == 3)
                        {
                            int price;
                            products.TryGetValue(v[j], out price);
                            sum = sum - price;
                            counter = 0;
                        }
                    }
                }
                counter = 0;
            }

            return sum;
        }

        //Every product is worth it's position in the alphabet, eg. "A"=1, "Z"=26, "AB"=3...
        internal double Eval(string v)
        {
            return PayForTwoDiscounted(InitialPrize(v), v) - ApplyBundles(v);
        }

        internal double ApplyBundles(string v)
        {
            double discount = 0;
            List<char> products = new List<char>(v.ToCharArray());
            List<string> bundleproducts = new List<string>(bundles.Keys);

            for (int i = 0; i < bundleproducts.Count; i++)
            {
                if (HasBundle(new string(products.ToArray()), bundleproducts[i]))
                {
                    discount += bundles[bundleproducts[i]];
                    foreach (char c in bundleproducts[i])
                    {
                        products.Remove(c);
                    }
                    i--;
                }
            }

            return discount;
        }

        internal bool HasBundle(string v, string bundle)
        {
            List<char> products = new List<char>(v.ToCharArray());
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
    }
}