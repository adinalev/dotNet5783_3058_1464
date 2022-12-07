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
        int counter = 0;
        foreach (OrderItem item2 in DataSource.orderItemList)
        {
            if (item2.OrderID == item.OrderID)
                counter++;
        }
        if (counter > 4)
        {
            throw new Exception("Cannot have more than 4 types of products per order! \n");
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
        OrderItem item = DataSource.orderItemList.Find(x => x.ID == _ID); // find an order item with a matching ID#
        if (item.ID == 0)
        {
            // if there is no matching ID#
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
        // traverse through the order item list and find an order item with a matching ID#
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.ID == _ID)
            {
                // saved the ID# of the matching order item
                ind = DataSource.orderItemList.IndexOf(item);
                break;
            }
        }
        OrderItem DelItem = DataSource.orderItemList[ind]; // saved the item that you located above       
        DataSource.orderItemList.Remove(DelItem); // delete that item from the order item list
    }

    /// <summary>
    /// public method to update an Order Item
    /// </summary>
    public void UpdateOrderItem(OrderItem item)
    {
        int _ID = item.ID;
        OrderItem OldItem = DataSource.orderItemList.Find(x => x.ID == _ID); // find an order item with a matching ID#
        if (OldItem.ID == 0)
        {
            throw new Exception("Order item does not exist!\n");
        }
        item.ProductID = OldItem.ProductID;
        item.OrderID = OldItem.OrderID;
        int index = DataSource.orderItemList.IndexOf(OldItem);
        DataSource.orderItemList[index] = item;
    }

    public void UpdateItemWithIDs(OrderItem item)
    {
        //OrderItem myItem = new OrderItem();
        int index = 0;
        foreach (OrderItem it in DataSource.orderItemList)
        {
            if (it.ProductID == item.ProductID)
            {
                if (it.OrderID == item.OrderID)
                {
                    item.ID = it.ID;
                    index = DataSource.orderItemList.IndexOf(it);
                }
            }
        }
        DataSource.orderItemList[index] = item;       
    }

    /// <summary>
    /// public method to GET/SET an Order Item given the product ID and order ID.
    /// </summary>
    public OrderItem ReadOrderItem(int prodID, int ordID)
    {
        OrderItem myItem = new OrderItem();
        myItem.ID = -1;
        // traverse through the the order item list and find an order item with a matching product ID# and order ID#
        foreach(OrderItem item in DataSource.orderItemList)
        {
            if (item.ProductID == item.ProductID)
            {
                if (item.OrderID == item.OrderID)
                    myItem = item;
            }
        }
        // if a matching ID was not found
        if (myItem.ID == -1)
        {
            throw new Exception("Order item does not exist!\n");
        }
        return myItem;
    }

    /// <summary>
    /// public method to get the list of items in an order, given the order ID
    /// </summary>
    public List<OrderItem> ReadOrderItemList(int ordID)
    {
        List<OrderItem> myList = new List<OrderItem>();
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.OrderID == ordID)
                myList.Add(item); 
        }
        return myList;
    }
}