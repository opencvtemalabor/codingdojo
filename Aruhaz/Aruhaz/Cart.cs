using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aruhaz
{
    class Cart : List<CartItem>
    {
        private const double ClubMemberDiscountPercent = 10.0;
        public bool IsClubMember { get; set; }
        public bool payWithSuperShopPoints { get; set; }
        public double globalSubtractDiscount { get; set; }
        

        public Cart(string cartString = "")
        {
            IsClubMember = false;
            SetCartString(cartString);
        }

        public void SetCartString(string cartString = "")
        {
            IsClubMember = false;
            this.Clear();
            foreach (var character in cartString)
            {
                if (Char.IsUpper(character))
                    this.Add(new CartItem(character, 0));

                else if (Char.IsLower(character) && character == 't')
                    IsClubMember = true;
                else if (Char.IsDigit(character))
                {
                    //TODO
                }
                else if (Char.IsLower(character) && character == 'p')
                {
                    payWithSuperShopPoints = true;
                }
                    
            }
        }

        public int GetTotal()
        {
            double price = 0;
            foreach (var itemInCart in this)
            {
                price += itemInCart.CurrentPrice;
            }

            price = IsClubMember ? price * ((100 - ClubMemberDiscountPercent) / 100.0) : price;
            price -= globalSubtractDiscount;
            return (int)Math.Round(price);
        }
    }
}
