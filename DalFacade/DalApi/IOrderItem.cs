using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem GetByIDs(int _productID, int _orderID);
    public void UpdateByIDs(OrderItem item);
    public IEnumerable<OrderItem?> GetAllByID(int ID); // DO WE NEED THIS?!?!?!?
}