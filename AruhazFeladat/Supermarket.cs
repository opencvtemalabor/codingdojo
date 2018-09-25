using System;
using System.Collections.Generic;

namespace AruhazFeladat
{
    internal class Supermarket
    {
        private Dictionary<char, int> products;
        public Supermarket()
        {
            products = new Dictionary<char, int>();

            for (int i = 0; i < 26; i++)
            {
                products.Add((char)('A' + i), i + 1 );
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
            return sum;
        }
    }
}