namespace Aruhaz
{
    internal class CartItem
    {
        private char name;
        private double currentPrice;

        public CartItem(char character, int currentPrice)
        {
            this.name = character;
            this.currentPrice = currentPrice;
        }
        public CartItem(char character)
        {
            this.name = character;
            currentPrice = 0;
        }

        public static implicit operator CartItem(char b) => new CartItem(b);

        public char Name { get { return name; } }

        public double CurrentPrice{ get { return currentPrice; } set { currentPrice = value; } }
       
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            
            CartItem shopItem = (CartItem)obj;
            return shopItem.Name.Equals(this.name);
        }
    }
}