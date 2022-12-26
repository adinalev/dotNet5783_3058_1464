using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    static IDal? dal = new DalList();

    /// <summary>
    /// public method to return the order list
    /// </summary>
    public IEnumerable<BO.OrderForList?> GetAllOrderForList()
    {
        IEnumerable<DO.Order> orders = dal.dalOrder.GetAll();//get all orders from DO  -- TOOK AWAY THE ?
        IEnumerable<DO.OrderItem> orderItems = dal.dalOrderItem.GetAll();//get all orderItems from DO  -- TOOK AWAY THE ?
        return from DO.Order? ord in orders
               select new BO.OrderForList
               {
                   ID = ord?.ID ?? throw new BO.DoesNotExistException(ord),
                   CustomerName = ord?.CustomerName,
                   Status = GetStatus(ord.Value),
                   AmountOfItems = orderItems.Select(orderItems => orderItems.ID == ord?.ID).Count(),
                   TotalPrice = (double)orderItems.Sum(orderItems => orderItems.Price)
               };

    }//calls get of DO order list, gets items for each order, and build orderItemlist

    private BO.Enums.OrderStatus GetStatus(DO.Order order)
    {

        return order.DeliveryDate != DateTime.MinValue ? BO.Enums.OrderStatus.COMPLETED : order.ShippingDate != DateTime.MinValue ?
            BO.Enums.OrderStatus.INPROGRESS : BO.Enums.OrderStatus.NEW;
    }

    /// <summary>
    /// public method to return an order
    /// </summary>
    public BO.Order GetBoOrder(int _ID)
    {
        DO.Order order = dal.dalOrder.GetByID(_ID);//get right DO Order
        if (_ID < 0)//id is negative
        {
            throw new BO.DoesNotExistException(order);
        }
        //DO.Order order = dal.dalOrder.GetByID(_ID);//get right DO Order
        double priceTemp = 0;
        foreach (DO.OrderItem ord in dal.dalOrderItem.GetAll())
        {
            if (ord.OrderID == _ID)
            {
                priceTemp += ord.Price;//add up all of prices in the order
            }
        }
        if (order.ID == _ID)//if exists 
        {
            return new BO.Order
            {
                ID = _ID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShippingDate = order.ShippingDate,
                DeliveryDate = order.DeliveryDate,
                Status = GetStatus(order),
                TotalPrice = priceTemp,
            };//new BO Order
        }
        throw new BO.DoesNotExistException(order);
    }//get order number, check if exists, update date in DO order, and return BO order that has been "shipped"

    /// <summary>
    /// public method to update customer details to an order
    /// </summary>
    public void UpdateCustomerDetails(BO.Order order) //Update Method, this time with logic checks
    {
        DO.Order newOrder = new DO.Order();
        int _id = -1; //set a check to see if the id will be found
        foreach (DO.Order ord in dal.dalOrder.GetAll())
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
        dal.dalOrder.Update(newOrder);
    }

    /// <summary>
    /// public method to update the delivery date of an order
    /// </summary>
    public BO.Order UpdateDeliveryDate(int orderID, DateTime date)
    {
        DO.Order order = dal.dalOrder.GetByID(orderID);//get the order from DO of orderId-or catch exception
        if (order.ID == orderID && order.DeliveryDate < DateTime.Today)//if an order with the same ID exists and has not been delivered yet
        {
            DO.Order ord = new()
            {
                ID = orderID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShippingDate = order.ShippingDate,
                DeliveryDate = date // used to be DateTime.Now // This is the only change in the order
            };//set new delivery date in new DO Order
            dal.dalOrder.Update(ord);//update the order in DO
            double priceTemp = 0;
            foreach (DO.OrderItem temp in dal.dalOrderItem.GetAll())
            {
                if (temp.OrderID == ord.ID)
                {
                    priceTemp += temp.Price;//add up all of prices in the order
                }
            }
            return new BO.Order
            {
                ID = orderID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShippingDate = order.ShippingDate,
                DeliveryDate = DateTime.Now,
                Status = GetStatus(ord),
                TotalPrice = priceTemp,
            };//new BO Order
        }
        throw new BO.DoesNotExistException(order);

    }//get order number, check if exists, update date in DO order, and return BO order that has been "delivered" 

    /// <summary>
    /// public method to update a shipping date of an order
    /// </summary>
    public BO.Order UpdateShippingDate(int orderID, DateTime date)
    {
        DO.Order order = dal.dalOrder.GetByID(orderID);//get the order from DO of orderId-or catch exception
        if (order.ID == orderID && order.ShippingDate < DateTime.Today) // if the order exists and has not been shipped yet
        {
            DO.Order ord = new()
            {
                ID = orderID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShippingDate = date, // changed from DateTime.Now
                DeliveryDate = DateTime.MinValue, // WHAT DO WE DO FOR THIS?!?!?
            };//set new ship date in new DO Order
            dal.dalOrder.Update(ord);//update the order in DO
            double priceTemp = 0;
            foreach (DO.OrderItem temp in dal.dalOrderItem.GetAll())
            {
                if (temp.OrderID == ord.ID)
                {
                    priceTemp += temp.Price;//add up all of prices in the order
                }
            }
            return new BO.Order
            {
                ID = orderID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShippingDate = DateTime.Now,
                Status = GetStatus(ord),
                TotalPrice = priceTemp,
                DeliveryDate = DateTime.MinValue,// IS THIS CORRECT??
            };//new BO Order
        }
        throw new BO.DoesNotExistException(order);
    }

    /// <summary>
    /// public method to delete/cancel an order
    /// </summary>
    public void DeleteOrder(int _id) //Delete Method, this time with logic checks
    {
        DO.Order delOrder = new DO.Order();
        int delID = -1; //set a check to see if the _id will be found
        foreach (DO.Order order in dal.dalOrder.GetAll())
        { //loop through orders to check IDs
            if (_id == order.ID)
                delID = order.ID; //if found, set the found ID to delID 
        }
        if (delID == -1) //that means it was not found in the foreach loop
        {
            throw new BO.DoesNotExistException(delOrder); //throw exception
        }
        // SHOULD I CHANGE THE STATUS TO CANCELLED?
        //if ID was found, then delete the order with that ID:
        dal.dalOrder.Delete(delID);
    }

    //public BO.OrderTracking GetOrderTracking(int orderID)
    //{
    //    bool found = false;
    //    foreach (DO.Order ord in dal.dalOrder.GetAll())
    //    { //loop through orders to check IDs
    //        if (ord.ID == orderID)
    //        {
    //            found = true;

    //        }
    //    }
    //    if (_id == -1) //id not found
    //    {
    //        throw new BO.DoesNotExistException(newOrder); //throw exception
    //    }
    //}

    //public void SetStatus(int ID)
    //{
    //    BO.Order order = new BO.Order();
    //    int orderID = -1; //set a check to see if the _id will be found
    //    foreach (BO.Order ord in dal.dalOrder.GetAll())
    //    { //loop through orders to check IDs
    //        if (_id == order.ID)
    //            delID = order.ID; //if found, set the found ID to delID 
    //    }
    //    if (delID == -1) //that means it was not found in the foreach loop
    //    {
    //        throw new BO.DoesNotExistException(delOrder); //throw exception
    //    }
    //}
}

