using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
/// <summary>
/// Interface for the BoEntity "Product"
/// </summary>
public interface IProduct
{
    public DO.Product AddProduct(Product product); // IS IT SUPPOSED TO RETURN AN INT?!?!??!!!?
    public void DeleteProduct(int _id);
    public DO.Product UpdateProduct(Product product);
    public DO.Product GetProduct(int _id);
    public IEnumerable<DO.Product> GetProductList();



}