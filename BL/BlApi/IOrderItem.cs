using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
/// <summary>
/// Interface for the BoEntity "OrderItem"
/// </summary>
public interface IOrderItem
{
    // REDO ALL THE COMMENTING!!!
    //manager funcs 
    public IEnumerable<OrderForList?> GetAllOrderForList();//calls get of DO order list, gets items for each order, and build BO orderItem list
    public Order GetBoOrder(int id);//get order id, check if right, and return BO order using DO order, orderItem, and product
    public Order ShipUpdate(int orderID);//get order number, check if exists, update date in DO order, and return BO order that has been "shipped"
    public Order DeliveredUpdate(int orderID);//get order number, check if exists, update date in DO order, and return BO order that has been "delivered" 
    //public OrderTracking GetOrderTracking(int orderId);//get order id, check if exists, and build strings of dates and status in DO orders


    //public DO.OrderItem AddOrderItem(OrderItem item); // IS IT SUPPOSED TO RETURN AN INT?!?!??!!!?
    //public void DeleteOrderItem(int ID);
    //public DO.OrderItem UpdateOrderItem(OrderItem item);
    //public DO.OrderItem GetOrderItem(int _id);
    //public IEnumerable<DO.OrderItem> GetOrderItemList();

    //// modifiers for entity status!!!!!

}
