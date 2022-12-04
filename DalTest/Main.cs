using DAL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static DO.Enums;

//namespace DalTest;
using DO;
namespace Dal;


internal class Program
{
    static void Main(string[] args)
    {
        Product product = new Product();
        Order order = new Order(); 
        OrderItem item = new OrderItem();
        //DataSource dataSource = new DataSource(); // NEED TO DO THIS!!!!!

        int num1, num2;
        string answer1, answer2;
        DalProduct dProduct = new DalProduct();
        DalOrder dOrder = new DalOrder();
        DalOrderItem dOrderItem = new DalOrderItem();

        while (true)
        {
            Console.WriteLine("For actions on a Product, please press 1.\n"
                + "For actions on an Order, please press 2. \n"
                + "For actions on an Order Item, please press 3." +
                "To exit, please press 0"
                );
            answer1 = Console.ReadLine();
            num1 = Convert.ToInt32(answer1);
            if (num1 == 0) break; // leave the while loop if the user inputs a 0
            Enums.Type type = (Enums.Type)num1;

            Console.WriteLine("To add, please press 1.\n" +
                "To view, please press 2. \n" +
                "To view list, please press 3. \n" +
                "To update, plesae press 4. \n" +
                "To delete, please press 5. \n" +
                "To exit, please press 0. \n");

            num2 = Convert.ToInt32(Console.ReadLine());
            if (num2 == 0) break; // leave the while loop if the user inputs a 0
            Enums.Action action = (Enums.Action)num2;

            switch (type, action)
            {
                // all the actions that can be done on a product:
               
                case (Enums.Type.PRODUCT, Enums.Action.ADD):
                    try
                    {
                        Console.WriteLine("Enter the product name: \n");
                        product.Name = Console.ReadLine() ?? ""; // Read in the user's name. If they did not enter a name, input an empty string
                        Console.WriteLine("Enter the product price: \n");
                        product.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product categroy: \n");
                        product.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the stock number: \n"); // SHOULD WE FIND THIS OUT??
                        product.InStock = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Your new product ID is: " + dProduct.AddProduct(product));
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                
                case (Enums.Type.PRODUCT, Enums.Action.GET):
                    try
                    {
                        Console.WriteLine("Enter the product ID#: \n");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        product = dProduct.ReadProduct(_ID);
                        product.ToString();
                        //^^ARE WE ALLOWED TO DO THAT???
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.PRODUCT, Enums.Action.GETLIST):
                    try
                    {
                        List<Product> list = new List<Product>();
                        list = dProduct.ReadProductList();
                        foreach (Product prod in list)
                        {
                            prod.ToString();
                            //^^ARE WE ALLOWED TO DO THAT???
                            Console.WriteLine("\n \n"); // separate each of the products by a line
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);

                    }
                    break;
                case (Enums.Type.PRODUCT, Enums.Action.UPDATE):
                    try
                    {
                        Console.WriteLine("Enter the product name: \n");
                        product.Name = Console.ReadLine();
                        Console.WriteLine("Enter the product ID: \n");
                        product.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product price: \n");
                        product.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product categroy: \n");
                        product.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the stock number: \n"); // SHOULD WE FIND THIS OUT??
                        product.InStock = Convert.ToInt32(Console.ReadLine());
                        dProduct.UpdateProduct(product);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.PRODUCT, Enums.Action.DELETE):
                    try
                    {
                        Console.WriteLine("Enter the product ID: \n");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        dProduct.DeleteProduct(_ID);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;

                // all the actions that can be taken on an order:
                case (Enums.Type.ORDER, Enums.Action.ADD):
                    try
                    {
                        Console.WriteLine("Enter the customer name: \n");
                        order.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the customer email: \n");
                        order.Email = Console.ReadLine();
                        Console.WriteLine("Enter the customer address: \n");
                        order.Address = Console.ReadLine();
                        // DATES!! FIGURE THEM OUT!!
                        Console.WriteLine("Your new order ID is: " + dOrder.AddOrder(order));
                    }
                    catch(Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDER, Enums.Action.GET):
                    try
                    {
                        Console.WriteLine("Enter the order ID#: \n");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        order = dOrder.ReadOrder(_ID);
                        order.ToString(); 
                        //^^ARE WE ALLOWED TO DO THAT???
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDER, Enums.Action.GETLIST):
                    try
                    {
                        List<Order> list = new List<Order>();
                        list = dOrder.ReadOrderList();
                        foreach (Order ord in list)
                        {
                            ord.ToString();
                            //^^ARE WE ALLOWED TO DO THAT???
                            Console.WriteLine("\n \n"); // separate each of the orders by a line
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);

                    }
                    break;
                case (Enums.Type.ORDER, Enums.Action.UPDATE):
                    try
                    {
                        Console.WriteLine("Enter the order ID#: \n");
                        order.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the customer name: \n");
                        order.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the customer email: \n");
                        order.Email = Console.ReadLine();
                        Console.WriteLine("Enter the customer address: \n");
                        order.Address = Console.ReadLine();
                        // DATES!! FIGURE THEM OUT!!
                        dOrder.UpdateOrder(order);
                    }
                    catch(Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDER, Enums.Action.DELETE):
                    try
                    {
                        Console.WriteLine("Enter the order ID: \n");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        dOrder.DeleteOrder(_ID);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;

                // all the actions that can be taken on an order item:
                case (Enums.Type.ORDERITEM, Enums.Action.ADD):
                    try
                    {
                        Console.WriteLine("Enter the order ID#: \n");
                        item.OrderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product ID#: \n");
                        item.ProductID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the price per unit: \n");
                        item.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the quantity: \n");
                        item.Quantity = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Your new order item ID is: " + dOrderItem.AddOrderItem(item));
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDERITEM, Enums.Action.GET):
                    try
                    {
                        Console.WriteLine("Enter the order item ID#: \n");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        item = dOrderItem.ReadOrderItem(_ID);
                        item.ToString();
                        //^^ARE WE ALLOWED TO DO THAT???
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDERITEM, Enums.Action.GETLIST):
                    try
                    {
                        List<OrderItem> list = new List<OrderItem>();
                        list = dOrderItem.ReadOrderItemList();
                        foreach (OrderItem it in list)
                        {
                            it.ToString();
                            //^^ARE WE ALLOWED TO DO THAT???
                            Console.WriteLine("\n \n"); // separate each of the orders by a line
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);

                    }
                    break;
                case (Enums.Type.ORDERITEM, Enums.Action.UPDATE):
                    try
                    {
                        Console.WriteLine("Enter the order item ID#: \n");
                        item.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the order ID#: \n");
                        item.OrderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product ID#: \n");
                        item.ProductID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the price per unit: \n");
                        item.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the quantity: \n");
                        item.Quantity = Convert.ToInt32(Console.ReadLine());
                        dOrderItem.UpdateOrderItem(item);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDERITEM, Enums.Action.DELETE):
                    try
                    {
                        Console.WriteLine("Enter the order item ID: \n");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        dOrderItem.DeleteOrderItem(_ID);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;




            }

        }
    }
}