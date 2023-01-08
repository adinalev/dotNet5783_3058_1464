﻿using BlApi;
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
using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for AppendWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        private BO.Product product = new BO.Product();
        public ProductWindow() // constructor
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
            updateProductButton.Visibility = Visibility.Collapsed;//update invisible 
            uinstock.Visibility = Visibility.Collapsed;
            uprice.Visibility = Visibility.Collapsed;
            uname.Visibility = Visibility.Collapsed;
            ID.Text = bl.Product.GetNextID().ToString();
        }

        public ProductWindow(Product prod)
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
            addProductButton.Visibility = Visibility.Collapsed;//add invisible
            updateProductButton.Visibility = Visibility.Visible;//show update
            tinstock.Visibility = Visibility.Collapsed;
            tprice.Visibility = Visibility.Collapsed;
            tname.Visibility = Visibility.Collapsed;
            uinstock.Text = prod.InStock.ToString();
            uprice.Text = prod.Price.ToString();
            CategoryBox.SelectedItem = prod.Category;
            uname.Text = prod.Name;
            ID.Text = prod.ID.ToString();
            //prod.ID.ToString();
        }
        private void tid_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for id
        }

        private void tinstock_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for instock
        }

        private void tprice_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for price
        }
        private void tname_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-z]+[A-Z]+").IsMatch(e.Text);//only get letters 
        }

        private void uinstock_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            uinstock.Text = product.InStock.ToString();
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for instock
        }

        private void uprice_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            uprice.Text = product.Price.ToString();
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for price
        }
        private void uname_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            uname.Text = product.Name;
            e.Handled = new Regex("[^a-z]+[A-Z]+").IsMatch(e.Text);//only get letters 
        }

        private void tname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tname != null && tname.Text != "")
            {
                product.Name = tname.Text;
            }
        }

        private void tprice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tprice != null && tprice.Text != "")
            {
                if (int.TryParse(tprice.Text, out int val))
                {
                    product.Price = val;
                }
                //else error
            }
        }

        private void tinstock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tinstock != null && tinstock.Text != "")
            {
                if (int.TryParse(tinstock.Text, out int val))
                {
                    product.InStock = val;
                }
                //else error
            }
        }

        private void tname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tname.Clear();
        }

        private void tprice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tprice.Clear();
        }

        private void tinstock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tinstock.Clear();
        }

        private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.ProductCategory productCategory = (BO.Enums.ProductCategory)CategoryBox.SelectedItem; // saves the selected category
            product.Category = productCategory;
        }


        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.AddProduct(product);
            Close();
        }

        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.UpdateProduct(product);
            Close();
        }
    }
}

