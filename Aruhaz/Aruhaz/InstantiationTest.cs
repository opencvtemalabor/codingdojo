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
            int expected = 0;

            Assert.Equal(expected, shop.Total());
        }
    }
}
