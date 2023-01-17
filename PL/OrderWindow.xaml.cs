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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.Order order = new BO.Order();
        public OrderWindow()
        {
            InitializeComponent();
            //.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));

        }
        public OrderWindow(BO.Order ord)
        {
            InitializeComponent();
            //StatusBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
            uname.Text = ord.CustomerName.ToString();
            uemail.Text = ord.Email.ToString();
            uaddress.Text = ord.Address.ToString();
            uprice.Text = ord.TotalPrice.ToString();
            status.Text = ord.Status.ToString();        
            ID.Text = ord.ID.ToString();
            foreach (var name in bl?.Order.GetItemNames(ord.ID))
            {

                items.AppendText(name + ", ");

            }
            //items.Text = bl?.Order.GetItemNames(ord.ID).ToList().ToString();
            if (ord.Status != BO.Enums.OrderStatus.BeingProcessed && ord.Status != BO.Enums.OrderStatus.Unknown)
            {
                updateShippingDateButton.Visibility = Visibility.Collapsed;
            }
            if (!(ord.ShippingDate != null && ord.Status != BO.Enums.OrderStatus.Delivered))
            {
                updateDeliveryDateButton.Visibility = Visibility.Collapsed;
            }
            order.ID = ord.ID;
        }

        private void UpdateShippingButton_Click(object sender, RoutedEventArgs e) => new UpdateDatesWindow(order, BO.Enums.OrderStatus.Shipped).Show();

        private void UpdateDeliveryButton_Click(object sender, RoutedEventArgs e) => new UpdateDatesWindow(order).Show();

        //private void SelectStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    BO.Enums.OrderStatus status = (BO.Enums.OrderStatus)StatusBox.SelectedItem; // saves the selected category
        //    order.Status = status;
        //}
    }
}