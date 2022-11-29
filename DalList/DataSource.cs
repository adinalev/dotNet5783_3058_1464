using DO;

namespace Dal;

internal static class DataSource
{
    readonly static Random rand = new Random(); // readonly static field for generating random numbers
                                                //internal static DataSource ds_instance { get; } // property to return a copy of the data

    /* Note to grader: Professor Kelman told us we can skip straight to lists instead of arrays */

    /// <summary>
    /// Creating Lists to hold Products, Orders, and OrderItems
    /// </summary>
    internal static List<Product> productList { get; set; } = new List<Product> { }; // creating a list of Products
    internal static List<Order> orderList { get; set; } = new List<Order> { }; // creating a list of Orders
    internal static List<OrderItem> orderItemList { get; set; } = new List<OrderItem> { }; // creating a list of OrderItems

    //static DataSource() => s_instance = new DataSource();


    /// <summary>
    /// Creating an internal class Config which will help us create and organize 
    /// our Products, Orders, and OrderItems
    /// </summary>
    internal static class Config
    {
        // variables for Product:
        internal const int s_startProductNumber = 100000; // Product ID needs to be 6 digits long, so the first possible ID is 100000
        private static int s_nextProductNumber = s_startProductNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; } // static variable which increases by 1 each time a get function is called on it

        // variables for Order:
        internal const int s_startOrderNumber = 1000; // order numbers are each 4 digits long
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; } // static variable which increases by 1 each time a get function is called on it

        // variables for OrderItem
        internal const int s_startOrderItemNumber = 0;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; } // static variable which increases by 1 each time a get function is called on it
    }

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
        string[] NameOfProduct = { "Shampoo", "Hairbrush", "Advil", "Motrin", "Huggies Diapers", "Wet Wipes", "Eye Drops", "Cheerios", "Mascara", "DayQuil" }


        for (int i = 0; i < 10; i++)
        {
            productList.Add(
                new()
                {
                    ID = Config.NextProductNumber,
                    Name = NameOfProduct[rand.Next(NameOfProduct.Length)], // randomly choosing one of the 10 products listed above
                    Price = rand.Next(20, 100),
                    // FINISH THIS!!!
                });
        }
    }

    /// <summary>
    /// method to initialize our first 10 Orders
    /// </summary>

    private static void PushOrders()
    {
        //MAKE SURE I DID THE CORRECT AMOUNT OF NAMES
        string[] CustomerName = { "Aiden", "Brenda", "Caroline", "David", "Edgar", "Frank", "Greg", "Harry", "Isaac", "Jack", "Kevin", "Larry", "Martin", "Nate", "Oliver", "Pamela", "Quinn", "Rachel", "Sara", "Theo", "Uzi", "Victor", "Warren", "Xander", "Yoel", "Zack" }
        string[] Email = { "aaa@mail.com", "bbb@mail.com", "ccc@mail.com", "ddd@mail.com", "eee@mail.com", "fff@mail.com", "ggg@mail.com", "hh@gamil.com",  "ii@gamil.com", "jj@gamil.com", "kk@gamil.com", "lll@mail.com",
                                 "mmm@mail.com", "ooo@mail.com", "ppp@mail.com", "qqq@mail.com", "rrr@mail.com", "sss@mail.com","ttt@mail.com", "uuu@mail.com", "vvv@mail.com", "www@mail.com", "xxx@mail.com", "yyy@mail.com", "zzz@mail.com" }
        string[] Address = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        for (int i = 0; i < 20; i++)
        {
            Order myOrder = new()
            {
                ID = Config.NextOrderNumber, // was originally nextproduct but changed it -- ok???
                //^ wHY IS IT THE NEXT PRODUCT #???
                CustomerName = CustomerName[rand.Next(CustomerName.Length)],
                Email = Email[rand.Next(Email.Length)],
                Address = Address[rand.Next(Address.Length)],
                OrderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L)),
                ShippingDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue
            };
            // DONT UNDERSTAND THIS:
            myOrder.ShippingDate = myOrder.OrderDate + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L));
            myOrder.DeliveryDate = myOrder.ShippingDate + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L));
            orderList.Add(myOrder);
        }
    }
    private static void PushOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            Product prod = productList[rand.Next(productList.Count)];
            orderItemList.Add(
                new OrderItem
                {
                    ID = Config.NextOrderItemNumber,
                    ProductID = prod.ID,
                    OrderID = rand.Next(Config.s_startOrderNumber, Config.s_startOrderNumber + orderList.Count),
                    Price = prod.Price,
                    Quantity = rand.Next(5)
                    // what's up with the 5??
                });
        }
    }

}
    //  Add internal static fields to index the first available element in each entity
    // array.These should be initialized with zero values.
    /* internal static int initialProductNumber = 0; // product number begins at 0
        private static int nextProductNumber = { get => ++previousProductNumber; } */

    //static variable that counts the amount of products. everytime you initialize an object out of htat class, have iD increment by 1 and set to iD.
    // ID = Config::prodcount++; --> in the default constructor for product 


