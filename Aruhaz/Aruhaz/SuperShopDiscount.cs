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

        public double GetUserSuperShopAmoun(int ID)
        {
            return userIDWithSuperShopPoints[ID];
        }

        public void AddSuperShopPoints(Cart cart, int ID)
        {
            userIDWithSuperShopPoints[ID] += Math.Round(cart.GetTotal()*0.01);
        }
    }
}