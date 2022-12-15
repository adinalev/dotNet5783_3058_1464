using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;

internal class OrderItem : BlApi.IOrderItem
{
    private IDal Dal = new DalList();

    public DO.OrderItem AddOrderItem(BO.OrderItem item)
    {
        if (item.ProductID < 100000 || item.Price <= 0 || item.Quantity <= 0)
        {
            throw new BO.InvalidInputException();
        }
        // ARE WE SUPPOSED TO CHECK FOR THE 4 HERE?
        DO.OrderItem newItem = new DO.OrderItem();
        newItem.ProductID = item.ProductID;
        newItem.Price = item.Price;
        newItem.Quantity = item.Quantity;
        newItem.ID = Dal.dalOrderItem.Add(newItem);
        return newItem;
    }

    public void DeleteOrderItem(int _id)
    {
        DO.OrderItem delItem = new DO.OrderItem();
        int delID = -1;
        foreach (DO.OrderItem item in Dal.dalOrderItem.GetAll())
        {
            if (_id == item.ID)
                delID = item.ID;
        }
        if (delID == -1)
        {
            throw new BO.DoesNotExistException(delItem);
        }
        Dal.dalOrderItem.Delete(delID);
    }

    public DO.OrderItem UpdateOrderItem(BO.OrderItem item)
    {
        DO.OrderItem newItem = new DO.OrderItem();
        int _id = -1;
        foreach (DO.OrderItem it in Dal.dalOrderItem.GetAll())
        {
            if (it.ID == item.ID)
                _id = it.ID;
        }
        if (_id == -1)
        {
            throw new BO.DoesNotExistException(newItem);
        }
        if (item.ProductID < 100000 || item.Price <= 0 || item.Quantity <= 0)
        {
            throw new BO.InvalidInputException();
        }
        newItem.ID = item.ID;
        newItem.ProductID = item.ProductID;
        newItem.Price = item.Price;
        newItem.Quantity = item.Quantity;      
        Dal.dalOrderItem.Update(newItem);
        return newItem; // need this??
    }

    public DO.OrderItem GetOrderItem(int ID)
    {
        int _id = -1;
        DO.OrderItem item = new DO.OrderItem();
        foreach (DO.OrderItem it in Dal.dalOrderItem.GetAll())
        {
            if (it.ID == ID)
            {
                item = it;
                _id = it.ID;
            }
        }
        if (_id == -1)
        {
            throw new BO.DoesNotExistException(item);
        }
        return item;
    }

    public IEnumerable<DO.OrderItem> GetOrderItemList()
    {
        return Dal.dalOrderItem.GetAll();
    }
}
