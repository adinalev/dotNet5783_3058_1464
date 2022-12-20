using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
/// <summary>
/// Interface for the BoEntity "Order"
/// </summary>
public interface IOrder
{
    //manager funcs 
    // SHOULD I INCLUDE METHODS THAT WILL ADD, DELETE, AND UPDATE?!?!?
    public IEnumerable<OrderForList?> GetAllOrderForList();//calls get of DO order list, gets items for each order, and build BO orderItem list
    public Order GetBoOrder(int ID);//get order id, check if right, and return BO order using DO order, orderItem, and product
    public Order ShipUpdate(int orderID);//get order number, check if exists, update date in DO order, and return BO order that has been "shipped"
    public Order DeliveredUpdate(int orderID);//get order number, check if exists, update date in DO order, and return BO order that has been "delivered" 
    //public OrderTracking GetOrderTracking(int orderID);//get order id, check if exists, and build strings of dates and status in DO orders

    //public DO.Order AddOrder(Order order); // IS IT SUPPOSED TO RETURN AN INT?!?!??!!!?
    //public void DeleteOrder(int ID);
    public void UpdateOrder(Order order);
    //public DO.Order GetOrder(int _id);
    //public IEnumerable<DO.Order> GetOrderList();
}