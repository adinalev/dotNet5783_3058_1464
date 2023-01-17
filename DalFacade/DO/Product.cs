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
        public static int productCounter = 100000;

        public Product()
        {
            ID = 0;
            Name = "";
            Price = 0;
            InStock = 0;
            Category = Enums.Category.MEDICINE;
        }

        public Product(int _ID) 
        {
            ID = _ID;
            Name = "";
            Price = 0;
            InStock = 0;
            Category = Enums.Category.MEDICINE;
        }

        /// <summary>
        /// unique ID for a product (autoincremental)
        /// </summary>
        public int ID { get; set; } //= productCounter++; // MAY NEED TO ADD AN EMPTY CTOR FOR EVERYTHING ELSE!!
        public string? Name { get; set; }
        public double Price { get; set; }
        public Enums.Category? Category { get; set; } // also supposed to be nullable?
        public int InStock { get; set; }
        public override string ToString() => $@"
            Product ID = {ID}: 
            {Name}
            Category: {Category}
            Price: {Price}
            Amount in stock: {InStock}
        ";
    }
}
