namespace BO;
public class OrderItem
{
    public int ID { get; set; } 
    public int ProductID { get; set; } // Product's identifier
    public double Price { get; set; }
    public int Quantity { get; set; }
    public override string ToString() => $@"
            ID = {ID}
            Product ID: {ProductID}
            Price Per Unit: {Price}
            Quantity: {Quantity}
        ";
}
