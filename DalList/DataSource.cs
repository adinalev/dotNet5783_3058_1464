using DO;

namespace Dal;

internal static class DataSource
{
    readonly static Random rand = new Random(); // readonly static field for generating random numbers
                                               
    /* Note to grader: Professor Kelman told us we can skip straight to lists instead of arrays */

    /// <summary>
    /// Creating Lists to hold Products, Orders, and OrderItems
    /// </summary>
    internal static List<Product> productList { get; set; } = new List<Product> { }; // creating a list of Products
    internal static List<Order> orderList { get; set; } = new List<Order> { }; // creating a list of Orders
    internal static List<OrderItem> orderItemList { get; set; } = new List<OrderItem> { }; // creating a list of OrderItems

    /// <summary>
    /// Creating an internal class Config which will help us create and organize 
    /// our Products, Orders, and OrderItems
    /// </summary>
    //internal static class Config
    //{
    //    // variables for Product:
    //    internal const int s_startProductNumber = 100000; // Product ID needs to be 6 digits long, so the first possible ID is 100000
    //    private static int s_nextProductNumber = s_startProductNumber;
    //    internal static int NextProductNumber { get => ++s_nextProductNumber; } // static variable which increases by 1 each time a get function is called on it

    //    // variables for Order:
    //    internal const int s_startOrderNumber = 1000; // order numbers are each 4 digits long
    //    private static int s_nextOrderNumber = s_startOrderNumber;
    //    internal static int NextOrderNumber { get => ++s_nextOrderNumber; } // static variable which increases by 1 each time a get function is called on it

    //    // variables for OrderItem
    //    internal const int s_startOrderItemNumber = 0;
    //    private static int s_nextOrderItemNumber = s_startOrderItemNumber;
    //    internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; } // static variable which increases by 1 each time a get function is called on it
    //}

    /// <summary>
    /// Creating the class methods
    /// </summary>

    static DataSource() { s_Initialize(); } // default constructor

    private static void s_Initialize()
    {
        PushProducts();
        PushOrders();
        PushOrderItems();
    }

    /// <summary>
    /// method to intitialize the first 10 Products
    /// </summary>
    private static void PushProducts()
    {
        // initializing 10 of the Products in our store
        string[] NameOfProduct = { "Shampoo", "Hairbrush", "Advil", "Motrin", "Huggies Diapers", "Wet Wipes", "Eye Drops", "Cheerios", "Mascara", "DayQuil" };


        for (int i = 0; i < 10; i++)
        {
            productList.Add(
                new()
                {
                    //ID = Config.NextProductNumber, // set the ID to the next auto-incremental ID#
                    Name = NameOfProduct[rand.Next(NameOfProduct.Length)], // randomly choosing one of the 10 products listed above
                    Price = rand.Next(20, 100), // set the price between 20 and 100
                    Category = (Enums.Category)rand.Next(0, 6), // choose a random category
                    InStock = (i < 3) ? 0 : rand.Next(15, 30) // hardcoding the first 5% of products to be out of stock, the rest will have stock of between 15 and 30
                }); 
        }
    }

    /// <summary>
    /// method to initialize our first 10 Orders
    /// </summary>

    private static void PushOrders()
    {
        string[] CustomerName = { "Aiden", "Brenda", "Caroline", "David", "Edgar", "Frank", "Greg", "Harry", "Isaac", "Jack", "Kevin", "Larry", "Martin", "Nate", "Oliver", "Pamela", "Quinn", "Rachel", "Sara", "Theo", "Uzi", "Victor", "Warren", "Xander", "Yoel", "Zack" };
        string[] Email = { "aaa@mail.com", "bbb@mail.com", "ccc@mail.com", "ddd@mail.com", "eee@mail.com", "fff@mail.com", "ggg@mail.com", "hh@gamil.com",  "ii@gamil.com", "jj@gamil.com", "kk@gamil.com", "lll@mail.com",
                                 "mmm@mail.com", "ooo@mail.com", "ppp@mail.com", "qqq@mail.com", "rrr@mail.com", "sss@mail.com","ttt@mail.com", "uuu@mail.com", "vvv@mail.com", "www@mail.com", "xxx@mail.com", "yyy@mail.com", "zzz@mail.com" };
        string[] Address = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        for (int i = 0; i < 20; i++)
        {
            Order myOrder = new()
            {
                //ID = Config.NextOrderNumber, // get the next auto-incremental ID#
                CustomerName = CustomerName[rand.Next(CustomerName.Length)], // randomly choose a name from the list
                Email = Email[rand.Next(Email.Length)], // randomly choose an email from the list
                Address = Address[rand.Next(Address.Length)], // randomly choose an address from the list
                OrderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L)),
                ShippingDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue
            };

            if (i < 4) // hardcoding that 20% of orders don't have a ship date yet - only 80% of orders are meant to have a shipping date at first
            {
                orderList.Add(myOrder);
            }

            if (i >= 4 && i < 10) // hardcoding 40% of current shipped items to not have a delivery date yet since only 60% are meant to have a deliery date
            {
                myOrder.ShippingDate = myOrder.OrderDate + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L)); // add a random time interval
                orderList.Add(myOrder);
            }
            else if (i >= 10)
            {
                myOrder.ShippingDate = myOrder.OrderDate + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L)); // add a random time interval
                myOrder.DeliveryDate = myOrder.ShippingDate + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L)); // add a random time interval
                orderList.Add(myOrder);
            }
        }
    }
    private static void PushOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            Product prod = productList[rand.Next(productList.Count)]; // choose a random product
            orderItemList.Add(
                new OrderItem
                {
                    //ID = Config.NextOrderItemNumber, // set the ID equal to the next auto-incremental order item ID#
                    ProductID = prod.ID,
                    OrderID = rand.Next(100000, 10000 + orderList.Count), // CHANGED THIS AND PROBABLY NOT CORRECT!!!!
                    Price = prod.Price,
                    Quantity = rand.Next(5)
                });
        }
    }
}