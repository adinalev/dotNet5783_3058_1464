﻿using BO;
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
    /// Interaction logic for EnterIDWindow.xaml
    /// </summary>
    public partial class EnterIDWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public EnterIDWindow()
        {
            InitializeComponent();
            IDInput.Text = "";
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            int ID = 0;
            BO.Order order = new BO.Order();
            try
            {
                ID = int.Parse(IDInput.Text);//save the entered id as a number
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Wrong ID number entered", "Enter Order ID Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                order = bl?.Order.GetBoOrder(ID);
            }
            catch (BO.DoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Order List Window", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            OrderTracking orderTracking = new OrderTracking();
            orderTracking.ID = order.ID;
            orderTracking.Status = order.Status;
            Close();//close current window                    
            new TrackOrderWindow(orderTracking).ShowDialog();
        }
        private void tid_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for id
        }

        void clickBackBtn(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();
            Close();//close this window
        }
        private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) OKButton_Click(sender, e);
        }
    }
}
