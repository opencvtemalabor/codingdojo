using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AruhazFeladat
{
    class PayForTwoDiscount : BundleDiscount
    {

        public PayForTwoDiscount()
        {
            Bundle = "";
            Discount = 0;
        }
        
        public PayForTwoDiscount(char c, Dictionary<char,int> products)
        {
            Bundle = new string(c,3);
            products.TryGetValue(c, out int value);
            Discount = value;
        }
    }
}
