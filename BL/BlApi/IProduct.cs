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
    // REDO ALL THE COMMENTING

    // the managerial methods
    public IEnumerable<ProductForList?> GetProductsForList();//returns a list of products for the manager
    public Product ManagerProduct(int ID);//return a BO product of DO product with id
    public void AddProduct(Product product);//gets a BO product, check if right and add a DO product 
    public void DeleteProduct(int id);//check in every order that DO product is deleted 
    public void UpdateProduct(Product product);//get BO product, check if right and updates DO product

    //customer user functions
    public IEnumerable<ProductItem?> GetCatalog();//get product list of DO and and return productItem list of BO

    //public DO.Product AddProduct(Product product); // IS IT SUPPOSED TO RETURN AN INT?!?!??!!!?
    //public void DeleteProduct(int _id);
    //public DO.Product UpdateProduct(Product product);
    //public DO.Product GetProduct(int _id);
    //public IEnumerable<DO.Product> GetProductList();
}