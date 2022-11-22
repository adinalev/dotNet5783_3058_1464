using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Enums
    {
        public override string ToString() => $@"
        {
            Product ID={ID}: {Name},
            category - {Category}
            Price: {Price}
            Amount in stock: {InStock}
        }
    }
}
