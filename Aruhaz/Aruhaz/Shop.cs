using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aruhaz
{
    class Shop
    {
        protected int price = 0;
        private Dictionary<char, int> Prices = new Dictionary<char, int>();
        private Dictionary<char, string> Discounts = new Dictionary<char, string>();
        private Dictionary<string, double> AmountDiscount = new Dictionary<string, double>();
        private Dictionary<string, int> ComboDiscount = new Dictionary<string, int>();

        public int Total(string param = "")
        {
            if (param == "")
            {
                return 0;
            }
            List<char> discountedItems = new List<char>();
            List<double> discPrice = new List<double>();

            foreach (var discount in Discounts)
            {
                if (param.Contains(discount.Key))
                {
                    string[] discountDetails = discount.Value.Split(' ');
                    int paidFor = Convert.ToInt32(discountDetails[0]);
                    int obtainedAmount = Convert.ToInt32(discountDetails[1]);
                    int amount = CountProductOccurrence(discount.Key, param);
                    if (amount >= obtainedAmount)
                    {
                        int countedAmount = amount / obtainedAmount;
                        price += countedAmount * paidFor * Prices[discount.Key];
                        int i = 0;
                        while (i < countedAmount * obtainedAmount)
                        {
                            param = param.Remove(param.IndexOf(discount.Key), 1);
                            i++;
                        }
                    }
                }
            }

            //Searching if there are possible Amount discounts in the input.
            CheckAmountDiscounts(param, discountedItems, discPrice);
            
            //Checks the possible Combo discounts and subtracts them from the price
            SubtractComboDiscounts(param);

            //Adds the discounted items and normal items to the price
            SummerisePrice(param, discountedItems, discPrice);

            return price;
        }

        private void SubtractComboDiscounts(string param)
        {
            foreach (var combo in ComboDiscount)
            {
                int numberOfOccurrences = CountComboDiscountOccurrence(combo.Key, param);
                for (int i = 0; i < numberOfOccurrences; i++)
                {
                    price += combo.Value;
                }
            }
        }

        private void CheckAmountDiscounts(string param, List<char> discountedItems, List<double> discPrice)
        {
            foreach (var item in AmountDiscount)
            {
                string[] parsedKey = item.Key.Split(" ");
                if (param.Contains(parsedKey[0][0]) && CountProductOccurrence(parsedKey[0][0], param) >= Convert.ToInt32(parsedKey[1]))
                {
                    discountedItems.Add(parsedKey[0][0]);
                    discPrice.Add(item.Value);
                }
            }
        }

        private void SummerisePrice(string param, List<char> discountedItems, List<double> discPrice)
        {
            foreach (char item in param)
            {
                if (discountedItems.Contains(item))
                    price += (int)(Prices[item] * discPrice[discountedItems.FindIndex(find => find == item)]);
                else
                    price += Prices[item];
            }
        }

        public void RegisterAmountDiscount(char product, int amount, double discount)
        {
            AmountDiscount.Add($"{product} {amount}", discount);
        }

        public void Register(char product, int productPrice)
        {
            Prices[product] = productPrice;
        }

        public void RegisterCountDiscount(char product, int paidFor, int obtainedAmount)
        {
            Discounts[product] = $"{paidFor} {obtainedAmount}";
        }

        public int CountProductOccurrence(char product, string param)
        {
            return param.ToCharArray().Where(c => c == product).Count();
        }

        public void RegisterComboDiscount(string comboOfProducts, int priceOfCombo)
        {
            ComboDiscount.Add(comboOfProducts, priceOfCombo);
        }

        // gets the occurrences of Combos in param
        public int CountComboDiscountOccurrence(string comboOfProducts, string param)
        {
            Dictionary<char, int> lettersWithOccurrences = new Dictionary<char, int>();
            int count = 0;
            // searching with ComboLetters one by one in the specified string
            foreach (char letter in comboOfProducts.ToCharArray())
            {
                foreach (char c in param.ToCharArray())
                {
                    if (letter == c)
                        count++;
                }
                if (count == 0) //if any letter doesn't occur, useless to count the others
                {
                    return 0;
                }
                lettersWithOccurrences.Add(letter, count);
                count = 0;
            }

            //searching minimal occurrence among letters
            count = lettersWithOccurrences.First().Value;
            foreach (var v in lettersWithOccurrences)
            {
                if (v.Value < count) count = v.Value;
            }
            return count;
        }

    }
}
