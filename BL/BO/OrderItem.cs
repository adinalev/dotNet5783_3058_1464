namespace BO;
public class OrderItem
{
    public int ID { get; set; } 
    public int ProductID { get; set; } // Product's identifier
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string? ProductName { get; set; }
    public double ProductPrice { get; set; }
    public override string ToString() => $@"
            ID = {ID}
            Product ID: {ProductID}
            Product Name: {ProductName}
            Price Per Unit: {Price}
            Quantity: {Quantity}
        ";
}
