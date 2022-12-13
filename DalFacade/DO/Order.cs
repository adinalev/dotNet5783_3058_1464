﻿using System;
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
        static int orderCounter = 0;

        //public Order()
        //{
        //    CustomerName = "";
        //    Email = "";
        //    Address = "";
        //    OrderDate = DateTime.MinValue;
        //    ShippingDate = DateTime.MinValue;
        //    DeliveryDate = DateTime.MinValue;
        //}

        /// <summary>
        /// unique ID for order that's auto-incremental
        /// </summary>
        public int ID { get; init; } = ++orderCounter;
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
