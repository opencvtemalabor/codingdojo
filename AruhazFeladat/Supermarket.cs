using System;

namespace AruhazFeladat
{
    internal class Supermarket
    {
        public Supermarket()
        {
        }

        //Every product is worth it's position in the alphabet, eg. "A"=1, "Z"=26, "AB"=3...
        internal int Eval(string v)
        {
            int sum = 0;
            for (int i = 0; i < v.Length; i++){
                if (char.IsLower(v[i]))
                    continue;
                sum += (int)v[i] - (int)'A' + 1;
            }
            return sum;
        }
    }
}