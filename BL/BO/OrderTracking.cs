using System.Threading.Channels;

namespace BO;
public class OrderTracking
{
    public int ID { get; set; }
    public Enums.OrderStatus? Status { get; set; }
    public List<Tuple<DateTime?, string>>? Tracking { set; get; }

    //public override string ToString() => this.ToStringProperty();

    public override string ToString() => $@"
            id = {ID} /
            status: {Status} 
        ";
}
//changed from lowercase