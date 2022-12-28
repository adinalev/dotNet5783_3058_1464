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
        public static int itemCounter = 0;

        public OrderItem()
        {
            OrderID = 0;
            ProductID = 0;
            Price = 0;
            Quantity = 0;
        }

        public OrderItem(int _ID)
        {
            ID = _ID;
            OrderID = 0;
            ProductID = 0;
            Price = 0;
            Quantity = 0;
        }

        /// <summary>
        /// unique ID for order item that's auto-incremental
        /// </summary>
        public int ID { get; init; } = ++itemCounter;
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
    }
}
