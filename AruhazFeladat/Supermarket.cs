using System;
using System.Collections.Generic;

namespace AruhazFeladat
{
    internal class Supermarket
    {
        private Dictionary<char, int> products;
        private List<char> payForTwo;
        public Supermarket()
        {
            products = new Dictionary<char, int>();
            payForTwo = new List<char>();

            for (int i = 0; i < 26; i++)
            {
                products.Add((char)('A' + i), i + 1 );
                payForTwo.Add((char)('A' + i));
            }
        }

        //Every product is worth it's position in the alphabet, eg. "A"=1, "Z"=26, "AB"=3...
        internal double Eval(string v)
        {
            double sum = 0;
            for (int i = 0; i < v.Length; i++){
                if (char.IsLower(v[i]))
                    continue;
                int price;
                if(products.TryGetValue(v[i], out price))
                {
                    sum += price;
                }
            }

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
    }
}