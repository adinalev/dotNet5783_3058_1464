//using BlApi;
namespace BlImplementation;



/// <summary>
/// Class Bl that carries public access permission, implements the interface IBl,
/// and implements the properties defined in the IDal interface
/// </summary>
sealed internal class Bl : BlApi.IBl 
{
    public BlApi.IProduct Product => new Product(); 
    public BlApi.IOrder Order => new Order();
    public BlApi.ICart Cart => new Cart();
}
