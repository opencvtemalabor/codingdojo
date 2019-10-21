using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aruhaz
{
    class CountDiscount : CartProcess
    {

        private char productName;
        private int payCount;
        private int aquireCount;

        public CountDiscount(char productName, int payCount, int aquireCount)
        {
            this.productName = productName;
            this.payCount = payCount;
            this.aquireCount = aquireCount;
        }

        public override void ApplyCart(Cart cart)
        {

            if (cart.Contains(productName))
            {
                
                int occurrence = cart.Count(f => f.Name==productName);
                if (occurrence >= aquireCount)
                {
                    int numOfDiscountSequence = occurrence / aquireCount;
                    foreach (var item in cart.FindAll(x => x.Name == productName).Take(numOfDiscountSequence).ToList())
                    {
                        item.CurrentPrice = 0;

                    }
                }

            }

        }
    }
}
