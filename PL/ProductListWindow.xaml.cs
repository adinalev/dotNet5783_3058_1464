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
                ProductsListView.ItemsSource = bl.Product.GetProductsForList();
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
                prod = bl.Product.GetProduct(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            ProductsListView.ItemsSource = bl?.Product.GetProductsForList(); // update list view after add
            //Close();
        }

   }
}


