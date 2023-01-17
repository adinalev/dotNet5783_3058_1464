using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class ProductItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private int id;
        public int ID
        {
            get
            { return id; }
            set
            {
                id = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ID"));
                }
            }
        }



        private string? productName;
        public string? ProductName
        {
            get
            { return productName; }
            set
            {
                productName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ProductName"));
                }
            }
        }


        private double price;
        public double Price
        {
            get
            { return price; }
            set
            {
                price = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Price"));
                }
            }
        }


        private Enums.ProductCategory category;
        public Enums.ProductCategory Category
        {
            get
            { return category; }
            set
            {
                category = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Category"));
                }
            }
        }

        private bool inStock;
        public bool InStock
        {
            get
            { return inStock; }
            set
            {
                inStock = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("InStock"));
                }
            }
        }


        private int amount;
        public int Amount
        {
            get
            { return amount; }
            set
            {
                amount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
                }
            }
        }

    }
}