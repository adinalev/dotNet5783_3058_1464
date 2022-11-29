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
        // make constructors for product, order, and orderitem 
        // create default constructors
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Enums.Category Category { get; set; } // DO THIS
        public int InStock { get; set; }
        public override string ToString() => $@"
            Product ID = {ID}: {Name},
            category - {Category} // FIX THIS
            Price: {Price}
            Amount in stock: {InStock}
        ";

        //default ctor to assign 0s for everything (right below)

        //public Product()
        //{
        //    ID = 0;
        //    Name = "";
        //}

        //public int id // property
        //{
        //    get { return ID; }   // get method
        //    set { ID = value; }  // set method
        //}
        //public string name   // property
        //{
        //    get { return Name; }   // get method
        //    set { Name = value; }  // set method
        //}
        //public double price   // property
        //{
        //    get { return Price; }   // get method
        //    set { Price = value; }  // set method
        //}
        ////public enum category   // property
        ////{
        ////    get { return Category; }   // get method
        ////    set { Category = value; }  // set method
        ////}
        //public int inStock   // property
        //{
        //    get { return InStock; }   // get method
        //    set { InStock = value; }  // set method
        //}


    }
}
