using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AruhazFeladat
{
    class PayForTwoDiscount : IDiscount
    { 
        private List<char> payForTwo;
        private string order;

        public PayForTwoDiscount()
        {
            payForTwo = new List<char>();
        }

        public int getOrderLength()
        {
            return order.Length;
        }

        public double CalculateDiscount(List<char> order, Dictionary<char, int> products)
        {
            int counter = 0;
            double sumDiscount = 0;
            this.order = order.ToString();

            for (int i = 0; i < payForTwo.Count; i++)
            {
                for (int j = 0; j < order.Count; j++)
                {
                    if (payForTwo[i] == order[j])
                    {
                        counter++;
                        if (counter == 3)
                        {
                            products.TryGetValue(order[j], out int price);
                            sumDiscount += price;
                            counter = 0;
                        }
                    }
                }
                counter = 0;
            }

            return sumDiscount;
        }

        public void AddItemsToPayForTwo(char item)
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

        public string AffectedProducts()
        {
            return payForTwo.ToString();
        }
    }
}
