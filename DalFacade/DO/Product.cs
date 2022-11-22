using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Product
    {
        public int ID;
        public string Name;
        public double Price;
        public enum Category { }; // DO THIS
        public int InStock;
        public override string ToString() => $@"
            Product ID = {ID}: {Name},
            category - {Category} // FIX THIS
            Price: {Price}
            Amount in stock: {InStock}
        ";
    }
}
