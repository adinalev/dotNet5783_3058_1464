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
    /// Interaction logic for TrackOrderWindow.xaml
    /// </summary>
    public partial class TrackOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        PO.OrderTracking orderTracking = new();
        PO.Cart myCart = new();
        public TrackOrderWindow()
        {
            InitializeComponent();
            DataContext = orderTracking;
        }

        public TrackOrderWindow(int ID)
        {
            InitializeComponent();
            BO.OrderTracking track = new BO.OrderTracking();
            try
            {
                track = bl.Order.GetOrderTracking(ID);
            }
            catch(BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Track Order Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            orderTracking = PL.Tools.CastBoOTToPo(track);
            DataContext = orderTracking;
        }

        public TrackOrderWindow(BO.OrderTracking order)
        {
            InitializeComponent();
            ID.Text = order.ID.ToString();
            status.Text = order.Status.ToString();
        }

        private void ttracking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
