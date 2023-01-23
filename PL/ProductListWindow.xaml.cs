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
            try
            {
                productList = Tools.IEnumerableToObservable(bl!.Product.GetProductsForList());
                orderList = Tools.IEnumerableToObservable(bl.Order.GetAllOrderForList());
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "List view window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CategorySelector.DataContext = Enum.GetValues(typeof(BO.Enums.ProductCategory)); // added this???
            productGrid.DataContext = productList;
            orderGrid.DataContext = orderList;
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
        }



        private void OrdersListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (orderGrid.ItemsSource is BO.OrderForList orderForList)
            {
                BO.Order ord = new BO.Order();
                try
                {
                    ord = bl?.Order.GetBoOrder(orderForList.ID);
                }
                catch (BO.DoesNotExistException exc)
                {
                    MessageBox.Show(exc.Message, "List View Window", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                new OrderWindow(ord).ShowDialog(); 
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
        private void ReturnHome_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}




