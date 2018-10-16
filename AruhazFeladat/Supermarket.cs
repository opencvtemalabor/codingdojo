using System.Collections.Generic;

namespace AruhazFeladat
{
    internal class Supermarket
    {
        // Kristóf: a payForTwo és bundle feature lehet, hogy jobb lenne, ha külön
        //  osztályba kerülne. Akkor gyakorlatilag mindenféle kedvezményeket egy közös
        //  interfészen keresztül be lehetne regisztrálni és használni. Pl.
        //  Supermarket.RegisterDiscount(new BundleDiscount("DEF",3));
        private Dictionary<char, int> products;
        private List<char> payForTwo;
        //Stores product bundles and respective discount amounts
        private Dictionary<string, double> bundles;
        private bool preferPayForTwoGetThree;

        public Supermarket()
        {
            products = new Dictionary<char, int>();
            payForTwo = new List<char>();
            bundles = new Dictionary<string, double>();
            preferPayForTwoGetThree = false;

            for (int i = 0; i < 26; i++)
            {
                products.Add((char)('A' + i), i + 1);
                payForTwo.Add((char)('A' + i));
            }

            // Kristóf: egy újrahasznosítható osztályban az ilyen bedrótozott dolgok előbb-utóbb
            //  el kell, hogy tűnjenek. Minden áruházban nem lesz A, B és C akció.
            bundles.Add("ABC", 1);
        }

        internal double InitialPrize(string v)
        {
            // Kristóf: a "v" név nem túl beszédes.
            double sum = 0;
            for (int i = 0; i < v.Length; i++)
            {
                if (char.IsLower(v[i]))
                    continue;
                int price;  // Kristóf: C# 7-ben már a lenti sorban is létre lehet hozni: "out int price".
                if (products.TryGetValue(v[i], out price))
                {
                    sum += price;
                }
            }

            return sum;
        }

        internal double PayForTwoDiscounted(double sum, string v)
        {
            // Kristóf: a "v" név nem túl beszédes.
            int counter = 0;
            for (int i = 0; i < payForTwo.Count; i++)
            {
                for (int j = 0; j < v.Length; j++)
                {
                    // Kristóf: code smell... miért kell pont az 'A'-t speciálisan kezelni?
                    if (payForTwo[i] == v[j] && !v[j].Equals('A'))
                    {
                        counter++;
                        if (counter == 3)
                        {
                            int price;  // Kristóf: "out int price"
                            products.TryGetValue(v[j], out price);
                            sum = sum - price;  // Kristóf: -=
                            counter = 0;
                            preferPayForTwoGetThree = true;
                        }
                    }
                }
                counter = 0;
            }

            return sum;
        }

        //Every product is worth it's position in the alphabet, eg. "A"=1, "Z"=26, "AB"=3...
        internal double Eval(string v)
        {
            double value = PayForTwoDiscounted(InitialPrize(v), v);
            if(!preferPayForTwoGetThree)
                value -= ApplyBundles(v);
            return value;
        }

        internal double ApplyBundles(string v)
        {
            // Kristóf: a "v" paraméternév nem túl kifejező...
            double discount = 0;
            List<char> products = new List<char>(v.ToCharArray());
            List<string> bundleproducts = new List<string>(bundles.Keys);

            for (int i = 0; i < bundleproducts.Count; i++)
            {
                // Kristóf: minden egyes iterációban ne hozzunk létre egy újabb stringet a
                //  products-ból. És egyébként is a metódus elején azt meg egy tömbből hoztuk
                //  létre, az itt nem jó ("v.ToCharArray()")?
                if (HasBundle(new string(products.ToArray()), bundleproducts[i]))
                {
                    discount += bundles[bundleproducts[i]];
                    foreach (char c in bundleproducts[i])
                    {
                        products.Remove(c);
                    }
                    i--;
                }
            }

            return discount;
        }

        internal bool HasBundle(string v, string bundle)
        {
            // Kristóf: a "v" név nem utal semmire...
            //  Már másodjára kell a tömbör készíteni string-ből, majd abból listát...
            //  Lehet, hogy már eleve jobb lenne listaként tárolni? Folyton konvertálgatunk.
            List<char> products = new List<char>(v.ToCharArray());
            foreach (char c in bundle)
            {
                if (products.Contains(c))
                {
                    products.Remove(c);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}