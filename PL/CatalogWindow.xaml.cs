using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        ObservableCollection<PO.ProductItem> catalog = new();
        BO.Product product = new BO.Product();
        PO.Cart cart = new();

        public CatalogWindow()
        {
            InitializeComponent();
            catalog.Clear();
            try
            {
                catalog = Tools.IEnumerableToObservable(bl!.Product.GetCatalog());
            }
            catch(BO.DoesNotExistException exc)
            {
                new ErrorWindow("Catalog Window\n", exc.Message).ShowDialog();
            }
            catalogGrid.DataContext = catalog;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
            //ProductsListView.ItemsSource = catalog;
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.ProductCategory productCategory = (BO.Enums.ProductCategory)CategorySelector.SelectedItem; // saves the selected category
            if (productCategory == BO.Enums.ProductCategory.NO_CATEGORY || CategorySelector.SelectedItem == null) // if the user would like to view all the products
            {
                try
                {
                    catalog = PL.Tools.IEnumerableToObservable(bl?.Product.GetCatalog()!);//get catalog products from BO
                }
                catch(BO.DoesNotExistException exc)
                {
                    MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                    //new ErrorWindow("Catalog Window\n", exc.Message).ShowDialog();
                }
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
                catalogGrid.DataContext = catalog;
                return;
            }
            if (productCategory is BO.Enums.ProductCategory cat)
            {
                try
                {
                    //show the filtered list
                    catalog = PL.Tools.IEnumerableToObservable(from p in bl?.Product.GetCatalog()//get all products
                                                               where p.Category == cat
                                                               select p);
                }
                catch (BO.DoesNotExistException exc)
                {
                    MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                    //new ErrorWindow("Catalog Window\n", exc.Message).ShowDialog();
                }

            }
            catalogGrid.DataContext = catalog;
        }

        // FIX THIS!! ONLY DID THIS TO BE ABLE TO RUN THE PROGRAM!!
        private void ProductItemView_click(object sender, MouseButtonEventArgs e)
        {
            if (catalogGrid.SelectedItem is BO.ProductItem productItem)
            {
                new ProductWindow(product).ShowDialog();
            }
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //FOR SURE GOING TO NEED TO CHANGE THIS!!
        private void DoubleClickEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (ProductsListView.SelectedItem is BO.ProductForList productForList)
            //{
            //    BO.Product prod = new BO.Product();
            //    prod = bl?.Product.GetProduct(productForList.ID);
            //    new ProductWindow(prod).ShowDialog();
            //}
            //ProductsListView.ItemsSource = bl?.Product.GetProductsForList(); // update list view after add
            //Close();
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (catalogGrid.SelectedItem is PO.ProductItem productItem)
            {
                try
                {
                    cart = PL.Tools.CastBoCToPo(bl!.Cart.AddToCart(PL.Tools.CastPoCToBo(cart), productItem.ID, 1));//add the selected product to cart
                    MessageBox.Show("Product successfully added to your cart.");
                }
                catch (BO.DoesNotExistException exc)
                {
                    MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                    //new ErrorWindow("Cart Window Window", ex.Message).ShowDialog();
                }
                catch(BO.OutOfStockException exc)
                {
                    MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ViewCart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(Tools.CastPoCToBo(cart)).Show();
            Close();
        }

        private void ReturnHome_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
