using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class OrderForList
{
    public int ID { get; init; }
    public string CustomerName { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
            ID = {ID}: {CustomerName},
            Status: {Status} 
            Amount of Items: {AmountOfItems}
            Total Price: {TotalPrice}
        ";

}
