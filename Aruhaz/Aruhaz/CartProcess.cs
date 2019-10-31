using System;
using System.Collections.Generic;
using System.Text;

namespace Aruhaz
{
    abstract class CartProcess
    {
        abstract public void ApplyCart(Cart cart);
        public virtual bool isSame(ComboDiscount cD)
        {
            return false;
        }
    }
}
