using Dal;
//using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static DO.Enums;

namespace DalTest;

internal class Program
{
    static void Main(string[] args)
    {
        Product product = new Product();
        Order order = new Order(); 
        OrderItem item = new OrderItem();
        DataSource dataSource = new DataSource(); // NEED TO DO THIS!!!!!

        int num1, num2;
        string answer1, answer2;

        while (true)
        {
            Console.WriteLine("For actions on a Product, please press 1.\n"
                + "For actions on an Order, please press 2. \n"
                + "For actions on an Order Item, please press 3." +
                "To exit, please press 0"
                );
            answer1 = Console.ReadLine();
            num1 = Convert.ToInt32(answer1);
            Enums.Type type = (Enums.Type)num1;

            Console.WriteLine("To add, please press 1.\n" +
                "To view, please press 2. \n" +
                "To view list, please press 3. \n" +
                "To update, plesae press 4. \n" +
                "To delete, please press 5. \n");
            num2 = Convert.ToInt32(Console.ReadLine());
            Enums.Action action = (Enums.Action)num2;

            switch (type, action)
            {
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
                        //MADE AN ID NUMBER (DALLIST PRODUCT AND CONFIG)
                    }
                    catch (Exception)
                    {
                        // HAVE TO WRITE THE THROWS -- THEY'RE INCLUDED IN THE ADD FUNCTION FOR CREATING AN ID ABOVE^^^
                        Console.WriteLine("The list is full\n");
                    }
                    break;
                case (Enums.Type.PRODUCT, Enums.Action.GET):
                    try
                    {

                    }

                    break;
            }

        }
    }
}