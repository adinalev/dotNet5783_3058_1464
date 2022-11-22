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

        public int id // property
        {
            get { return ID; }   // get method
            set { ID = value; }  // set method
        }
        public string name   // property
        {
            get { return Name; }   // get method
            set { Name = value; }  // set method
        }
        public double price   // property
        {
            get { return Price; }   // get method
            set { Price = value; }  // set method
        }
        public enum category   // property
        {
            get { return Category; }   // get method
            set { Category = value; }  // set method
        }
        public int inStock   // property
        {
            get { return InStock; }   // get method
            set { InStock = value; }  // set method
        }


    }
}
