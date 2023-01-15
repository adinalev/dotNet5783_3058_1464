using DO;

namespace Dal;

internal static class DataSource
{
    readonly static Random rand = new Random(); // readonly static field for generating random numbers

    /// <summary>
    /// Creating Lists to hold Products, Orders, and OrderItems
    /// </summary>
    internal static List<Product?> productList { get; set; } = new List<Product?> { }; // creating a list of Products
    internal static List<Order?> orderList { get; set; } = new List<Order?> { }; // creating a list of Orders
    internal static List<OrderItem?> orderItemList { get; set; } = new List<OrderItem?> { }; // creating a list of OrderItems

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
                    //ID = 0,
                    ID = Product.productCounter++, // ADDED THIS
                    Name = NameOfProduct[rand.Next(NameOfProduct.Length)], // randomly choosing one of the 10 products listed above
                    Price = rand.Next(20, 100),
                    Category = (Enums.Category)rand.Next(1, 7),
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
        string[] Address = { "28 Derech Hevron", "545 Dam Hamacabbim Street", "609 Lincoln Highway", "11 Beit Hadfus Street", "48 Kanfei Nesharim Road", "123 Semame Street", "50 King George Street", "40 Duvdevani Street",
            "12 Washington Road", "23 Naftali Street", "54 Raritan Avenue", "177 Main Street", "865 Herzog Boulevard", "24 Leslie Street", "99 HaPisga Street", "402 North Eighth Road", "10 Downing Street",
            "199 HaRakevet Street", "1600 Pennsylvania Avenue", "50 HaUman Street" };

        for (int i = 0; i < 20; i++)
        {
            Order myOrder = new()
            {
                //ID = 0, // was originally nextproduct but changed it -- ok???
                ID = Order.orderCounter++, // ADDED THIS
                CustomerName = CustomerName[rand.Next(CustomerName.Length)],
                Email = Email[rand.Next(Email.Length)],
                Address = Address[rand.Next(Address.Length)],
                OrderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L)),
                ShippingDate = null,
                DeliveryDate = null
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
            Product prod = (Product)productList[rand.Next(productList.Count)]; // ADDED A CAST
            Order ord = (Order)orderList[rand.Next(orderList.Count)]; // ADDED A CAST
            orderItemList.Add(
                new OrderItem
                {
                    ProductID = prod.ID,
                    OrderID = ord.ID, 
                    Price = prod.Price,
                    Quantity = rand.Next(5)
                });
        }
    }
}