using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
namespace Dal;

/// <summary>
/// public class to implement the interface IDal
/// </summary>
sealed public class DalList : IDal
{
    public IProduct dalProduct => new DalProduct();
    public IOrder dalOrder => new DalOrder();
    public IOrderItem dalOrderItem => new DalOrderItem();
}
