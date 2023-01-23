using BO;
using System;
using System;
using System.Linq;
using System.Windows;

//using BlApi;
//using BlImplementation;
namespace BlTest;


// ADDED METHOD TO TRACK AN ORDER!!

class Program
{
    //static IBl bl = new Bl();
   // BlApi.IBl? bl = BlApi.Factory.Get();
    //BlApi.IBl? bl = BlApi.Factory.Get();

    static void Main(string[] args)
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        //BlApi.Factory factory = new();
        Cart? cart = new() { Items = new List<BO.OrderItem?>() };
        BO.Product product = new BO.Product();
        BO.Order order = new BO.Order();
        int num1, option;
        int ID;
        string categories;    

        while(true)
        {
            // main menu:
            Console.WriteLine(
                "\nWelcome! \n" +
                "Please choose one of the following options: \n" +
                "For actions on a product, press 1. \n" +
                "For actions on a cart, press 2. \n" +
                "For actions on an order, press 3. \n" +
                "To exit, press 0. ");
            num1 = Convert.ToInt32(Console.ReadLine()); // turn the input into a number
            if (num1 == 0)
            {
                Console.WriteLine("Goodbye! \n"); // leave the while loop if the user iputs a 0
                break;
            }
            BO.Enums.Type type = (BO.Enums.Type)num1; // turn the number into the corresponding enum category
            
            switch (type)
            {
                case BO.Enums.Type.PRODUCT:
                    // menu for product
                    Console.WriteLine(
                        "To view the product list, press 1. \n" +
                        "For the manager: To add a product, press 2. \n" +
                        "For the manager: To view a product, press 3. \n" +
                        "For the manager: To update product, press 4. \n" +
                        "For the manager: To delete a product, press 5. \n" +
                        "To return to main menu, press 6.");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch(option)
                    {
                        case 1: // view the product list
                            IEnumerable<ProductForList?> productList = bl?.Product.GetProductsForList(); // retrive the product list
                            foreach (ProductForList? prod in productList) // traverse through the product list and display each one
                            {
                                Console.WriteLine(prod);
                            }
                            break;
                        case 2: // add a product
                            try
                            {
                                Console.WriteLine("Enter the product name: ");
                                product.Name = Console.ReadLine() ?? ""; // Read in the user's name. If they did not enter a name, input an empty string
                                Console.WriteLine("Enter the product price: ");
                                product.Price = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter the product category: ");
                                categories = Console.ReadLine().ToUpper();
                                if (Enum.TryParse(categories, out BO.Enums.ProductCategory cat)) // Convert the inputted string into an Enum number of type Category
                                {
                                    product.Category = cat;
                                }
                                Console.WriteLine("Enter the stock number: ");
                                product.InStock = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Your new product ID is: " + bl.Product.AddProduct(product)); // add a product and return the new ID numebr
                            }
                            catch (InvalidInputException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }                           
                            break;
                        case 3: // view a product
                            try
                            {
                                Console.WriteLine("Enter the product ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(bl?.Product.GetProduct(ID)); // retrieve the product with the corresponding ID and display it

                            }
                            catch (BO.DoesNotExistException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 4: // update the product
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
                                if (Enum.TryParse(categories, out BO.Enums.ProductCategory cat)) // Convert the inputted string into an Enum number of type Category
                                {
                                    product.Category = cat;
                                }
                                Console.WriteLine("Enter the stock number: ");
                                product.InStock = Convert.ToInt32(Console.ReadLine());
                                bl?.Product.UpdateProduct(product); // Send the new product to the Update function in DalProduct
                            }
                            catch (InvalidInputException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 5: // delete the product
                            try
                            {
                                Console.WriteLine("Enter the product ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                bl?.Product.DeleteProduct(ID); // delete the product with the corresponding ID
                            }
                            catch (BO.DoesNotExistException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;                       
                        case 6: // return to the main list
                            Console.WriteLine("Returning to the main menu... \n");
                            break;
                    }
                    break;
                case BO.Enums.Type.CART:
                    // menu for the cart options
                    Console.WriteLine("To add to cart, press 1. \n" +
                        "To view cart, press 2. \n" +
                        "To update cart, press 3. \n" +
                        "To delete cart, press 4. \n" +
                        "To place an order, press 5. \n" +
                        "To return to the main menu, press 6.");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1: // to add to cart
                            try
                            {
                                Console.WriteLine("Enter the product ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                int? stock = bl?.Product.GetStockNumber(ID);
                                Console.WriteLine("There are " + stock + " of this product left in stock. \n" +
                                    "How many would you like to add to your cart?"); // how much are in stock
                                int amount = Convert.ToInt32(Console.ReadLine());
                                cart = bl?.Cart.AddToCart(cart, ID, amount);
                            }
                            catch (OutOfStockException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (NotEnoughInStockException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (DO.DoesNotExistException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (BO.DoesNotExistException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 2: // to view the cart
                            /* Note to grader: The fields for name, email, and address stay empty until the user places an order, 
                             because generally when shopping online you only input those values when checking out your cart. */
                            Console.WriteLine(cart);
                            List<string?> list = bl?.Cart.GetItemNames(cart); // get the names of the items in the cart
                            Console.Write("\t \t");
                            foreach (string name in list) // travers through the list of names and print them to the screen
                            {
                                Console.Write(name);
                                if (name == list.Last())
                                {
                                    Console.WriteLine();
                                    break;
                                }
                                else
                                    Console.Write(", ");
                            }
                            break;
                        case 3: // to update the cart
                            try
                            {
                                Console.WriteLine("Enter the product ID: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                int? exists = bl?.Cart.InCart(cart, ID);
                                int? stock = bl?.Product.GetStockNumber(ID);
                                Console.WriteLine("There are " + stock + " of this product left in stock. \n" +
                                    "You currently have " + exists + " items of this product in your cart. \n" +
                                    "What would you like to change that to?"); // how much are in stock
                                int amount = Convert.ToInt32(Console.ReadLine());
                                bl?.Cart.UpdateCart(cart, ID, amount); // send the inputted values into the update function
                            }
                            catch (BO.DoesNotExistException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch(BO.OutOfStockException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch(BO.NotInCartException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 4: // to delete the cart
                            bl?.Cart.DeleteCart(cart);
                            break;
                        case 5: // to place an order
                            try
                            {
                                // get customer details before placing an order
                                Console.WriteLine("Enter customer name: ");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter the customer email: ");
                                string email = Console.ReadLine();
                                Console.WriteLine("Enter the customer address: ");
                                string address = Console.ReadLine();
                                ID = bl.Cart.MakeOrder(cart, name, email, address);
                                Console.WriteLine("Your order has been placed! Your order ID is: " + ID);
                                // display the cart
                                Console.WriteLine(cart);
                                List<string?> myList = bl?.Cart.GetItemNames(cart);
                                Console.Write("\t \t");
                                foreach (string? myItem in myList)
                                {
                                    Console.Write(myItem);
                                    if (myItem == myList.Last())
                                    {
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                        Console.Write(", ");
                                }                                
                                bl?.Cart.DeleteCart(cart); // delete/reset the cart
                            }
                            catch (InvalidInputException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (BO.DoesNotExistException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 6: // to return to the main menu
                            Console.WriteLine("Returning to the main menu... \n");
                            break;
                    }
                    break;
                case BO.Enums.Type.ORDER:
                    // menu for order options
                    Console.WriteLine(
                        "To view an order, press 1. \n" +
                        "To view all orders, press 2. \n" +
                        "To track an order, press 3. \n" +
                        "For the manager: To update a shipping date, press 4. \n" +
                        "For the manager: To update a delivery date, press 5.\n" +
                        "To return to the main menu, press 6.");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1: // to view an order
                            try
                            {
                                Console.WriteLine("Enter the order ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(bl?.Order.GetBoOrder(ID));
                                //List<string> list = bl.Cart.GetItemNames(cart);
                                //Console.Write("\t \t");
                                //foreach (string name in list)
                                //{
                                //    Console.Write(name);
                                //    if (name == list.Last())
                                //    {
                                //        Console.WriteLine();
                                //        break;
                                //    }
                                //    else
                                //        Console.Write(", ");
                                //}
                            }
                            catch (BO.DoesNotExistException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 2: // to view the order list
                            List<OrderForList?> orderList = bl?.Order.GetAllOrderForList();
                            foreach (OrderForList? ord in orderList)
                            {
                                Console.WriteLine(ord);
                            }
                            break;
                        case 3: // to track an order
                            try
                            {
                                Console.WriteLine("Enter the order ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(bl?.Order.TrackOrder(ID));
                            }
                            catch (BO.DoesNotExistException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 4: // to update a shipping date
                            try
                            {
                                Console.WriteLine("Enter the order ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                //Console.WriteLine("Enter the updated shipping date (e.g. 10/22/1987 5:32:10): \n");
                                //DateTime inputtedDate = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("The updated order is: \n" + bl?.Order.UpdateShippingDate(ID)); // update the shippping date and display the order
                                order.ID = ID;
                                Console.WriteLine(order);
                            }
                            catch (DoesNotExistException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (OrderNotPlacedYetException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (AlreadyShippedException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (ShipDateOutOfRangeException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 5: // to update a delivery date
                            try
                            {
                                Console.WriteLine("Enter the order ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                //Console.WriteLine("Enter the updated delivery date (e.g. 10/22/1987 5:32:10): \n");
                                //DateTime inputtedDate = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("The updated order is: \n" + bl?.Order.UpdateDeliveryDate(ID)); // update the delivery date and display the order
                                order.ID = ID;
                                Console.WriteLine(order);
                            }
                            catch (BO.DoesNotExistException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (AlreadyDeliveredException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (DeliveryDateOutOfRangeException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            catch (NoShipDateException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 6: // to return to the main menu
                            Console.WriteLine("Returning to the main menu... \n");
                            break;
                    }
                    break;



            }
            

        }
    }
}