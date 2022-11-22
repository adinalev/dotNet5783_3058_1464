namespace Dal; // this semicolon doesn't work!!
{
    public class Class1
    {

    }
    internal static class DataSource
    {
        readonly static Random rand = new Random();
        internal static int[] products = new int[50];
        internal static int[] orders = new int[100];
        internal static int[] orderItems = new int[200];
        private static void pushProducts(Product prod) 
        {
            products.Append(prod); 
        // should take into account if the array is full
        }
        private static void pushOrders(Order ord)
        {
            orders.Append(ord);
            // should take into account if the array is full
        }
        private static void pushOrderItems(OrderItem oi)
        {
            orderItems.Append(oi);
            // should take into account if the array is full
        }
        private static void s_Initialize(Product prod, Order ord, OrderItem oi)
        {
            pushProducts(prod);
            pushOrders(ord);
            pushOrderItems(oi);
        }
        
        internal class Config
        {
        //  Add internal static fields to index the first available element in each entity
        // array.These should be initialized with zero values.

            private static 

        }

}

}