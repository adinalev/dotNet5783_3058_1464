using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Product
{
    // add properties!!!   
    public int ID { get; set; }  // MAY NEED TO ADD AN EMPTY CTOR FOR EVERYTHING ELSE!!
    public string Name { get; set; }
    public double Price { get; set; }
    public Enums.Category Category { get; set; }
    public int InStock { get; set; }
    public override string ToString() => $@"
            Product ID = {ID}: {Name},
            Category: {Category}
            Price: {Price}
            Amount in stock: {InStock}
        ";
}
