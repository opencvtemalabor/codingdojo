using System.Collections.Generic;

namespace AruhazFeladat
{
    public interface IDiscount
    {
        double CalculateDiscount(List<char> order);
    }
}