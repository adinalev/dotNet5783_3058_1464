using DalApi;
using Dal;
namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    static IDal? dal = new DalList();

    /// <summary>
    /// public method to return the order list
    /// </summary>
    public List<BO.OrderForList> GetAllOrderForList()
    {
        IEnumerable<DO.Order> orders = dal.dalOrder.GetAll(); // get all the orders from DO 
        IEnumerable<DO.OrderItem> orderItems = dal.dalOrderItem.GetAll(); // get all the order items from DO
        List<BO.OrderForList> list = new List<BO.OrderForList>(); // create a list of BO orders
        foreach (DO.Order order in orders)
        {
            int quantity = 0;
            double total = 0;

            foreach (DO.OrderItem item in orderItems) // for each order item in DO
            {
                if (item.OrderID == order.ID) // if the OrderID of that order item matches with an order ID (meaning it's part of that order)
                {
                    quantity++; // increase the amount of items inside of an order
                    total += item.Price; // increase the total by the price of that item
                }
            }
            list.Add(new BO.OrderForList // add a new BO order to the list created above
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                Status = GetStatus(order),
                AmountOfItems = quantity,
                TotalPrice = total // set the total price equal to the variable total
            }); 

        }
        return list; // return the list of new BO orders
    }

    /// <summary>
    /// method to return the status of an order
    /// </summary>
    public BO.Enums.OrderStatus GetStatus(DO.Order order)
    {
        if (order.OrderDate == null) // if the order wasn't placed yet
            return BO.Enums.OrderStatus.New;
        if (order.OrderDate < DateTime.Now && (order.ShippingDate > DateTime.Now || order.ShippingDate == DateTime.MinValue)) // if the order has been placed but not shipped yet
            return BO.Enums.OrderStatus.BeingProcessed;
        if ((order.ShippingDate < DateTime.Now && order.ShippingDate != DateTime.MinValue) && (order.DeliveryDate > DateTime.Now || order.DeliveryDate == DateTime.MinValue))  // if the order has been shipped but not been delivered yet
            return BO.Enums.OrderStatus.Shipped;
        if (order.DeliveryDate < DateTime.Now && order.DeliveryDate != DateTime.MinValue) // if the order has been delivered
            return BO.Enums.OrderStatus.Delivered;
        else return BO.Enums.OrderStatus.Unknown; //if the order matches none of the criteria above
    }

    /// <summary>
    /// public method to return an order
    /// </summary>
    public BO.Order GetBoOrder(int _ID)
    {
        DO.Order order = dal.dalOrder.GetByID(_ID); // retrieve the corresponding DO order
        if (_ID < 0) 
        {
            throw new BO.DoesNotExistException(order);
        }
        double priceTemp = 0;
        foreach (DO.OrderItem? ord in dal.dalOrderItem.GetAll())
        {
            if (ord.OrderID == _ID)
            {
                priceTemp += ord.Price; // add up all of prices in the order
            }
        }
        if (order.ID == _ID) // if exists 
        {
            return new BO.Order // create a new BO order with appropriate critera
            {
                ID = _ID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                PaymentDate = order.OrderDate, /* Payment date should be the same as the order date */
                ShippingDate = order.ShippingDate,
                DeliveryDate = order.DeliveryDate,
                Status = GetStatus(order),
                TotalPrice = priceTemp,
            };
        }
        throw new BO.DoesNotExistException(order);
    }

    /// <summary>
    /// public method to update the delivery date of an order
    /// </summary>
    public BO.Order UpdateDeliveryDate(int orderID, DateTime date)
    {
        DO.Order order = dal.dalOrder.GetByID(orderID); // retrieve the corresponding DO order
        BO.Order orderBO = GetBoOrder(orderID); // retrieve the status of an order
        if (GetStatus(order) == BO.Enums.OrderStatus.New) // if the order has not been placed yet
        {
            throw new BO.OrderNotPlacedYetException();
        }
        if (GetStatus(order) == BO.Enums.OrderStatus.Delivered) // if the order has already been delivered
        {
            throw new BO.AlreadyDeliveredException();
        }
        if (order.ShippingDate == DateTime.MinValue) // if there is no shipping date available for the order yet
        {
            throw new BO.NoShipDateException();
        }
        if (date < order.ShippingDate) // if the delivery date is set for before the shipping date
        {
            throw new BO.DeliveryDateOutOfRangeException();
        }
        DO.Order ord = new() // create a new DO order with the same criteria but with the new delivery date
        {
            ID = orderID,
            Address = order.Address,
            Email = order.Email,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            ShippingDate = order.ShippingDate,
            DeliveryDate = date // This is the only change in the order
        };
        dal.dalOrder.Update(ord); // update the order in DO

        orderBO.DeliveryDate = date;
        orderBO.Status = GetStatus(order);
        return orderBO;
    } 

    /// <summary>
    /// public method to update a shipping date of an order
    /// </summary>
    public BO.Order UpdateShippingDate(int orderID, DateTime date)
    {
        DO.Order order = dal.dalOrder.GetByID(orderID); // retrieve the corresponding DO order
        BO.Order orderBO = GetBoOrder(orderID); // retrieve the corresponding BO order
        if(GetStatus(order) == BO.Enums.OrderStatus.New) // if the order has not been placed yet
        {
            throw new BO.OrderNotPlacedYetException();
        }
        else if (GetStatus(order) != BO.Enums.OrderStatus.BeingProcessed) // if the order has already shipped
        {
            throw new BO.AlreadyShippedException();
        }
        if (date > orderBO.DeliveryDate && orderBO.DeliveryDate != DateTime.MinValue) // if the shipping date is set to later than the delivery date
        {
            throw new BO.ShipDateOutOfRangeException();
        }
                    
        DO.Order ord = new() // create a new DO order with the same critera but the shipping date is set to the new date
        {
            ID = orderID,
            Address = order.Address,
            Email = order.Email,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            ShippingDate = date, // this is the only change
            DeliveryDate = order.DeliveryDate, 
        };
        dal.dalOrder.Update(ord); // update the order in DO
                                    
        orderBO.ShippingDate = date;
        orderBO.Status = GetStatus(order);
        return orderBO;
    }

    public BO.OrderTracking TrackOrder(int orderID)
    {
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        DO.Order order = new DO.Order(-1);
        IEnumerable<DO.Order?> orders = dal.dalOrder.GetAll(); // get all the orders from DO 
        foreach (DO.Order ord in orders)
        {
            if (ord.ID == orderID)
                order = ord;
        }
        if (order.ID == -1)
        {
            throw new BO.DoesNotExistException(order);
        }
        orderTracking.ID = orderID;
        orderTracking.Status = GetStatus(order);
        return orderTracking;
    }



}
