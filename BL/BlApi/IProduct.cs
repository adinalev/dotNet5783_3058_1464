using BO;
using System.Collections.ObjectModel;
namespace BlApi;
/// <summary>
/// Interface for the BoEntity "Product"
/// </summary>
public interface IProduct
{
    /// <summary>
    /// public method to return a list of products to display
    /// </summary>
    public IEnumerable<ProductForList?>? GetProductsForList();

    /// <summary>
    /// public method to get a product using the ID
    /// </summary>
    public Product GetProduct(int ID);

    /// <summary>
    /// public method for a manager to add a product to the list
    /// </summary>
    public int AddProduct(Product product);

    /// <summary>
    /// public method for a manager to delete a product from the catalog
    /// </summary>
    public void DeleteProduct(int id);

    /// <summary>
    /// public method for a manager to update a product information
    /// </summary>
    public void UpdateProduct(Product product);

    //public IEnumerable<ProductItem?> GetCatalog();

    /// <summary>
    /// public method to return the amount of product in stock
    /// </summary>
    public int GetStockNumber(int ID);
    public int GetNextID();

    public IEnumerable<ProductItem?> GetCatalog();
}