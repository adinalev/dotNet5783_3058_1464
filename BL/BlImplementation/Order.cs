using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;
internal class Order : BlApi.IOrder
{
    private IDal Dal = new DalList();

    public DO.Order AddOrder(BO.Order order)
    {
        if (order.CustomerName == "" || order.Email == "" || order.Address == "" || order.TotalPrice <= 0)
        {
            throw new BO.InvalidInputException();
        }
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

    public void DeleteOrder(int _id)
    {
        DO.Order delOrder = new DO.Order();
        int delID = -1;
        foreach (DO.Order order in Dal.dalOrder.GetAll())
        {
            if (_id == order.ID)
                delID = order.ID;
        }
        if (delID == -1)
        {
            throw new BO.DoesNotExistException(delOrder);
        }
        Dal.dalOrder.Delete(delID);
    }

    public DO.Order UpdateOrder(BO.Order order)
    {
        DO.Order newOrder = new DO.Order();
        int _id = -1;
        foreach (DO.Order ord in Dal.dalOrder.GetAll())
        {
            if (ord.ID == order.ID)
                _id = ord.ID;
        }
        if (_id == -1)
        {
            throw new BO.DoesNotExistException(newOrder);
        }
        if (order.CustomerName == "" || order.Email == "" || order.Address == "" || order.TotalPrice <= 0)
        {
            throw new BO.InvalidInputException();
        }
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

    public DO.Order GetOrder(int ID)
    {
        int _id = -1;
        DO.Order order = new DO.Order();
        foreach (DO.Order ord in Dal.dalOrder.GetAll())
        {
            if (ord.ID == ID)
            {
                order = ord;
                _id = ord.ID;
            }
        }
        if (_id == -1)
        {
            throw new BO.DoesNotExistException(order);
        }
        return order;
    }

    public IEnumerable<DO.Order> GetOrderList()
    {
        return Dal.dalOrder.GetAll();
    }

}


