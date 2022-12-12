using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO
{
    public struct Order
    {
        static int counter = 0;
        public Order()
        {
            ID = ++counter;
            CustomerName = "";
            Email = "";
            Address = "";
            OrderDate = DateTime.MinValue;
            ShippingDate = DateTime.MinValue;
            DeliveryDate = DateTime.MinValue;
        }
        // create another constructor that takes in all the variables besides for ID and then sets the ID according to the next value of counter

        public int ID { get; set; } 
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public override string ToString() => $@"
            Order ID={ID}: {CustomerName},
            Email: {Email}
            Address: {Address}
            Order Date: {OrderDate}
            Shipping Date: {ShippingDate}
            Delivery Date: {DeliveryDate}
        ";
    }
}
