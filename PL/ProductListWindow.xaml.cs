using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
//using BlApi;
//using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window // changed from Window
    {
        //private IBl bl = new Bl();
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = bl?.Product.GetProductsForList();
            OrdersListView.ItemsSource = bl?.Order.GetAllOrderForList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().Show();
            Close();
        }
       
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.ProductCategory productCategory = (BO.Enums.ProductCategory)CategorySelector.SelectedItem; // saves the selected category
            if(productCategory == BO.Enums.ProductCategory.NO_CATEGORY) // if the user would like to view all the products
            {
                ProductsListView.ItemsSource = bl?.Product?.GetProductsForList();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
                return;
            }
            if (productCategory is BO.Enums.ProductCategory cat)
            {
                //show the filtered list
                ProductsListView.ItemsSource = bl?.Product?.GetProductsForList()?.Select(x => x.Category == cat);


            }
            ProductsListView.ItemsSource = from product in bl?.Product.GetProductsForList()
                                           where product.Category == productCategory
                                           select product;
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DoubleClickEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProductsListView.SelectedItem is BO.ProductForList productForList)
            {
                BO.Product prod = new BO.Product();
                prod = bl?.Product.GetProduct(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            ProductsListView.ItemsSource = bl?.Product.GetProductsForList(); // update list view after add
            //Close();
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.OrderStatus status = (BO.Enums.OrderStatus)StatusSelector.SelectedItem; // saves the selected category
            if (status == BO.Enums.OrderStatus.ALL)
            {
                OrdersListView.ItemsSource = bl?.Order?.GetAllOrderForList();
                StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
                return;
            }
            if (status is BO.Enums.OrderStatus stat)
            {
                //show the filtered list
                OrdersListView.ItemsSource = bl?.Order?.GetAllOrderForList()?.Select(x => x?.Status == stat);
            }
            OrdersListView.ItemsSource = from order in bl?.Order.GetAllOrderForList()
                                           where order.Status == status
                                           select order;
        }

        private void OrdersListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (OrdersListView.SelectedItem is BO.OrderForList orderForList)
            {
                BO.Order ord = new BO.Order();
                ord = bl?.Order.GetBoOrder(orderForList.ID);
                new OrderWindow(ord).ShowDialog(); // NOT SURE IF CORRECR WINDOW
            }
            //ProductsListView.ItemsSource = bl?.Product.GetProductsForList(); // update list view after add
        }
    }


   }



