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
internal sealed class DalList : IDal // changed from sealed
{
    public static IDal Instance { get; } = new DalList(); 
    public IProduct dalProduct => new DalProduct();
    public IOrder dalOrder => new DalOrder();
    public IOrderItem dalOrderItem => new DalOrderItem();
    private DalList()
    {
            
    } // constructor

}
