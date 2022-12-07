using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static DO.Enums;
using DO;
namespace Dal;

internal class Program
{
    static void Main(string[] args)
    {
        Product product = new Product();
        Order order = new Order(); 
        OrderItem item = new OrderItem();

        int num1, num2;
        string answer1, categories;
        DalProduct dProduct = new DalProduct(); // creating an instance of DalProduct in order to call its methods
        DalOrder dOrder = new DalOrder(); // creating an instance of DalOrder in order to call its methods
        DalOrderItem dOrderItem = new DalOrderItem(); // creating an instance of DalOrderItem in order to call its methods

        while (true)
        {
            Console.WriteLine("For actions on a Product, please press 1.\n"
                + "For actions on an Order, please press 2. \n"
                + "For actions on an Order Item, please press 3.\n" +
                "To exit, please press 0"
                );
            answer1 = Console.ReadLine();
            num1 = Convert.ToInt32(answer1);
            if (num1 == 0) break; // leave the while loop if the user inputs a 0
            Enums.Type type = (Enums.Type)num1; // convert the number into an Enum of type Type

            Console.WriteLine("To add, please press 1.\n" +
                "To view, please press 2. \n" +
                "To view list, please press 3. \n" +
                "To update, please press 4. \n" +
                "To delete, please press 5. \n" +
                "To exit, please press 0. ");

            num2 = Convert.ToInt32(Console.ReadLine());
            if (num2 == 0) break; // leave the while loop if the user inputs a 0
            Enums.Action action = (Enums.Action)num2; // convert the number into an Enum of type Action

            switch (type, action)
            {
                // all the actions that can be done on a product:
               
                case (Enums.Type.PRODUCT, Enums.Action.ADD):
                    try
                    {
                        Console.WriteLine("Enter the product name: ");
                        product.Name = Console.ReadLine() ?? ""; // Read in the user's name. If they did not enter a name, input an empty string
                        Console.WriteLine("Enter the product price: ");
                        product.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product category: ");
                        categories = Console.ReadLine().ToUpper();
                        if (Enum.TryParse(categories, out Enums.Category cat)) // Convert the inputted string into an Enum number of type Category
                        {
                            product.Category = cat;
                        }
                        Console.WriteLine("Enter the stock number: ");
                        product.InStock = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Your new product ID is: " + dProduct.AddProduct(product)); // Add the product to the list and get the new ID
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                
                case (Enums.Type.PRODUCT, Enums.Action.GET):
                    try
                    {
                        Console.WriteLine("Enter the product ID#: ");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        product = dProduct.ReadProduct(_ID); // Calling the GET method in DalProduct to retrieve the product
                        Console.WriteLine(product);
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
                        list = dProduct.ReadProductList(); // Retrieve the product list by calling the GETLIST method in DalProduct
                        foreach (Product prod in list) // Traverse through the list to print each product
                        {
                            Console.WriteLine(prod);
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
                        Console.WriteLine("Enter the product ID: ");
                        product.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product name: ");
                        product.Name = Console.ReadLine();                     
                        Console.WriteLine("Enter the product price: ");
                        product.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product category: ");
                        categories = Console.ReadLine().ToUpper();
                        if (Enum.TryParse(categories, out Enums.Category cat)) // Convert the inputted string into an Enum number of type Category
                        {
                            product.Category = cat;
                        }
                        Console.WriteLine("Enter the stock number: ");
                        product.InStock = Convert.ToInt32(Console.ReadLine());
                        dProduct.UpdateProduct(product); // Send the new product to the Update function in DalProduct
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.PRODUCT, Enums.Action.DELETE):
                    try
                    {
                        Console.WriteLine("Enter the product ID: ");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        dProduct.DeleteProduct(_ID); // Delete the product with the given ID#
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
                        order.ID = 0; // ADDED THIS!!!
                        Console.WriteLine("Enter the customer name: ");
                        order.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the customer email: ");
                        order.Email = Console.ReadLine();
                        Console.WriteLine("Enter the customer address: ");
                        order.Address = Console.ReadLine();
                        Console.WriteLine("Your new order ID is: " + dOrder.AddOrder(order)); // Adding a new order to the list and get the new order ID#
                    }
                    catch(Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDER, Enums.Action.GET):
                    try
                    {
                        Console.WriteLine("Enter the order ID#: ");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        order = dOrder.ReadOrder(_ID); // Retrieve the desired order using the GET method in DalOrder
                        Console.WriteLine(order);
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
                        foreach (Order ord in list) // traverse through the order list and print each of them to the screen
                        {
                            Console.WriteLine(ord); 
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
                        Console.WriteLine("Enter the order ID#: ");
                        order.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the customer name: ");
                        order.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the customer email: ");
                        order.Email = Console.ReadLine();
                        Console.WriteLine("Enter the customer address: ");
                        order.Address = Console.ReadLine();
                        dOrder.UpdateOrder(order); // Send the new order to the update method in DalOrder
                    }
                    catch(Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDER, Enums.Action.DELETE):
                    try
                    {
                        Console.WriteLine("Enter the order ID: ");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        dOrder.DeleteOrder(_ID); // delete the order
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
                        Console.WriteLine("Enter the order ID#: ");
                        item.OrderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the product ID#: ");
                        item.ProductID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the price per unit: ");
                        item.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the quantity: ");
                        item.Quantity = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Your new order item ID is: " + dOrderItem.AddOrderItem(item)); // Add a new order item and receive the new Order Item ID#
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDERITEM, Enums.Action.GET):
                    try
                    {
                        Console.WriteLine("Press 1 if you know the order item ID#. \n"
                            + "Press 2 if you know the order ID# and product ID#. "); // Give the user two options of how to retrieve the order item
                        int option = Convert.ToInt32(Console.ReadLine());
                        if (option == 1) // if they know the order item ID#
                        {
                            Console.WriteLine("Enter the order item ID# ");
                            int _ID = Convert.ToInt32(Console.ReadLine());
                            item = dOrderItem.ReadOrderItem(_ID); // use the GET method in order item that takes in the order item ID# 
                            Console.WriteLine(item);
                        }
                        if (option == 2) // if the know the product ID# and order ID#
                        {                           
                            Console.WriteLine("Enter the Order ID#: ");
                            int ordID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the Product ID#: ");
                            int prodID = Convert.ToInt32(Console.ReadLine());
                            item = dOrderItem.ReadOrderItem(prodID, ordID); // use the GET method in order item that takesn in the product ID# and order ID#
                            Console.WriteLine(item);
                        }
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
                        foreach (OrderItem it in list) // traverse through the order item list and print each one to the screen
                        {
                            Console.WriteLine(it);
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
                        Console.WriteLine("Press 1 if you know the order item ID#. \n"
                            + "Press 2 if you know the order ID# and product ID#. ");
                        int option = Convert.ToInt32(Console.ReadLine());
                        if (option == 1)
                        {
                            Console.WriteLine("Enter the order item ID#: ");
                            item.ID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the price per unit: ");
                            item.Price = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the quantity: ");
                            item.Quantity = Convert.ToInt32(Console.ReadLine());
                            dOrderItem.UpdateOrderItem(item); // Send the updated order item to the update method in DalOrderItem
                        }
                        if (option == 2)
                        {
                            Console.WriteLine("Enter the order ID#: ");
                            item.OrderID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the product ID#: ");
                            item.ProductID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the price per unit: ");
                            item.Price = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the quantity: ");
                            item.Quantity = Convert.ToInt32(Console.ReadLine());
                            dOrderItem.UpdateItemWithIDs(item); // Send the updated order item to the update method in DalOrderItem
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: {0}", exc);
                    }
                    break;
                case (Enums.Type.ORDERITEM, Enums.Action.DELETE):
                    try
                    {
                        Console.WriteLine("Enter the order item ID: ");
                        int _ID = Convert.ToInt32(Console.ReadLine());
                        dOrderItem.DeleteOrderItem(_ID); // delete the order item
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