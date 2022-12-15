﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Order
{
    // ARE THESE THE CORRECT PROPERTIES??
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public DateTime OrderDate { get; set; }
    // status of type order status?
    public DateTime PaymentDate { get; set; }
    public DateTime ShippingDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public OrderItem Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
            Order ID={ID}: {CustomerName},
            Email: {Email}
            Address: {Address}
            Order Date: {OrderDate}
            Status: {} // ADD THE STATUS HERE!!!
            Payment Date: {PaymentDate}
            Shipping Date: {ShippingDate}
            Delivery Date: {DeliveryDate}
            Items: {Items}
            Total Price: {TotalPrice}
        ";
}