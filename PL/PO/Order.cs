using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    internal class Order : INotifyPropertyChanged
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
                    PropertyChanged(this, new PropertyChangedEventArgs("CostumerName"));
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
                    PropertyChanged(this, new PropertyChangedEventArgs("CostumerEmail"));
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
                    PropertyChanged(this, new PropertyChangedEventArgs("CostumerAddress"));
                }
            }
        }


        private DateTime? orderDate;
        public DateTime? OrderDate
        {
            get
            { return orderDate; }
            set
            {
                orderDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OrderDate"));
                }
            }
        }


        private DateTime? shipDate;
        public DateTime? ShipDate
        {
            get
            { return shipDate; }
            set
            {
                shipDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShipDate"));
                }
            }
        }


        private DateTime? deliveryDate;
        public DateTime? DeliveryDate
        {
            get
            { return deliveryDate; }
            set
            {
                deliveryDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DeliveryDate"));
                }
            }
        }

        private DateTime? isDeleted;
        public DateTime? IsDeleted
        {
            get
            { return isDeleted; }
            set
            {
                isDeleted = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsDeleted"));
                }
            }
        }


        private Enums.OrderStatus status;
        public Enums.OrderStatus Status
        {
            get
            { return status; }
            set
            {
                status = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }



        private double totalPrice;
        public double TotalPrice
        {
            get
            { return totalPrice; }
            set
            {
                totalPrice = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
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


    }
}