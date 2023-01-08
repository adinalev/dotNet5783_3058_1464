using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;
using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl = new Bl(); 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

        // When the button to add a product is pressed, open the append window to add a product
        //private void AddButton_Click(object sender, RoutedEventArgs e) => new AppendWindow().Show();

        //private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

    }
}
