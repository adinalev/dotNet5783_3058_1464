using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
namespace BlApi;
public interface IOrder
{
    public DO.Order AddOrder(Order order); // IS IT SUPPOSED TO RETURN AN INT?!?!??!!!?
    public void DeleteOrder(int ID);
    public DO.Order UpdateOrder(Order order);
    public DO.Order GetOrder(int _id);
    public IEnumerable<DO.Order> GetOrderList();
}