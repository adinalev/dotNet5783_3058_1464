using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        ObservableCollection<PO.OrderItem> items = new();

        public CartWindow(BO.Cart cart)
        {
            InitializeComponent();
            items.Clear();
            try
            {
                items = Tools.IEnumerableToObservable(bl.Cart.GetItems(cart));
            }
            catch
            {
                MessageBox.Show("Error", "Update Product Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cartGrid.DataContext = items;
        }

        private void ProductItemView_click(object sender, RoutedEventArgs e)
        {

        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