//DO EWE NEED TO INCLUDE THE FUNCTION OF GETORDERTRACKING???

///// <summary>
///// Implementation of class Order that inherits
///// from Iorder interface under BlAPi. 
///// </summary>
//internal class Order : BlApi.IOrder 
//{
//    private IDal Dal = new DalList(); //private field Dal

//    //Implementing methods declared in the interface:

//    public DO.Order AddOrder(BO.Order order) //Add Method, this time with logic checks
//    {
//        if (order.CustomerName == "" || order.Email == "" || order.Address == "" || order.TotalPrice <= 0)
//        {
//            throw new BO.InvalidInputException(); //if any input is empty or out of bound, it is invalid
//        }
//        //if input is valid, set the fields of newOrder and return it
//        DO.Order newOrder = new DO.Order();
//        newOrder.CustomerName = order.CustomerName;
//        newOrder.Email = order.Email;
//        newOrder.Address = order.Address;
//        newOrder.OrderDate = order.OrderDate;
//        newOrder.ShippingDate = order.ShippingDate;
//        newOrder.DeliveryDate = order.DeliveryDate;
//        newOrder.ID = Dal.dalOrder.Add(newOrder);
//        return newOrder;            
//    }

//    public void DeleteOrder(int _id) //Delete Method, this time with logic checks
//    {
//        DO.Order delOrder = new DO.Order();
//        int delID = -1; //set a check to see if the _id will be found
//        foreach (DO.Order order in Dal.dalOrder.GetAll())
//        { //loop through orders to check IDs
//            if (_id == order.ID)
//                delID = order.ID; //if found, set the found ID to delID 
//        }
//        if (delID == -1) //that means it was not found in the foreach loop
//        {
//            throw new BO.DoesNotExistException(delOrder); //throw exception
//        }
//        //if ID was found, then delete the order with that ID:
//        Dal.dalOrder.Delete(delID);
//    }

//    public DO.Order UpdateOrder(BO.Order order) //Update Method, this time with logic checks
//    {
//        DO.Order newOrder = new DO.Order();
//        int _id = -1; //set a check to see if the id will be found
//        foreach (DO.Order ord in Dal.dalOrder.GetAll())
//        { //loop through orders to check IDs
//            if (ord.ID == order.ID)
//                _id = ord.ID; //if found, set the found ID to _id 
//        }
//        if (_id == -1) //id not found
//        {
//            throw new BO.DoesNotExistException(newOrder); //throw exception
//        }
//        //otherwise move on to next data check:
//        if (order.CustomerName == "" || order.Email == "" || order.Address == "" || order.TotalPrice <= 0)
//        {
//            throw new BO.InvalidInputException(); //if data is invalid throw exception
//        }
//        //else update the order
//        newOrder.ID = order.ID;
//        newOrder.CustomerName = order.CustomerName;
//        newOrder.Email = order.Email;
//        newOrder.Address = order.Address;
//        newOrder.OrderDate = order.OrderDate;
//        newOrder.ShippingDate = order.ShippingDate;
//        newOrder.DeliveryDate = order.DeliveryDate;
//        Dal.dalOrder.Update(newOrder);
//        return newOrder; // need this??
//    }

//    public DO.Order GetOrder(int ID) //GetOrder method with logic checks
//    {
//        int _id = -1; //set a check to see if the id will be found
//        DO.Order order = new DO.Order();
//        foreach (DO.Order ord in Dal.dalOrder.GetAll())
//        { //loop to check if the ID exists
//            if (ord.ID == ID)
//            {
//                order = ord;
//                _id = ord.ID; //set the ID check to the ID
//            }
//        } 
//        if (_id == -1) //the desired ID was not found
//        {
//            throw new BO.DoesNotExistException(order); //so throw an exception
//        }
//        //otherwise return the order
//        return order;
//    }

//    public IEnumerable<DO.Order> GetOrderList() //method to get the list of orders
//    {
//        return Dal.dalOrder.GetAll();
//    }

//}


