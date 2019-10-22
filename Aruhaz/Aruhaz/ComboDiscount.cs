using System;
using System.Collections.Generic;
using System.Linq;

namespace Aruhaz
{
    internal class ComboDiscount:CartProcess
    {
        private List<char> comboOfProducts;
        private int priceOfCombo;
        private bool onlyForClubMembers;

        public ComboDiscount(string comboOfProducts, int priceOfCombo, bool onlyForClubMembers = false)
        {
            this.comboOfProducts = comboOfProducts.ToList();
            this.priceOfCombo = priceOfCombo;
            this.onlyForClubMembers = onlyForClubMembers;
        }

        public override void ApplyCart(Cart cart)
        {
            
            Dictionary<char, List<CartItem>> cartItemsThatMatchAChar = new Dictionary<char, List<CartItem>>();
            Dictionary<char, int> numOfCharInComboOfProducts = new Dictionary<char,int>();

            foreach (var charInCombo in comboOfProducts)
            {
                numOfCharInComboOfProducts.TryAdd(charInCombo, comboOfProducts.Count(x => x ==charInCombo));
                cartItemsThatMatchAChar.TryAdd(charInCombo, cart.FindAll(shopItem => shopItem.Name == charInCombo).ToList());
            }
           
            int ocurrenceOfSequenceInCart = int.MaxValue;
            foreach (var oneCharMatch in cartItemsThatMatchAChar)
            {
                int floor=(int)Math.Floor((double)oneCharMatch.Value.Count / numOfCharInComboOfProducts[oneCharMatch.Key]);
                if (floor < ocurrenceOfSequenceInCart) ocurrenceOfSequenceInCart = floor;
            }
            double discountToAdd = (double)priceOfCombo / cart.Count*ocurrenceOfSequenceInCart;
            cart.ForEach(x => x.CurrentPrice += discountToAdd);
        }
    }
}