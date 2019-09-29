using System;
using Xunit;

namespace Aruhaz
{
    public class InstantiationTest
    {
        [Fact]
        public void EmptyCartTotalIsZero()
        {
            var shop = new Shop();

            Assert.Equal(0, shop.Total());
        }
       
    }
}
