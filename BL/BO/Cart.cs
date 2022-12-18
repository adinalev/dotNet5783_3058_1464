namespace BO;
public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public List<OrderItem> Items { get; set; } // LIST???
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
            Name: {CustomerName}
            Email: {CustomerEmail}
            Address: {CustomerAddress}            
            Items: {Items}
            Total Price: {TotalPrice}
        ";

}
