using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;

/// <summary>
/// Implementation of class Order that inherits
/// from Iorder interface under BlAPi. 
/// </summary>
internal class Order : BlApi.IOrder 
{
    private IDal Dal = new DalList(); //private field Dal

    //Implementing methods declared in the interface:
    
    public DO.Order AddOrder(BO.Order order) //Add Method, this time with logic checks
    {
        if (order.CustomerName == "" || order.Email == "" || order.Address == "" || order.TotalPrice <= 0)
        {
            throw new BO.InvalidInputException(); //if any input is empty or out of bound, it is invalid
        }
        //if input is valid, set the fields of newOrder and return it
        DO.Order newOrder = new DO.Order();
        newOrder.CustomerName = order.CustomerName;
        newOrder.Email = order.Email;
        newOrder.Address = order.Address;
        newOrder.OrderDate = order.OrderDate;
        newOrder.ShippingDate = order.ShippingDate;
        newOrder.DeliveryDate = order.DeliveryDate;
        newOrder.ID = Dal.dalOrder.Add(newOrder);
        return newOrder;            
    }

    public void DeleteOrder(int _id) //Delete Method, this time with logic checks
    {
        DO.Order delOrder = new DO.Order();
        int delID = -1; //set a check to see if the _id will be found
        foreach (DO.Order order in Dal.dalOrder.GetAll())
        { //loop through orders to check IDs
            if (_id == order.ID)
                delID = order.ID; //if found, set the found ID to delID 
        }
        if (delID == -1) //that means it was not found in the foreach loop
        {
            throw new BO.DoesNotExistException(delOrder); //throw exception
        }
        //if ID was found, then delete the order with that ID:
        Dal.dalOrder.Delete(delID);
    }

    public DO.Order UpdateOrder(BO.Order order) //Update Method, this time with logic checks
    {
        DO.Order newOrder = new DO.Order();
        int _id = -1; //set a check to see if the id will be found
        foreach (DO.Order ord in Dal.dalOrder.GetAll())
        { //loop through orders to check IDs
            if (ord.ID == order.ID)
                _id = ord.ID; //if found, set the found ID to _id 
        }
        if (_id == -1) //id not found
        {
            throw new BO.DoesNotExistException(newOrder); //throw exception
        }
        //otherwise move on to next data check:
        if (order.CustomerName == "" || order.Email == "" || order.Address == "" || order.TotalPrice <= 0)
        {
            throw new BO.InvalidInputException(); //if data is invalid throw exception
        }
        //else update the order
        newOrder.ID = order.ID;
        newOrder.CustomerName = order.CustomerName;
        newOrder.Email = order.Email;
        newOrder.Address = order.Address;
        newOrder.OrderDate = order.OrderDate;
        newOrder.ShippingDate = order.ShippingDate;
        newOrder.DeliveryDate = order.DeliveryDate;
        Dal.dalOrder.Update(newOrder);
        return newOrder; // need this??
    }

    public DO.Order GetOrder(int ID) //GetOrder method with logic checks
    {
        int _id = -1; //set a check to see if the id will be found
        DO.Order order = new DO.Order();
        foreach (DO.Order ord in Dal.dalOrder.GetAll())
        { //loop to check if the ID exists
            if (ord.ID == ID)
            {
                order = ord;
                _id = ord.ID; //set the ID check to the ID
            }
        } 
        if (_id == -1) //the desired ID was not found
        {
            throw new BO.DoesNotExistException(order); //so throw an exception
        }
        //otherwise return the order
        return order;
    }

    public IEnumerable<DO.Order> GetOrderList() //method to get the list of orders
    {
        return Dal.dalOrder.GetAll();
    }

}


