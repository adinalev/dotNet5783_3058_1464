using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
using DO;

public interface IDal
{
    IProduct dalProduct { get; }
    IOrder dalOrder { get; }
    IOrderItem dalOrderItem { get; }
}
