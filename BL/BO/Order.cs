namespace BO;
public class Order
{
    public int ID { get; set; } // CHNAGED FROM INIT TO SET
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime? OrderDate { get; set; } 
    public Enums.OrderStatus? Status { get; set; }
    public DateTime? PaymentDate { get; set; }  
    public DateTime? ShippingDate { get; set; } 
    public DateTime? DeliveryDate { get; set; } 
    public List<OrderItem?>? Items { get; set; } = new List<OrderItem?>();
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
            Order ID={ID}: 
            Customer Name: {CustomerName}
            Email: {Email}
            Address: {Address}
            Order Date: {OrderDate}
            Status: {Status}
            Payment Date: {PaymentDate}
            Shipping Date: {ShippingDate}
            Delivery Date: {DeliveryDate}
            Total Price: {TotalPrice}
            Items:";
}
