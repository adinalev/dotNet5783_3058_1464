using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class ProductItem
{
    public int ID { get; set; } 
    public string Name { get; set; }
    public double Price { get; set; }
    public Enums.Category Category { get; set; }
    public int Amount { get; set; }
    public bool InStock { get; set; }
    public override string ToString() => $@"
            ID = {ID}, Name: {Name}
            Price: {Price}
            Category: {Category}
            Amount: {Amount}
            In Stock: {InStock}
        ";

}
