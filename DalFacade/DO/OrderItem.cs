using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct OrderItem
    {
        public int ID;
        // Order's identifier
        // Product's identifier
        public double PPU;
        public int quantity;
        public override string ToString() => $@"
            Product ID={ID}
            Order: {/*order id*/}
            Product: {/* product ID*/}
            Price Per Unit: {PPU}
            Quantity: {quantity}
        ";
    }
}
