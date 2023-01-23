using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CheckOutWindow.xaml
    /// </summary>
    public partial class CheckOutWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        PO.Cart cart = new();
        public CheckOutWindow()
        {
            InitializeComponent();
        }

        public CheckOutWindow(PO.Cart myCart)
        {
            InitializeComponent();
            cart.Price = myCart.Price;
            foreach(BO.OrderItem item in myCart.orderItems)
            {
                cart?.orderItems?.Append(item);
            }
        }

        private void email_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-z]+[^0-9]+[^.]+[^@]").IsMatch(e.Text);//only gets numbers for price
        }
        private void name_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-z]+[^A-Z]+").IsMatch(e.Text);//only get letters 
        }



        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (name != null && name.Text != "")
            {
                cart.CustomerName = name.Text;
            }
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (name != null && name.Text != "")
            {
                cart.CustomerEmail = name.Text;
            }
        }
        private void address_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (name != null && name.Text != "")
            {
                cart.CustomerAddress = name.Text;
            }
        }

        private void PlaceOrderButton_Click(object sender, RoutedEventArgs e)
        {
            string cname = name.Text;
            string cemail = email.Text;
            string caddress = address.Text;
            BO.Cart myCart = new BO.Cart();
            myCart = PL.Tools.CastPoCToBo(cart);
            try
            {
                bl?.Cart.MakeOrder(myCart, cname, cemail, caddress);
            }
            catch(BO.AlreadyExistsException exc)
            {
                MessageBox.Show(exc.Message, "Checkout Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.InvalidInputException exc)
            {
                MessageBox.Show(exc.Message, "Checkout Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.TooManyProductsException exc)
            {
                MessageBox.Show(exc.Message, "Checkout Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Checkout Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BO.NotEnoughInStockException exc)
            {
                MessageBox.Show(exc.Message, "Checkout Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show("Your order has been placed! \n Thank you for shopping with us!");
            Close();
        }

    }
}
