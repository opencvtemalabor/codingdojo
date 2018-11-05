using System.Collections.Generic;

namespace AruhazFeladat
{
    class BundleDiscount : IDiscount
    {
        private string bundle;
        private double discount;
        protected int priority=9;

        public string Bundle
        {
            get => bundle;
            set => bundle = value;
        }

        public double Discount
        {
            get => discount;
            set => discount = value;
        }

        public BundleDiscount()
        {
            bundle = "";
            discount = 0;
        }

        public BundleDiscount(string bundle, double discount)
        {
            this.bundle = bundle;
            this.discount = discount;
        }

        // Kristof: expression body notation?
        //          public string AffectedProducts() => bundle;
        public string AffectedProducts()
        {
            return bundle;
        }

        public double CalculateDiscount(List<char> order, Dictionary<char, int> products)
        {
            double sumDiscount = 0;

            while (ContainsBundle(order, bundle))
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
            // Kristof: ez a "copy" kommentár szerintem felesleges...
            //  Elég triviális, hogy az orderCopy egy másolat...
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

        virtual public int Priority()
        {
            return priority;
        }
    }
}