//    public int createProduct(Product prod)
//    {
//        int oldID = productList[productList.Count - 1].ID; // getting the ID of the last product in the list
//        prod.ID = oldID++;
//        // IS THE ID A RUNNING NUMBER?!?!?!
//        productList.Add(prod);
//        return prod.ID;
//    }

//    public int createOrder(Order ord)
//    {
//        int oldID = orderList[orderList.Count - 1].ID; // getting the ID of the last product in the list
//        ord.ID = oldID++;
//        // IS THE ID A RUNNING NUMBER?!?!?!
//        orderList.Add(ord);
//        return ord.ID;
//    }

//    public int createOrderItem(OrderItem oi)
//    {
//        int oldID = orderItemList[orderItemList.Count - 1].ID; // getting the ID of the last product in the list
//        oi.ID = oldID++;
//        // IS THE ID A RUNNING NUMBER?!?!?!
//        orderItemList.Add(oi);
//        return oi.ID;
//    }

//    public Product readProduct(int _ID)
//    {
//        Product prod = productList.Find(x => x.ID == _ID);
//        if (prod == null)
//            throw new Exception("Product with ID# {0} does not exist", _ID);
//        return prod;
//    }
//    public Order readOrder(int _ID)
//    {
//        Order ord = orderList.Find(x => x.ID == _ID);
//        if (ord == null)
//            throw new Exception("Order with ID# {0} does not exist", _ID);
//        return ord;
//    }
//    public OrderItem readOrderItem(int _ID)
//    {
//        OrderItem oi = orderItemList.Find(x => x.ID == _ID);
//        if (oi == null)
//            throw new Exception("Order Item with ID# {0} does not exist", _ID);
//        return oi;
//    }

//    public void deleteProduct(int _ID)
//    {
//        Product prod = productList.Find(x => x.ID == _ID);
//        if (prod == null)
//            throw new Exception("Product with ID# {0} does not exist", _ID);
//        productList.Remove(prod);
//    }
//    public void deleteOrder(int _ID)
//    {
//        Order ord = orderList.Find(x => x.ID == _ID);
//        if (ord == null)
//            throw new Exception("Order with ID# {0} does not exist", _ID);
//        orderList.Remove(ord);
//    }
//    public void deleteOrderItem(int _ID)
//    {
//        OrderItem oi = orderItemList.Find(x => x.ID == _ID);
//        if (oi == null)
//            throw new Exception("Order Item with ID# {0} does not exist", _ID);
//        orderItemList.Remove(oi);
//    }

//    public void updateProduct(Product prod)
//    {
//        int index = productList.FindIndex(x => x.ID == prod.ID);
//        if (index != -1)
//            productList[index] = prod;
//        else
//            throw new Exception("Product with ID# {0} does not exist", prod.ID);

//    }
//    public void updateOrder(Order ord)
//    {
//        int index = orderList.FindIndex(x => x.ID == ord.ID);
//        if (index != -1)
//            orderList[index] = ord;
//        else
//            throw new Exception("Order with ID# {0} does not exist", ord.ID);
//    }
//    public void updateOrderItem(OrderItem oi)
//    {
//        int index = orderItemList.FindIndex(x => x.ID == oi.ID);
//        if (index != -1)
//            orderItemList[index] = oi;
//        else
//            throw new Exception("Order Item with ID# {0} does not exist", oi.ID);
//    }


















   




//}


