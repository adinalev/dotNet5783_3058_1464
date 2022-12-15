using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
public interface IOrderItem
{
    public DO.OrderItem AddOrderItem(OrderItem item); // IS IT SUPPOSED TO RETURN AN INT?!?!??!!!?
    public void DeleteOrderItem(int ID);
    public DO.OrderItem UpdateOrderItem(OrderItem item);
    public DO.OrderItem GetOrderItem(int _id);
    public IEnumerable<DO.OrderItem> GetOrderItemList();

    // modifiers for entity status!!!!!

}
