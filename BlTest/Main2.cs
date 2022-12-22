using DO;
using System;
using BO;
using BlApi;
using BlImplementation;
//using BL;
namespace BlTest;

// LOOK AT PART ABOUT ADDING BLTEST -- WHEN ADDING A PROJECT REFERENCE I 
// ADDED BL AS ONE OF THEM EVEN THOUGH IT DIDNT SAY TO BUT OTHERWISE IT WASN'T WORKING

// EVERY SINGLE EXCEPTION NEEDS TO BE CHANGED AND FIXED!!!!

class Program
{
    static IBl bl = new Bl();
    static void Main(string[] args)
    {
        Cart? cart = new() { Items = new List<BO.OrderItem?>() };
        BO.Product? product = new BO.Product();
        BO.Order? order = new BO.Order();
        int num1, option, ID;
        string categories;

        while(true)
        {
            Console.WriteLine("Welcome! \n" +
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
                    // do I add in for the manager???
                    Console.WriteLine(                   
                        "To add a product, press 1. \n" +
                        "To view a product, press 2. \n" +
                        "To view the product list, press 3. \n" +
                        "To update product, press 4. \n" +
                        "To delete a product, press 5. \n" +
                        "To return to main menu, press 6. \n");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch(option)
                    {
                        case 1: // add a product
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
                                bl.Product.AddProduct(product);
                            }
                            catch (InvalidInputException exc) 
                            {
                                Console.WriteLine(exc.Message);
                            }                           
                            break;
                        case 2: // view a product
                            try
                            {
                                Console.WriteLine("Enter the product ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(bl.Product.GetProduct(ID));

                            }
                            catch (BO.DoesNotExistException exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 3: // view the product list
                            IEnumerable<ProductForList> productList = bl.Product.GetProductsForList();
                            foreach (ProductForList prod in productList)
                            {
                                Console.WriteLine(prod);
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
                                bl.Product.UpdateProduct(product); // Send the new product to the Update function in DalProduct
                            }
                            catch (InvalidInputException exc) // FIX THIS EXCEPTION!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 5: // delete the product
                            try
                            {
                                Console.WriteLine("Enter the product ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                bl.Product.DeleteProduct(ID);
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
                    Console.WriteLine("To add to cart, press 1. \n" +
                        "To view a cart, press 2. \n" +
                        "To update a cart, press 3. \n" +
                        "To delete a cart, press 4. \n" +
                        "To place an order, press 5. \n" +
                        "To return to the main menu, press 6. \n");
                    // PROBLEM: CANT IDENTIFY A SPECIFIC CART AND THERE'S NO LIST OF CARTS TO VIEW THEM THROUGH
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            try
                            {
                                Console.WriteLine("Enter the product ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                //Console.WriteLine("Enter the customer name: ");
                                //cart.CustomerName = Console.ReadLine();
                                Console.WriteLine("Enter the customer email: ");
                                cart.CustomerEmail = Console.ReadLine();
                                // TRYING TO ADD TO A SPECIFIC CART BUT THAT DOESNT EXIST!!!!
                                bl.Cart.AddToCart(cart, ID);
                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 2:
                            try
                            {

                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 3:
                            try
                            {

                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 4:
                            try
                            {

                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 5:
                            try
                            {

                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 6:
                            Console.WriteLine("Returning to the main menu... \n");
                            break;
                    }
                    break;
                case BO.Enums.Type.ORDER:
                    Console.WriteLine("To add an order, press 1. \n" +
                        "To view an order, press 2. \n" +
                        "To view an order list press 3. \n" +
                        "To update an order, press 4. \n" +
                        "To delete an order, press 5. \n" +
                        "To track an order, press 6.  \n" +
                        "To return to the main menu, press 7. \n"
                        );
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            try
                            {

                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 2:
                            try
                            {
                                Console.WriteLine("Enter the order ID#: ");
                                ID = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(bl.Order.GetBoOrder(ID));
                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 3:
                            try
                            {
                                IEnumerable<OrderForList> orderList = bl.Order.GetAllOrderForList();
                                foreach (OrderForList ord in orderList)
                                {
                                    Console.WriteLine(ord);
                                }
                                break;
                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 4:
                            try
                            {

                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 5:
                            try
                            {

                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                        case 6:
                            try
                            {

                            }
                            catch (Exception exc) // FIX THE EXCEPTION!!!!
                            {
                                Console.WriteLine(exc.Message);
                            }
                            break;
                    }
                    break;



            }
            

        }
    }
}