﻿using System;
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
//using BlApi;
using BO;
//using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private IBl bl = new Bl(); 
        BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdministratorButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();
        private void NewOrderWindow_Click(object sender, RoutedEventArgs e) => new CatalogWindow().Show();

        private void TrackOrder_Click(object sender, RoutedEventArgs e) => new EnterIDWindow().Show();


    }
}