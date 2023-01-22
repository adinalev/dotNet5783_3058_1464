using BO;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
        ObservableCollection<PO.ProductForList> productList = new();
        ObservableCollection<PO.OrderForList> orderList = new();

        public ProductListWindow()
        {
            productList.Clear();
            orderList.Clear();
            InitializeComponent();
            //productList.Clear();
            //orderList.Clear();
            try
            {
                productList = Tools.IEnumerableToObservable(bl!.Product.GetProductsForList());
                orderList = Tools.IEnumerableToObservable(bl.Order.GetAllOrderForList());
            }
            catch (BO.DoesNotExistException exc)
            {
                //FIX THIS!!!
                MessageBox.Show(exc.Message, "List view window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CategorySelector.DataContext = Enum.GetValues(typeof(BO.Enums.ProductCategory)); // added this???
            productGrid.DataContext = productList;
            orderGrid.DataContext = orderList;
            //CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
            //ProductsListView.ItemsSource = bl?.Product.GetProductsForList();
            //OrdersListView.ItemsSource = bl?.Order.GetAllOrderForList();
            //StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
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
            if (productCategory == BO.Enums.ProductCategory.NO_CATEGORY || CategorySelector.SelectedItem == null) // if the user would like to view all the products
            {
                try
                {
                    productList = PL.Tools.IEnumerableToObservable(bl?.Product.GetProductsForList()!);//get catalog products from BO
                }
                catch (BO.DoesNotExistException exc)
                {
                    MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                    //new ErrorWindow("Catalog Window\n", exc.Message).ShowDialog();
                }
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
                productGrid.DataContext = productList;
                return;
            }
            if (productCategory is BO.Enums.ProductCategory cat)
            {
                try
                {
                    //show the filtered list
                    productList = PL.Tools.IEnumerableToObservable(from p in bl?.Product.GetProductsForList()//get all products
                                                                   where p.Category == cat
                                                                   select p);
                }
                catch (BO.DoesNotExistException exc)
                {
                    MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                    //new ErrorWindow("Catalog Window\n", exc.Message).ShowDialog();
                }

            }
            productGrid.DataContext = productList;
        }
        //BO.Enums.ProductCategory productCategory = (BO.Enums.ProductCategory)CategorySelector.SelectedItem; // saves the selected category
        //if(productCategory == BO.Enums.ProductCategory.NO_CATEGORY) // if the user would like to view all the products
        //{
        //    ProductsListView.ItemsSource = bl?.Product?.GetProductsForList();
        //    CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
        //    return;
        //}
        //if (productCategory is BO.Enums.ProductCategory cat)
        //{
        //    //show the filtered list
        //    ProductsListView.ItemsSource = bl?.Product?.GetProductsForList()?.Select(x => x.Category == cat);


        //}
        //ProductsListView.ItemsSource = from product in bl?.Product.GetProductsForList()
        //                               where product.Category == productCategory
        //                               select product;


        //private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        private void DoubleClickEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (productGrid.SelectedItem is PO.ProductForList productForList)
            {
                BO.Product prod = new BO.Product();
                prod = bl?.Product.GetProduct(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            try
            {
                productList = PL.Tools.IEnumerableToObservable(bl.Product.GetProductsForList());
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "List View Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            productGrid.DataContext = productList;
            //ProductsListView.ItemsSource = bl?.Product.GetProductsForList(); // update list view after add
            //Close();
        }

        //private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    BO.Enums.OrderStatus status = (BO.Enums.OrderStatus)StatusSelector.SelectedItem; // saves the selected category
        //    if (status == BO.Enums.OrderStatus.ALL)
        //    {
        //        try
        //        {
        //            orderList = PL.Tools.IEnumerableToObservable(bl.Order.GetAllOrderForList());
        //        }
        //        catch (BO.DoesNotExistException exc)
        //        {
        //            MessageBox.Show(exc.Message, "List View Window", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //        //orderGrid.ItemsSource = bl?.Order?.GetAllOrderForList();
        //        StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
        //        return;
        //    }
        //    if (status is BO.Enums.OrderStatus stat)
        //    {
        //        try
        //        {
        //            orderList = PL.Tools.IEnumerableToObservable(bl.Order.GetAllOrderForList());
        //        }
        //        catch (BO.DoesNotExistException exc)
        //        {
        //            MessageBox.Show(exc.Message, "List View Window", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //        //show the filtered list
        //        StatusSelector.ItemsSource = bl?.Order?.GetAllOrderForList()?.Select(x => x?.Status == stat);
        //    }
        //    orderGrid.DataContext = orderList;
        //    //OrdersListView.ItemsSource = from order in bl?.Order.GetAllOrderForList()
        //    //                             where order.Status == status
        //    //                             select order;
        //}



        private void OrdersListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (orderGrid.ItemsSource is BO.OrderForList orderForList)
            {
                BO.Order ord = new BO.Order();
                ord = bl?.Order.GetBoOrder(orderForList.ID);
                new OrderWindow(ord).ShowDialog(); // NOT SURE IF CORRECR WINDOW
            }
            //ProductsListView.ItemsSource = bl?.Product.GetProductsForList(); // update list view after add
        }

        private void ProductItemView_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {           
            if (productGrid.SelectedItem is PO.ProductForList productForList)
            {
                BO.Product prod = new BO.Product();
                prod = bl?.Product.GetProduct(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            try
            {
                productList = PL.Tools.IEnumerableToObservable(bl.Product.GetProductsForList());
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "List View Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            productGrid.DataContext = productList;
            //ProductsListView.ItemsSource = bl?.Product.GetProductsForList(); // update list view after add
            //Close();          
        }
        private void OrderListView_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (orderGrid.ItemsSource is PO.OrderForList orderForList)
            //{
            //    BO.Order ord = new BO.Order();
            //    ord = bl?.Order.GetBoOrder(orderForList.ID);
            //    new OrderWindow(ord).ShowDialog(); // NOT SURE IF CORRECT WINDOW
            //}
            if (orderGrid.SelectedItem is PO.OrderForList orderForList)
            {
                BO.Order ord = new BO.Order();
                ord = bl?.Order.GetBoOrder(orderForList.ID);
                new OrderWindow(ord).ShowDialog();
            }
            try
            {
                orderList = PL.Tools.IEnumerableToObservable(bl.Order.GetAllOrderForList());
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "List View Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            orderGrid.DataContext = orderList;
        }

        private void GroupByStatus_Click(object sender, RoutedEventArgs e)
        {
            RemoveGroupings_Click(sender, e);//remove prev grouping
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(orderGrid.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            SortDescription sortDscription = new SortDescription("Status", ListSortDirection.Ascending);
            view.GroupDescriptions.Add(groupDescription);
            view.SortDescriptions.Add(sortDscription);
            GroupByStatus.IsEnabled = false;
        }

        private void RemoveGroupings_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(orderList);
            view.GroupDescriptions.Clear();
            view.SortDescriptions.Clear();
            GroupBack.IsEnabled = false;
        }


    }
}




