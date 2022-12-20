using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;
//using BO;
/// <summary>
/// Class Bl that carries public access permission, implements the interface IBl,
/// and implements the properties defined in the IDal interface
/// </summary>
sealed public class Bl : IBl
{
    public IProduct Product => new Product(); 
    public IOrder Order => new Order();
    //public IOrderItem OrderItem => new OrderItem();
    public ICart Cart => new Cart();
    //public IOrderTracking OrderTracking => new OrderTracking();
    //public IOrderForList OrderForList => new OrderForList();
    //public IProductForList ProductForList => new ProductForList();
    //public IProductItem ProductItem => new ProductItem();
}
