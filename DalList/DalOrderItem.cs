using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
using DO;
namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// public method to add an Order Item
    /// </summary>
    public int Add(DO.OrderItem item)
    {
        //case 1: Order item does not exist yet, needs to be initialized
        if (item.ID == 0)
        {
            OrderItem myItem = new OrderItem();
            item.ID = OrderItem.itemCounter++;
            myItem.ProductID = item.ProductID;
            myItem.OrderID = item.OrderID;
            myItem.Price = item.Price;
            myItem.Quantity = item.Quantity;
            DataSource.orderItemList.Add(item);
            return item.ID;
        }

        // case 2: Order item already exists, throw an exception
        int index = DataSource.orderItemList.IndexOf(item);
        if (index != -1)
        {
            throw new AlreadyExistsException();
        }
        int counter = 0;
        //DataSource.orderItemList.ForEach(item2 => item2?.OrderID == item.OrderID  )
        foreach (DO.OrderItem item2 in DataSource.orderItemList)
        {
            if (item2.OrderID == item.OrderID)
                counter++;
        }
        if (counter > 4)
        {
            throw new TooManyProductsException();
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
    public DO.OrderItem? GetByID(int _ID)
    {
        DO.OrderItem? item = DataSource.orderItemList.Find(x => x?.ID == _ID); // find an order item with a matching ID#
        if (item == null)
        {
            // if there is no matching ID#
            throw new DoesNotExistException();
        }
        return item;
    }

    /// <summary>
    /// public method to read the Order Item list
    /// </summary>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter)
    {
        if (filter == null)//select whole list
        {
            return from item in DataSource.orderItemList
                   where item!=null
                   select item;
        }
        return from myItem in DataSource.orderItemList//select with filter
               where myItem != null && filter(myItem)
               select myItem;
       // return DataSource.orderItemList.ToList();
    }

//    if (filter == null)//select whole list
//        {
//            return from v in _ds.orderItemList
//                   where v?.IsDeleted == false
//                   select v;
//}
//        return from v in _ds.orderItemList//select with filter
//               where v?.IsDeleted == false && filter(v)
//               select v;

/// <summary>
/// public method to delete an Order Item
/// </summary>
public void Delete(int _ID)
    {
        //int ind = 0;
        //// traverse through the order item list and find an order item with a matching ID#
        //foreach (DO.OrderItem item in DataSource.orderItemList)
        //{
        //    if (item.ID == _ID)
        //    {
        //        // saved the ID# of the matching order item
        //        ind = DataSource.orderItemList.IndexOf(item);
        //        break;
        //    }
        //}
        int ind = DataSource.orderItemList.FindIndex(x => x?.ID == _ID);
        if (ind == -1)
        {
            throw new DoesNotExistException();
        }
        DO.OrderItem? DelItem = DataSource.orderItemList[ind]; // saved the item that you located above       
        DataSource.orderItemList.Remove(DelItem); // delete that item from the order item list
    }

    /// <summary>
    /// public method to update an Order Item using an Order Item ID#
    /// </summary>
    public void Update(DO.OrderItem item)
    {
        int _ID = item.ID;
        DO.OrderItem? OldItem = DataSource.orderItemList.Find(x => x?.ID == _ID); // find an order item with a matching ID#
        if (OldItem == null)
        {
            throw new DoesNotExistException();
        }
        item.ProductID = (int)OldItem?.ProductID!;
        item.OrderID = (int)OldItem?.OrderID!;
        int index = DataSource.orderItemList.IndexOf(OldItem);
        DataSource.orderItemList[index] = item;
    }

    /// <summary>
    /// public method to update an Order Item using the product ID and order ID
    /// </summary>
    public void UpdateByIDs(DO.OrderItem item) // WHAT DO I DO ABOUT THIS UPDATE FUCNTION?! USED TO BE CALLED DIFF NAMES BC CANNOT OVERLOAD
    {
        //int ID = 0;
        var v = from newItem in DataSource.orderItemList
                where newItem?.OrderID == item.OrderID && newItem?.ProductID == item.ProductID
                select DataSource.orderItemList.IndexOf(newItem);
        var v2 = from newItem in DataSource.orderItemList
                where newItem?.OrderID == item.OrderID && newItem?.ProductID == item.ProductID
                select newItem?.ID;
        if (!v.Any())
        {
            throw new DO.DoesNotExistException();
        }
        OrderItem myItem = new OrderItem();
        foreach(var id in v2.ToList())
        {
            myItem.ID = (int)id;
        }
        myItem.ProductID = item.ProductID;
        myItem.OrderID = item.OrderID;
        myItem.Price = item.Price;
        myItem.Quantity = item.Quantity;
        foreach (var index in v.ToList())
        {
            DataSource.orderItemList[index] = myItem;
        }

        //foreach (DO.OrderItem it in DataSource.orderItemList)
        //{
        //    if (it.ProductID == item.ProductID)
        //    {
        //        if (it.OrderID == item.OrderID)
        //        {
        //            ID = it.ID;
        //            index = DataSource.orderItemList.IndexOf(it);
        //        }
        //    }
        //}
        //OrderItem myItem = new OrderItem(ID);
        //myItem.ProductID = item.ProductID;
        //myItem.OrderID = item.OrderID;
        //myItem.Price = item.Price;
        //myItem.Quantity = item.Quantity;
        //DataSource.orderItemList[index] = myItem;
    }

    /// <summary>
    /// public method to GET/SET an Order Item given the product ID and order ID.
    /// </summary>
    public OrderItem GetByIDs(int prodID, int ordID)
    {
        DO.OrderItem myItem = new OrderItem(-1);
        myItem.ProductID = prodID;
        myItem.OrderID = ordID;
        //myItem.ID = -1;
        // traverse through the the order item list and find an order item with a matching product ID# and order ID#
        foreach (DO.OrderItem item in DataSource.orderItemList)
        {
            if (item.ProductID == myItem.ProductID && item.OrderID == myItem.OrderID)
            {
                    myItem = item;
            }
        }
        // if a matching ID was not found
        if (myItem.ID == -1)
        {
            throw new DoesNotExistException();
        }
        return myItem;
    }

    /// <summary>
    /// public method to get the list of items in an order, given the order ID
    /// </summary>


    // HOW DO I CHANGE THIS TO GET ALL BECAUSE NEED TO HAVE AN INPUT
    public IEnumerable<OrderItem?> GetAllByID(int ordID)
    {
        List<OrderItem?> myList = new List<OrderItem?>();
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.OrderID == ordID)
                myList.Add(item); 
        }
        return myList;
    }

    public OrderItem GetByFilter(Func<OrderItem?, bool>? filter)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));//filter is null
        }
        foreach (OrderItem? item in DataSource.orderItemList)
        {
            if (item != null && filter(item))
            {
                return (OrderItem)item;
            }
        }
        throw new DoesNotExistException();
    }
}