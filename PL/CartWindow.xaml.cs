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
        PO.Cart poCart = new();
        BO.Cart boCart = new BO.Cart();
        int productID;

        public CartWindow()
        {
            InitializeComponent();
        }

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
                MessageBox.Show("Error", "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cartGrid.DataContext = items;
            poCart.OrderItems = cart.Items; // MLOWERCASE VS. UPPERCASE
            poCart.Price = cart.TotalPrice; // MLOWERCASE VS. UPPERCASE
            TotalPrice.Text = cart.TotalPrice.ToString();
        }

        private void ProductItemView_click(object sender, RoutedEventArgs e)
        {

        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            new CheckOutWindow(poCart).Show();
            Close();
        }
        private void ReturnHome_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cartGrid.SelectedItem is PO.OrderItem orderItem)
                {
                    //myCart = PL.Tools.CastPoCToBo(poCart);
                    poCart = PL.Tools.CastBoCToPo(bl.Cart.IncreaseCart(PL.Tools.CastPoCToBo(poCart), orderItem.ProductID));
                }
                //poCart = bl.Cart.IncreaseCart(poCart, cartGrid.SelectedItem.ID);
            }
            catch(BO.NotEnoughInStockException exc)
            {
                MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //cartGrid.DataContext = items;

            Close();
            new CartWindow(PL.Tools.CastPoCToBo(poCart)).Show();
        }
        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cartGrid.SelectedItem is PO.OrderItem orderItem)
                {
                    poCart = PL.Tools.CastBoCToPo(bl.Cart.DecreaseCart(PL.Tools.CastPoCToBo(poCart), orderItem.ProductID));
                }
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //cartGrid.DataContext = items;

            new CartWindow(PL.Tools.CastPoCToBo(poCart)).Show();
            Close();
        }
    }
}
