using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi;
/// <summary>
/// Main Interface of Business Layer.
/// It groups the individual interfaces in the layer.
/// </summary>
public interface IBl
{
    public IProduct Product { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }
}
