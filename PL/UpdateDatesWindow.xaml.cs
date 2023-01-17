using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateDatesWindow.xaml
    /// </summary>
    public partial class UpdateDatesWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.Order order = new BO.Order();
        public UpdateDatesWindow(BO.Order ord, BO.Enums.OrderStatus stat)
        {
            InitializeComponent();
            ID.Text = ord.ID.ToString();
            status.Text = stat.ToString();
            orderDate.Text = ord.OrderDate.ToString();
            shippingDate1.Text = ord.ShippingDate.ToString();
            shippingDate2.Visibility = Visibility.Collapsed;
            deliveryDate.Text = ord.DeliveryDate.ToString();
        }
        public UpdateDatesWindow(BO.Order ord)
        {
            InitializeComponent();
            ID.Text = ord.ID.ToString();
            orderDate.Text = ord.OrderDate.ToString();
            shippingDate2.Text = ord.ShippingDate.ToString();
            shippingDate1.Visibility = Visibility.Collapsed;
            deliveryDate.Text = ord.DeliveryDate.ToString();
        }
    }
}
