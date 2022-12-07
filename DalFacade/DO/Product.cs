using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO
{
    public struct Product
    {
        public int ID { get; set; } 
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
}
