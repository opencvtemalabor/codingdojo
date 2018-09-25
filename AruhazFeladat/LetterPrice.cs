namespace AruhazFeladat
{
    internal class LetterPrice
    {
        public char Letter { get; set; }
        public double BasePrice { get; set; }

        public double Disount { get; set; }

        public double CalculatedPrice
        {
            get
            {
                return (1.0 - Disount) * BasePrice;
            }
        }

    }
}