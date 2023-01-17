using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class Cart : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string? customerName;
        public string? CustomerName
        {
            get
            { return customerName; }
            set
            {
                customerName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomerName"));
                }
            }
        }

        private string? customerEmail;
        public string? CustomerEmail
        {
            get
            { return customerEmail; }
            set
            {
                customerEmail = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomerEmail"));
                }
            }
        }


        private string? customerAddress;
        public string? CustomerAddress
        {
            get
            { return customerAddress; }
            set
            {
                customerAddress = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomerAddress"));
                }
            }
        }


        private List<BO.OrderItem?>? orderItems;
        public List<BO.OrderItem?>? OrderItems
        {
            get
            { return orderItems; }
            set
            {
                orderItems = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OrderItems"));
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


    }
}