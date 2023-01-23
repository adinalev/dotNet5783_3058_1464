//using DalApi;
//using Dal;
//namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// public method to return the order list
    /// </summary>
    public List<BO.OrderForList?> GetAllOrderForList()
    {
        IEnumerable<DO.Order?>? orders = dal?.dalOrder.GetAll(); // get all the orders from DO 
        IEnumerable<DO.OrderItem?>? orderItems = dal?.dalOrderItem.GetAll(); // get all the order items from DO
        List<BO.OrderForList?>? list = new List<BO.OrderForList?>(); // create a list of BO orders
        //return (List<BO.OrderForList?>)(from DO.Order ord in orders
        //       select new BO.OrderForList
        //       {
        //           ID = ord.ID,
        //           CustomerName = ord.CustomerName!,
        //           Status = GetStatus(ord),
        //           AmountOfItems = orderItems.Select(orderItems => orderItems?.ID == ord.ID).Count(),
        //           TotalPrice = (double)orderItems.Sum(orderItems => orderItems?.Price)
        //       });
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
        return list!; // return the list of new BO orders
    }

    /// <summary>
    /// method to return the status of an order
    /// </summary>
    public BO.Enums.OrderStatus GetStatus(DO.Order? order)
    {
        if (order?.OrderDate == null) // if the order wasn't placed yet
            return BO.Enums.OrderStatus.New;
        if (order?.OrderDate < DateTime.Now && (order?.ShippingDate > DateTime.Now || order?.ShippingDate == null)) // if the order has been placed but not shipped yet
            return BO.Enums.OrderStatus.BeingProcessed;
        if ((order?.ShippingDate < DateTime.Now && order?.ShippingDate != null) && (order?.DeliveryDate > DateTime.Now || order?.DeliveryDate == null))  // if the order has been shipped but not been delivered yet
            return BO.Enums.OrderStatus.Shipped;
        if (order?.DeliveryDate < DateTime.Now && order?.DeliveryDate != null) // if the order has been delivered
            return BO.Enums.OrderStatus.Delivered;
        else return BO.Enums.OrderStatus.Unknown; //if the order matches none of the criteria above
    }

    /// <summary>
    /// public method to return an order
    /// </summary>
    public BO.Order? GetBoOrder(int _ID)
    {
        DO.Order? order = new DO.Order(-1);
        try
        {
            order = dal!.dalOrder.GetByID(_ID); // retrieve the corresponding DO order // NULLABLE?
        }
        catch
        {
            throw new BO.DoesNotExistException();
        }
        double priceTemp = 0;
        foreach (DO.OrderItem? ord in dal.dalOrderItem.GetAll())
        {
            if (ord?.OrderID == _ID)
            {
                priceTemp += (int)ord?.Price!; // add up all of prices in the order
            }
        }
        if (order?.ID == _ID) // if exists 
        {
            return new BO.Order // create a new BO order with appropriate critera
            {
                ID = _ID,
                Address = order?.Address,
                Email = order?.Email,
                CustomerName = order?.CustomerName,
                OrderDate = order?.OrderDate,
                PaymentDate = order?.OrderDate, /* Payment date should be the same as the order date */
                ShippingDate = order?.ShippingDate,
                DeliveryDate = order?.DeliveryDate,
                Status = GetStatus(order),
                TotalPrice = priceTemp,
            };
        }
        throw new BO.DoesNotExistException();
    }
    public BO.Order? UpdateDeliveryDate(int orderID)
    {
        DO.Order order;
        try
        {
            order = (DO.Order)(dal?.dalOrder.GetByID(orderID)!);//get the order from DO of orderId-or catch exception
        }
        catch(DO.DoesNotExistException)
        {
            throw new BO.DoesNotExistException();
        }

        if (order.ID == orderID /*&& oId.DeliveryDate < DateTime.Today*/)//if oId exists and has not been shipped 
        {
            DO.Order ord = new DO.Order()
            {
                ID = orderID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShippingDate = order.ShippingDate,
                DeliveryDate = DateTime.Now,//the only difference
            };//set new delivery date in new DO Order
            try
            {
                dal?.dalOrder.Update(ord);//update the order in DO
            }
            catch (DO.DoesNotExistException)
            {
                throw new BO.DoesNotExistException();
            }
            double priceTemp = 0;
            priceTemp = (double)(dal?.dalOrderItem.GetAll()!.Where(x => x != null && x?.OrderID == ord.ID).Sum(x => x?.Price)!);
            //foreach (DO.OrderItem? temp in DOList?.OrderItem.GetAll()!)
            //{
            //    if (temp?.IsDeleted == false && temp?.OrderID == o.ID)
            //    {
            //        priceTemp += temp?.Price ?? 0;//add up all of prices in the order
            //    }
            //}
            return new BO.Order
            {
                ID = orderID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate ?? throw new Exception(),
                ShippingDate = order.ShippingDate ?? throw new Exception(),
                DeliveryDate = DateTime.Now,
                Status = GetStatus(ord),
                TotalPrice = priceTemp,
            };//new BO Order
        }
        throw new BO.DoesNotExistException();
    }

    public BO.Order? UpdateShippingDate(int orderID)
    {
        DO.Order order;
        try
        {
            order = (DO.Order)(dal?.dalOrder.GetByID(orderID)!);//get the order from DO of orderId-or catch exception
        }
        catch (DO.DoesNotExistException)
        {
            throw new BO.DoesNotExistException();
        }
        if (order.ID == orderID /*&& oId.ShipDate < DateTime.Today*/ )//if oId exists and has not been shipped 
        {
            DO.Order ord = new()
            {
                ID = orderID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShippingDate = DateTime.Now,
                DeliveryDate = null,
            };//set new ship date in new DO Order
            try
            {
                dal?.dalOrder.Update(ord);//update the order in DO
            }
            catch (DO.DoesNotExistException)
            {
                throw new BO.DoesNotExistException();
            }
            double priceTemp = 0;
            priceTemp = (double)(dal?.dalOrderItem.GetAll()!.Where(x => x != null && x?.OrderID == ord.ID).Sum(x => x?.Price)!);
            //foreach (DO.OrderItem? temp in DOList?.OrderItem.GetAll()!)
            //{
            //    if (temp?.IsDeleted == false && temp?.OrderID == o.ID)
            //    {
            //        priceTemp += temp?.Price ?? 0;//add up all of prices in the order
            //    }
            //}
            return new BO.Order
            {
                ID = orderID,
                Address = order.Address,
                Email = order.Email,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate ?? throw new Exception(),
                ShippingDate = DateTime.Now,
                Status = GetStatus(ord),
                TotalPrice = priceTemp,
                DeliveryDate = null,
            };//new BO Order
        }
        throw new BO.DoesNotExistException();
    }

    public BO.OrderTracking TrackOrder(int orderID)
    {
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        DO.Order order = new DO.Order(-1);
        IEnumerable<DO.Order?> orders = dal!.dalOrder.GetAll(); // get all the orders from DO 
        foreach (DO.Order ord in orders)
        {
            if (ord.ID == orderID)
                order = ord;
        }
        if (order.ID == -1)
        {
            throw new BO.DoesNotExistException();
        }
        orderTracking.ID = orderID;
        orderTracking.Status = GetStatus(order);
        return orderTracking;
    }
    public List<string> GetItemNames(int orderID)
    {
        IEnumerable<DO.Order?> orders = dal?.dalOrder.GetAll();
        IEnumerable<DO.OrderItem?> items = dal?.dalOrderItem.GetAll();
        IEnumerable<DO.Product?> products = dal?.dalProduct.GetAll();
        List<string> productNames = new List<string>();
        DO.Product product = new DO.Product();
        foreach(DO.OrderItem item in items)
        {
            if (item.OrderID == orderID)
            {
                product = (DO.Product)dal?.dalProduct.GetByID(item.ProductID);
                productNames.Add(product.Name);
            }
        }
        return productNames;               
    }

    public BO.OrderTracking GetOrderTracking(int orderID)
    {
        //DO.Order order = new DO.Order();
        //try
        //{
        //    order = (DO.Order)dal?.dalOrder.GetByID(orderID);
        //}
        //catch
        //{
        //    throw new BO.DoesNotExistException();
        //}
        //return new BO.OrderTracking()
        //{
        //    ID = orderID,
        //    Status = GetStatus(order),
        //    Tracking = new List<Tuple<DateTime?, string>> { new Tuple<DateTime?, string>(order.OrderDate, "Ordered"), new Tuple<DateTime?, string>(order.ShippingDate, "Shipped"),
        //    new Tuple<DateTime?, string>(order.DeliveryDate, "Delivered")}
        //};
        DO.Order order = new();
        try
        {
            order = (DO.Order)dal?.dalOrder.GetByID(orderID)!;//get the requested order from dal
        }
        catch
        {
            throw new BO.DoesNotExistException();//order does not exist
        }
        return new BO.OrderTracking()
        {
            ID = orderID,
            Status = GetStatus(order),
            Tracking = new List<Tuple<DateTime?, string>> { new Tuple<DateTime?, string>(order.OrderDate, "Approved"), new Tuple<DateTime?, string>(order.ShippingDate, "Sent"),
            new Tuple<DateTime?, string>(order.DeliveryDate, "Delivered")}
        };//create new order tracking
    }

    public int GetNextID()
    {
        return DO.Order.orderCounter;
    }

}
