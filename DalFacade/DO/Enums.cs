using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
