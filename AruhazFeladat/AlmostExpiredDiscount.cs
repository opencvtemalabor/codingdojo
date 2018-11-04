using System.Collections.Generic;

namespace AruhazFeladat
{
    internal class AlmostExpiredDiscount : IDiscount
    {
        private string product;
        protected int priority = 10;

        public AlmostExpiredDiscount(string product)
        {
            this.product = product;
        }

        public string AffectedProducts()
        {
            return product;
        }

        public double CalculateDiscount(List<char> order, Dictionary<char, int> products)
        {
            int sumDiscount = 0;

            foreach (var item in order)
            {
                foreach (var productItem in product)
                {
                    if (item == productItem)
                    {
                        int value;
                        products.TryGetValue(item, out value);

                        sumDiscount += (value / 2);
                    }
                }
            }

            return sumDiscount;
        }

        public int Priority()
        {
            return priority;
        }
    }
}