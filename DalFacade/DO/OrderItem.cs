using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO
{
    public struct OrderItem
    {
        public int ID;
        // Order's identifier
        // Product's identifier
        public double PPU;
        public int Quantity;
        public override string ToString() => $@"
            Product ID={ID}
            Order: {/*order id*/}
            Product: {/* product ID*/}
            Price Per Unit: {PPU}
            Quantity: {quantity}
        ";

        public int id   // property
        {
            get { return ID; }   // get method
            set { ID = value; }  // set method
        }

        // order identifier and product identifier??

        public double ppu   // property
        {
            get { return PPU; }   // get method
            set { PPU = value; }  // set method
        }

        public int quantity   // property
        {
            get { return Quantity; }   // get method
            set { Quantity = value; }  // set method
        }
    }
}
