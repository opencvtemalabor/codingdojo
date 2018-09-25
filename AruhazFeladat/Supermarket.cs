using System;
using System.Collections.Generic;

namespace AruhazFeladat
{
    internal class Supermarket
    {
        private Dictionary<char, LetterPrice> products;
        public Supermarket()
        {
            products = new Dictionary<char, LetterPrice>();

            for (int i = 0; i < 26; i++)
            {
                products.Add((char)('A' + i), new LetterPrice { Letter = (char)('A' + i) , BasePrice = i+1, Disount = 0 });
            }
        }

        //Every product is worth it's position in the alphabet, eg. "A"=1, "Z"=26, "AB"=3...
        internal double Eval(string v)
        {
            double sum = 0;
            for (int i = 0; i < v.Length; i++){
                if (char.IsLower(v[i]))
                    continue;
                LetterPrice price;
                if(products.TryGetValue(v[i], out price))
                {
                    sum += price.CalculatedPrice;
                }
            }
            return sum;
        }

        public void AddDiscount(char letter, double discount)
        {

        }
    }
}