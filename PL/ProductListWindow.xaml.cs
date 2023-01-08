using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window // changed from Window
    {
        private IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = bl.Product.GetProductsForList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
        }
        private void AddButton_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();


        // constructor to take in an ID
        public ProductListWindow(int ID)
        {

        }
       
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.ProductCategory productCategory = (BO.Enums.ProductCategory)CategorySelector.SelectedItem; // saves the selected category
            if(productCategory == BO.Enums.ProductCategory.NO_CATEGORY) // if the user would like to view all the products
            {
                //try
                //{
                    ProductsListView.ItemsSource = bl.Product.GetProductsForList();
                //}
                //catch (BO.Exceptions exc) // figure this out!!
                //{

                //}
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
                return;
            }
            if (productCategory is BO.Enums.ProductCategory cat)
            {
                //try
                //{
                    // show the filtered list
                    ProductsListView.ItemsSource = bl?.Product?.GetProductsForList()?.Select(x => x.Category == cat); 
                //}
                //catch(BO.Exceptions exc) // figure this out!!
                //{

                //}
            }
            //try
            //{
            // select all the products in the given category
            //IEnumerable<BO.ProductForList> list = from product in bl?.Product.GetProductsForList()
            //                                      where product.Category == productCategory
            //                                      select product;
            ProductsListView.ItemsSource = from product in bl?.Product.GetProductsForList()
                                           where product.Category == productCategory
                                           select product;
            //catch (BO.Exceptions exc) // figure this out!!
            //{

            //}
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DoubleClickEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProductsListView.SelectedItem is BO.ProductForList productForList)
            {
                BO.Product prod = new BO.Product();
                prod = bl.Product.GetProduct(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            //try
            //{
                ProductsListView.ItemsSource = bl?.Product.GetProductsForList();//update list view after add
            //}
            //catch (BO.Exceptions ex)
            //{
            //    new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
                //Console.WriteLine("List View Window\n");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("getting products failed-id is null\n");
                //Console.WriteLine(ex.InnerException?.ToString());
                ////id is null error on screen
            //}
        }

   }
}


