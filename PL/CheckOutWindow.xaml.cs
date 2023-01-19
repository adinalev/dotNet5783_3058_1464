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
        public CheckOutWindow()
        {
            InitializeComponent();
        }


       

        private void address_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = new Regex().IsMatch(e.Text);//only gets numbers for instock
        }

        private void email_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-z]+[^0-9]+[^.]+[^@]").IsMatch(e.Text);//only gets numbers for price
        }
        private void name_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-z]+[^A-Z]+").IsMatch(e.Text);//only get letters 
        }


        //private void name_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (name != null && name.Text != "")
        //    {
        //        product.Name = name.Text;
        //    }
        //}

        //private void tprice_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (tprice != null && tprice.Text != "")
        //    {
        //        if (int.TryParse(tprice.Text, out int val))
        //        {
        //            product.Price = val;
        //        }
        //    }
        //}

        //private void tinstock_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (tinstock != null && tinstock.Text != "")
        //    {
        //        if (int.TryParse(tinstock.Text, out int val))
        //        {
        //            product.InStock = val;
        //        }
        //    }
        //}

        //private void tname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    tname.Clear();
        //}

        //private void tprice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    tprice.Clear();
        //}

        //private void tinstock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    tinstock.Clear();
        //}

        //private void uname_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (uname != null && uname.Text != "")
        //    {
        //        product.Name = uname.Text;
        //    }
        //}

        //private void uprice_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (uprice != null && uprice.Text != "")
        //    {
        //        if (int.TryParse(uprice.Text, out int val))
        //        {
        //            product.Price = val;
        //        }
        //    }
        //}

        //private void uinstock_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (uinstock != null && uinstock.Text != "")
        //    {
        //        if (int.TryParse(uinstock.Text, out int val))
        //        {
        //            product.InStock = val;
        //        }
        //    }
        //}

        //private void uname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    uname.Clear();
        //}

        //private void uprice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    uprice.Clear();
        //}

        //private void uinstock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    uinstock.Clear();
        //}
    }
}
