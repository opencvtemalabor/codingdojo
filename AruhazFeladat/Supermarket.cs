﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AruhazFeladat
{
    internal class Supermarket
    {
        // Kristóf: a payForTwo és bundle feature lehet, hogy jobb lenne, ha külön
        //  osztályba kerülne. Akkor gyakorlatilag mindenféle kedvezményeket egy közös
        //  interfészen keresztül be lehetne regisztrálni és használni. Pl.
        //  Supermarket.RegisterDiscount(new BundleDiscount("DEF",3));
        private Dictionary<char, int> products;
        //Stores product bundles and respective discount amounts
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


        //Every product is worth it's position in the alphabet, eg. "A"=1, "Z"=26, "AB"=3...
        internal double Eval(string order)
        {
            double value = InitialPrize(order);
            List<char> orderList = new List<char>(order);
            string affectedProducts = "";
            
            foreach(var d in discounts)
            {
                if (d.GetType().Equals(typeof(PayForTwoDiscount)))
                {
                    value -= d.CalculateDiscount(orderList, products);
                    affectedProducts += d.AffectedProducts();
                }
                
            }

            if (discounts.Count != 0)
            {
                foreach(var d in discounts)
                {
                    if (!affectedProducts.Any( x => d.AffectedProducts().Contains(x)))
                    {
                        value -= d.CalculateDiscount(orderList, products);
                    }            
                }

            }

            return value;
        }


        public void RegisterDiscount(IDiscount discount)
        {
            discounts.Add(discount);
        }
    }
}