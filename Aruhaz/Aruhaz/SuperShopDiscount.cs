using System;
using System.Collections.Generic;

namespace Aruhaz
{
    internal class SuperShopDiscount
    {
        private Dictionary<int, double> userIDWithSuperShopPoints = new Dictionary<int, double>();

        public void AddSuperShopUser(int ID)
        {
            userIDWithSuperShopPoints.Add(ID, 0);
        }

        public double GetUserSuperShopAmount(int ID)
        {
            return userIDWithSuperShopPoints[ID];
        }

        public void ApplyCart(Cart cart, int superShopUser)
        {
            if (cart.payWithSuperShopPoints)
            {
                double superShopPoints = 0;
                userIDWithSuperShopPoints.TryGetValue(superShopUser, out superShopPoints);
                userIDWithSuperShopPoints[superShopUser] = 0;

                cart.globalSubtractDiscount += superShopPoints;
            }

            userIDWithSuperShopPoints[superShopUser] += Math.Round(cart.GetTotal() * 0.01);
        }
    }
}