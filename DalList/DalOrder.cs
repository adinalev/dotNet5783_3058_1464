using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
using DO;
namespace Dal;
internal class DalOrder : IOrder
{
    readonly static Random rand = new Random(); // readonly static field for generating random numbers

    /// <summary>
    /// public method to add an Order
    /// </summary>
    public int Add(DO.Order ord)
    {
        //case 1: Order does not exist yet, needs to be initialized
        ord.OrderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L));
        ord.ShippingDate = ord.OrderDate + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L)); // add a random time interval to the order date to get the shipping date
        ord.DeliveryDate = ord.ShippingDate + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 100L)); // add a random time interval to the shipping date to get the delivery date
        if (ord.ID == 0)
        {
            Order order = new Order();
            order.CustomerName = ord.CustomerName;
            order.Email = ord.Email;
            order.Address = ord.Address;
            order.OrderDate = ord.OrderDate;
            order.ShippingDate = ord.ShippingDate;
            order.DeliveryDate = ord.DeliveryDate;
            order.ID = Order.orderCounter++;
            DataSource.orderList.Add(order);
            return order.ID;     // CHANGED THIS FROM ORD.ID TO ORDER.ID!!!     
        }
    
        // case 2: Order already exists, throw an exception
        int index = DataSource.orderList.FindIndex(x => x?.ID == ord.ID); // find the order with a matching ID as the inputted order
        if (index != -1) // the index will be -1 if there is no order with the same ID#
        {
            // entering this loop means the order already exists since the ID# exists already
            throw new AlreadyExistsException();
        }
        else
        // Order is initialized but it's not in the list yet
        {
            DataSource.orderList.Add(ord);
            return ord.ID;
        }
    }

    //public int GetID(DO.Order? ord)
    //{
    //    return (int)ord.ID;
    //}

    ///// <summary>
    ///// public method to read an Order
    ///// </summary>
    public DO.Order? GetByID(int _ID)
    {
        DO.Order? ord = DataSource.orderList.Find(x => x?.ID == _ID); // find an order with a matching ID
        if (ord == null)
        {
            throw new DoesNotExistException();
        }
        return ord;
    }

    /// <summary>
    /// public method to read the Order list
    /// </summary>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter)
    {
        if (filter == null)//select whole list
        {
            return from order in DataSource.orderList
                   where order != null
                   select order;
        }
        return from myOrder in DataSource.orderList//select with filter
               where myOrder != null && filter(myOrder)
               select myOrder;

        //return DataSource.orderList.ToList();
    }

    /// <summary>
    /// public method to delete an Order
    /// </summary>
    public void Delete(int _ID)
    {
        int ind = -1;
        // traverse through the order list to find an order with a matching ID#
        foreach (DO.Order ord in DataSource.orderList)
        {
            if (ord.ID == _ID) 
            {
                // if an order with a matching ID# is found, save the index and break out of the loop
                ind = DataSource.orderList.IndexOf(ord);
                break;
            }
        }
        if(ind == -1)
        {
            throw new DoesNotExistException();
        }
        // retrieve the order sitting in the found index
        DO.Order? DelOrd = DataSource.orderList[ind];
        // delete that order
        DataSource.orderList.Remove(DelOrd);
    }

    /// <summary>
    /// public method to update an Order
    /// </summary>
    public void Update(DO.Order ord)
    {
        int _ID = ord.ID;
        DO.Order? OldOrd = DataSource.orderList.Find(x => x?.ID == _ID); // find an order with a matching ID#
        if (OldOrd == null)
        {
            // if no order is found with a matching ID#, there is no order to update
            throw new DoesNotExistException();
        }
        // ensure that the dates stay the same
        ord.OrderDate = OldOrd?.OrderDate;
        ord.ShippingDate = OldOrd?.ShippingDate;
        ord.DeliveryDate = OldOrd?.DeliveryDate;
        // locate the index of the old order that you would like to update 
        int index = DataSource.orderList.IndexOf(OldOrd);
        // input the updated order into the index of the old order
        DataSource.orderList[index] = ord;
    }

    public Order GetByFilter(Func<Order?, bool>? filter)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));//filter is null
}
        foreach (Order? order in DataSource.orderList)
        {
            if (order!=null && filter(order))
            {
                return (Order) order;
            }
        }
        throw new DoesNotExistException();  // CORRECT EXCEPTION?
    }
}
