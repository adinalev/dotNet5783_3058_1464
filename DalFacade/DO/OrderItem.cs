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
        public int ID { get; set; }
        public int OrderID { get; set; } // Order's identifier
        public int ProductID { get; set; } // Product's identifier
        public double Price { get; set; }
        public int Quantity { get; set; }
        public override string ToString() => $@"
            ID = {ID}
            Order ID: {OrderID}
            Product ID: {ProductID}
            Price Per Unit: {Price}
            Quantity: {Quantity}
        ";

        //public int id   // property
        //{
        //    get { return ID; }   // get method
        //    set { ID = value; }  // set method
        //}

        //// order identifier and product identifier??

        //public double ppu   // property
        //{
        //    get { return PPU; }   // get method
        //    set { PPU = value; }  // set method
        //}

        //public int quantity   // property
        //{
        //    get { return Quantity; }   // get method
        //    set { Quantity = value; }  // set method
        //}
    }
}
