using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Order
    {
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

        //public string name   // property
        //{
        //    get { return Name; }   // get method
        //    set { Name = value; }  // set method
        //}

        //public int id   // property
        //{
        //    get { return ID; }   // get method
        //    set { ID = value; }  // set method
        //}

        //public string email   // property
        //{
        //    get { return Email; }   // get method
        //    set { Email = value; }  // set method
        //}
        //public string address   // property
        //{
        //    get { return Address; }   // get method
        //    set { Address = value; }  // set method
        //}
        //public DateTime creation   // property
        //{
        //    get { return Creation; }   // get method
        //    set { Creation = value; }  // set method
        //}
        //public DateTime shipping   // property
        //{
        //    get { return Shipping; }   // get method
        //    set { Shipping = value; }  // set method
        //}
        //public DateTime delivery   // property
        //{
        //    get { return Delivery; }   // get method
        //    set { Delivery = value; }  // set method
        //}
    }
}
