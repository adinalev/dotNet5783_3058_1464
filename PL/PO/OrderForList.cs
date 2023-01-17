using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class OrderForList : INotifyPropertyChanged
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

        private string? name;
        public string? Name
        {
            get
            { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CostumerName"));
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

    }
}