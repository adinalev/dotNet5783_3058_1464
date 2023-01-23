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

        //public TrackOrderWindow()
        //{
        //    InitializeComponent();
        //    DataContext = orderTracking;
        //}

        public TrackOrderWindow(BO.OrderTracking orderTracking)
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            DataContext = orderTracking;
            _id.Text = orderTracking.ID.ToString();
            r_status.Text = orderTracking.Status.ToString();
            //InitializeComponent();
            //BO.OrderTracking track = new BO.OrderTracking();
            //try
            //{
            //    track = bl.Order.GetOrderTracking(ID);
            //}
            //catch (BO.DoesNotExistException exc)
            //{
            //    MessageBox.Show(exc.Message, "Track Order Window", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //orderTracking = PL.Tools.CastBoOTToPo(track);
            //DataContext = orderTracking;
        }

        //public TrackOrderWindow(BO.OrderTracking order)
        //{
        //    InitializeComponent();
        //    ID.Text = order.ID.ToString();
        //    status.Text = order.Status.ToString();
        //    BO.OrderTracking track = new BO.OrderTracking();
        //    try
        //    {
        //        track = bl.Order.GetOrderTracking(order.ID);
        //    }
        //    catch (BO.DoesNotExistException exc)
        //    {
        //        MessageBox.Show(exc.Message, "Track Order Window", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    orderTracking = PL.Tools.CastBoOTToPo(track);
        //    DataContext = orderTracking;
        //}

        //public TrackOrderWindow(int id, PO.Cart cart, BlApi.IBl? b)
        //{
        //    InitializeComponent();
        //    bl = b;//new bl
        //    myCart = cart;
        //    BO.OrderTracking o = new();
        //    try
        //    {
        //        o = bl?.Order.GetOrderTracking(id)!;
        //    }
        //    catch (BO.DoesNotExistException exc)
        //    {
        //        MessageBox.Show(exc.Message, "Track Order Window", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    orderTracking = PL.Tools.CastBoOTToPo(o);//get matching po order tracking
        //    DataContext = orderTracking;//set data context

        //}

        private void ttracking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
