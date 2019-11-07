using System.Collections.Generic;

namespace Aruhaz
{
    internal class SuperShopDiscount : CartProcess
    {
        private Dictionary<int, double> userIDWithSuperShopPoints = new Dictionary<int, double>();

        public void addSuperShopUser(int ID)
        {
            userIDWithSuperShopPoints.Add(ID, 0);
        }

        public override void ApplyCart(Cart cart)
        {
            //TODO
            throw new System.NotImplementedException();
        }
    }
}