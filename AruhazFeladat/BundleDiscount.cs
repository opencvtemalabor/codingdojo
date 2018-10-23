using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AruhazFeladat
{
    class BundleDiscount : IDiscount
    {
        private string bundle;
        private double discount;

        public BundleDiscount(string bundle, double discount)
        {
            this.bundle = bundle;
            this.discount = discount;
        }

        public string AffectedProducts()
        {
            return bundle;
        }

        public double CalculateDiscount(List<char> order, Dictionary<char, int> products)
        {
            double sumDiscount = 0;

            while(ContainsBundle(order, bundle))
            {
                sumDiscount += discount;
                foreach (char c in bundle)
                {
                    order.Remove(c);
                }
            }
           
            return sumDiscount;
        }

        private bool ContainsBundle(List<char> order, string bundle)
        {
            //copy
            List<char> orderCopy = new List<char>(order);
           
            foreach (char c in bundle)
            {
                if (orderCopy.Contains(c))
                {
                    orderCopy.Remove(c);
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
