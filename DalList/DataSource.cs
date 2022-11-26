using DO;

namespace Dal // this semicolon doesn't work!! -- should there be a semicolon?
{
    internal static class DataSource
    {
        readonly static Random rand = new Random(); // get a random number
        internal static List<Product> productList { get; set; } = new List<Product> { }; // creating a list of Products
        internal static List<Order> orderList { get; set; } = new List<Order> { }; // creating a list of Orders
        internal static List<OrderItem> orderItemList { get; set; } = new List<OrderItem> { }; // creating a list of OrderItems
        static DataSource() { s_Initialize(); }

        //static DataSource() => s_instance = new DataSource();

        internal static class Config
        {
            //  Add internal static fields to index the first available element in each entity
            // array.These should be initialized with zero values.
            internal static int initialProductNumber = 0; // product number begins at 0
            private static int nextProductNumber = { get => ++previousProductNumber; }
        }

        public int createProduct(Product prod)
        {
            int oldID = productList[productList.Count - 1].ID; // getting the ID of the last product in the list
            prod.ID = oldID++;
            // IS THE ID A RUNNING NUMBER?!?!?!
            productList.Add(prod);
            return prod.ID;
        }

        public int createOrder(Order ord)
        {
            int oldID = orderList[orderList.Count - 1].ID; // getting the ID of the last product in the list
            ord.ID = oldID++;
            // IS THE ID A RUNNING NUMBER?!?!?!
            orderList.Add(ord);
            return ord.ID;
        }

        public int createOrderItem(OrderItem oi)
        {
            int oldID = orderItemList[orderItemList.Count - 1].ID; // getting the ID of the last product in the list
            oi.ID = oldID++;
            // IS THE ID A RUNNING NUMBER?!?!?!
            orderItemList.Add(oi);
            return oi.ID;
        }

        public Product readProduct(int _ID)
        {
            Product prod = productList.Find(x => x.ID == _ID);
            if (prod == null)
                throw new Exception("Product with ID# {0} does not exist", _ID);
            return prod;
        }
        public Order readOrder(int _ID)
        {
            Order ord = orderList.Find(x => x.ID == _ID);
            if (ord == null)
                throw new Exception("Order with ID# {0} does not exist", _ID);
            return ord;
        }
        public OrderItem readOrderItem(int _ID)
        {
            OrderItem oi = orderItemList.Find(x => x.ID == _ID);
            if (oi == null)
                throw new Exception("Order Item with ID# {0} does not exist", _ID);
            return oi;
        }

        public void deleteProduct(int _ID)
        {
            Product prod = productList.Find(x => x.ID == _ID);
            if (prod == null)
                throw new Exception("Product with ID# {0} does not exist", _ID);
            productList.Remove(prod);
        }
        public void deleteOrder(int _ID)
        {
            Order ord = orderList.Find(x => x.ID == _ID);
            if (ord == null)
                throw new Exception("Order with ID# {0} does not exist", _ID);
            orderList.Remove(ord);
        }
        public void deleteOrderItem(int _ID)
        {
            OrderItem oi = orderItemList.Find(x => x.ID == _ID);
            if (oi == null)
                throw new Exception("Order Item with ID# {0} does not exist", _ID);
            orderItemList.Remove(oi);
        }

        public void updateProduct(Product prod)
        {
            int index = productList.FindIndex(x => x.ID == prod.ID);
            if (index != -1)
                productList[index] = prod;
            else
                throw new Exception("Product with ID# {0} does not exist", prod.ID);

        }
        public void updateOrder(Order ord)
        {
            int index = orderList.FindIndex(x => x.ID == ord.ID);
            if (index != -1)
                orderList[index] = ord;
            else
                throw new Exception("Order with ID# {0} does not exist", ord.ID);
        }
        public void updateOrderItem(OrderItem oi)
        {
            int index = orderItemList.FindIndex(x => x.ID == oi.ID);
            if (index != -1)
                orderItemList[index] = oi;
            else
                throw new Exception("Order Item with ID# {0} does not exist", oi.ID);
        }


















        private static void pushProducts(Product prod)
        {
            productList.Add(prod);
            // should take into account if the array is full
        }
        private static void pushOrders(Order ord)
        {
            orderList.Add(ord);
            // should take into account if the array is full
        }
        private static void pushOrderItems(OrderItem oi)
        {
            orderItemList.Add(oi);
            // should take into account if the array is full
        }

        static void s_Initialize()
        {

        }
        private void s_Initialize(Product prod, Order ord, OrderItem oi)
        {
            pushProducts(prod);
            pushOrders(ord);
            pushOrderItems(oi);
        }




    }
}

