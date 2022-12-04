using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// public method to add an Order Item
    /// </summary>
    public int AddOrderItem(OrderItem item)
    {
        //case 1: Order item does not exist yet, needs to be initialized
        if (item.ID == 0)
        {
            item.ID = DataSource.Config.NextOrderItemNumber;
            DataSource.orderItemList.Add(item);
            return item.ID;
        }

        // case 2: Order item already exists, throw an exception
        int index = DataSource.orderItemList.IndexOf(item);
        if (index != -1)
        {
            throw new Exception("Order item exists already!\n");
        }
        else
        // Order item is initialized but it's not in the list yet
        {
            DataSource.orderItemList.Add(item);
            return item.ID;
        }
    }

    /// <summary>
    /// public method to read an Order Item
    /// </summary>
    public OrderItem ReadOrderItem(int _ID)
    {
        OrderItem item = DataSource.orderItemList.Find(x => x.ID == _ID);
        if (item.ID != _ID)
        {
            throw new Exception("Order item does not exist!\n");
        }
        return item;
    }

    /// <summary>
    /// public method to read the Order Item list
    /// </summary>
    public List<OrderItem> ReadOrderItemList()
    {
        return DataSource.orderItemList.ToList();
    }

    /// <summary>
    /// public method to delete an Order Item
    /// </summary>
    public void DeleteOrderItem(int _ID)
    {
        int ind = 0;
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.ID == _ID)
            {
                ind = DataSource.orderItemList.IndexOf(item);
                break;
            }
        }
        OrderItem DelItem = DataSource.orderItemList[ind];
        DataSource.orderItemList.Remove(DelItem);
    }

    /// <summary>
    /// public method to update an Order Item
    /// </summary>
    public void UpdateOrderItem(OrderItem item)
    {
        int _ID = item.ID;
        OrderItem OldItem = DataSource.orderItemList.Find(x => x.ID == _ID);
        if (item.ID != OldItem.ID)
        {
            throw new Exception("Order item does not exist!\n");
        }
        int index = DataSource.orderItemList.IndexOf(OldItem);
        DataSource.orderItemList[index] = item;
    }

    /// <summary>
    /// public method to GET/SET an Order Item given the product ID and order ID.
    /// </summary>
    public OrderItem GetFromID(int prodID, int ordID)
    {
        //OrderItem item = DataSource.orderItemList.Find(x => (x.ProductID == prodID) && (x => x.OrderID == ordID));
        OrderItem myItem = new OrderItem();
        foreach(OrderItem item in DataSource.orderItemList)
        {
            if(item.ProductID == prodID && item.OrderID == ordID)
                myItem = item;
        }
        return myItem;
    }

    /// <summary>
    /// public method to get the list of items in an order, given the order ID
    /// </summary>
    public List<OrderItem> GetListFromID(int ordID)
    {
        List<OrderItem> myList = new List<OrderItem>();
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.OrderID == ordID)
                myList.Add(item); // SHOULD WE USE APPEND HERE???
        }
        return myList;
    }

   // public 
}