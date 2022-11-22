using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Order
    {
        public int ID;
        public string Name;
        public string Email;
        public string Address;
        public DateTime Creation;
        public DateTime Shipping;
        public DateTime Delivery;
        public override string ToString() => $@"
            Order ID={ID}: {Name},
            Email: {Email}
            Address: {Address}
            Creation Date: {Creation}
            Shipping Date: {Shipping}
            Delivery Date: {Delivery}
        ";

        public string name   // property
        {
            get { return Name; }   // get method
            set { Name = value; }  // set method
        }

        public int id   // property
        {
            get { return ID; }   // get method
            set { ID = value; }  // set method
        }

        public string email   // property
        {
            get { return Email; }   // get method
            set { Email = value; }  // set method
        }
        public string address   // property
        {
            get { return Address; }   // get method
            set { Address = value; }  // set method
        }
        public DateTime creation   // property
        {
            get { return Creation; }   // get method
            set { Creation = value; }  // set method
        }
        public DateTime shipping   // property
        {
            get { return Shipping; }   // get method
            set { Shipping = value; }  // set method
        }
        public DateTime delivery   // property
        {
            get { return Delivery; }   // get method
            set { Delivery = value; }  // set method
        }
    }
}
