using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class OrderForList
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    // status of type orderstatus --??
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
            ID = {ID}: {CustomerName},
            Status: {} // ADD HERE THE ORDERSTATUS!!
            Amount of Items: {AmountOfItems}
            Total Price: {TotalPrice}
        ";

}
