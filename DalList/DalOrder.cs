using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace Dal;
public class DalOrder
{
    /// <summary>
    /// public method to add an Order
    /// </summary>
    public int AddOrder(Order ord)
        //  DO WE NEED TO DO ALL THE TIME STAMPS AND STUFF OR IS IT HARDCODED IN?
    {
        //case 1: Order does not exist yet, needs to be initialized
        if (ord.ID == 0)
        {
            ord.ID = DataSource.Config.NextOrderNumber;
            DataSource.orderList.Add(ord);
            return ord.ID;
        }

        // case 2: Order already exists, throw an exception
        int index = DataSource.orderList.FindIndex(x => x.ID == ord.ID);
        if (index != -1)
        {
            throw new Exception("Order exists already!\n");
        }
        else
        // Order is initialized but it's not in the list yet
        {
            Dal.DataSource.orderList.Add(ord);
            return ord.ID;
        }
    }

    ///// <summary>
    ///// public method to read an Order
    ///// </summary>
    public Order ReadOrder(int _ID)
    {
        Order ord = DataSource.orderList.Find(x => x.ID == _ID);
        if (ord.ID != _ID)
        {
            throw new Exception("Order does not exist!\n");
        }
        return ord;
    }

    /// <summary>
    /// public method to read the Order list
    /// </summary>
    public List<Order> ReadOrderList()
    {
        return DataSource.orderList.ToList();
    }

    /// <summary>
    /// public method to delete an Order
    /// </summary>
    public void DeleteOrder(int _ID)
    {
        int ind = 0;
        foreach (Order ord in DataSource.orderList)
        {
            if (ord.ID == _ID)
            {
                ind = DataSource.orderList.IndexOf(ord);
                break;
            }
        }
        Order DelOrd = DataSource.orderList[ind];
        DataSource.orderList.Remove(DelOrd);
    }

    /// <summary>
    /// public method to update an Order
    /// </summary>
    public void UpdateOrder(Order ord)
    {
        int _ID = ord.ID;
        Order OldOrd = DataSource.orderList.Find(x => x.ID == _ID);
        if (ord.ID != OldOrd.ID)
        {
            throw new Exception("Order does not exist!\n");
        }
        int index = DataSource.orderList.IndexOf(OldOrd);
        DataSource.orderList[index] = ord;
    }
}
