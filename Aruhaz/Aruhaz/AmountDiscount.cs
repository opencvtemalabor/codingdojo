namespace Aruhaz
{
    internal class AmountDiscount : CartProcess
    {
        private char product;
        private int amount;
        private double discount;

        public AmountDiscount(char product, int amount, double discount)
        {
            this.product = product;
            this.amount = amount;
            this.discount = discount;
        }

        public override void ApplyCart(Cart cart)
        {
            var discountAppliesItems = cart.FindAll(x => x.Name.Equals(product));
            if (discountAppliesItems.Count >= amount)
                discountAppliesItems.ForEach(item => item.CurrentPrice *= discount);
        }
       
    }
}