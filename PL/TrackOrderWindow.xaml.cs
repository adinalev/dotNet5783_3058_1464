using BO;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for TrackOrderWindow.xaml
    /// </summary>
    public partial class TrackOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.OrderTracking ordTracking = new();
        PO.Cart myCart = new();
        int myID = 0;

        public TrackOrderWindow(BO.OrderTracking orderTracking)
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            DataContext = orderTracking;
            myID = orderTracking.ID;
            ID.Text = orderTracking.ID.ToString();
            status.Text = orderTracking.Status.ToString();
        }

        private void ttracking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ViewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            BO.Order order = new BO.Order();
            try
            {
                order = bl.Order.GetBoOrder(myID);
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Track Order Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            new OrderWindow(order, "overload").ShowDialog();
        }
    }
}

