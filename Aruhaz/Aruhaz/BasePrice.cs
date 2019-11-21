using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aruhaz
{
    class BasePrice : CartProcess, IDictionary<char,int>
    {
        private Dictionary<char, int> prices=new Dictionary<char, int>();
        public override void ApplyCart(Cart cart)
        {
            foreach (var itemFromCart in cart)
            {
                if (prices.ContainsKey(itemFromCart.Name))
                {
                    int itemPrice;
                    prices.TryGetValue(itemFromCart.Name, out itemPrice);
                    itemFromCart.CurrentPrice = itemPrice;
                }
            }
        }
        public int this[char key] { get => ((IDictionary<char, int>)prices)[key]; set => ((IDictionary<char, int>)prices)[key] = value; }

        public ICollection<char> Keys => ((IDictionary<char, int>)prices).Keys;

        public ICollection<int> Values => ((IDictionary<char, int>)prices).Values;

        public int Count => ((IDictionary<char, int>)prices).Count;

        public bool IsReadOnly => ((IDictionary<char, int>)prices).IsReadOnly;

        public void Add(char key, int value)
        {
            ((IDictionary<char, int>)prices).Add(key, value);
        }

        public void Add(KeyValuePair<char, int> item)
        {
            ((IDictionary<char, int>)prices).Add(item);
        }

        public void Clear()
        {
            ((IDictionary<char, int>)prices).Clear();
        }

        public bool Contains(KeyValuePair<char, int> item)
        {
            return ((IDictionary<char, int>)prices).Contains(item);
        }

        public bool ContainsKey(char key)
        {
            return ((IDictionary<char, int>)prices).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<char, int>[] array, int arrayIndex)
        {
            ((IDictionary<char, int>)prices).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<char, int>> GetEnumerator()
        {
            return ((IDictionary<char, int>)prices).GetEnumerator();
        }

        public bool Remove(char key)
        {
            return ((IDictionary<char, int>)prices).Remove(key);
        }

        public bool Remove(KeyValuePair<char, int> item)
        {
            return ((IDictionary<char, int>)prices).Remove(item);
        }

        public bool TryGetValue(char key, out int value)
        {
            return ((IDictionary<char, int>)prices).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<char, int>)prices).GetEnumerator();
        }
    }
}
