using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;
/// <summary>
/// Class Bl that carries public access permission, implements the interface IBl,
/// and implements the properties defined in the IDal interface
/// </summary>
sealed public class Bl : IBl
{
    public IProduct Product => new Product();
    public IOrder Order => new Order();
    public IOrderItem OrderItem => new OrderItem();
}
