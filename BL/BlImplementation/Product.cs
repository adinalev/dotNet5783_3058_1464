using DalApi;
using Dal;
using BlApi;
namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal Dal = new DalList();

    // if we're returning a BO.Product then how do we convert the BO to the DO?
    public DO.Product AddProduct(BO.Product product)
    {
        if (product.Name == "" || product.Price <= 0 || product.InStock <= 0 || product.Category < BO.Enums.Category.MEDICINE || product.Category > BO.Enums.Category.BABIES)
        {
            throw new BO.InvalidInputException();
        }
        DO.Product newProduct = new DO.Product();
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.InStock = product.InStock;
        newProduct.Category = (DO.Enums.Category)product.Category;
        newProduct.ID = Dal.dalProduct.Add(newProduct);
        return newProduct; // need this?
    }

    public void DeleteProduct(int _id)
    {
        DO.Product delProd = new DO.Product();
        int delID = -1;
        foreach(DO.Product product in Dal.dalProduct.GetAll())
        {
            if (_id == product.ID)
                delID = product.ID;
        }
        if (delID == -1)
        {
            throw new BO.DoesNotExistException(delProd);
        }
        Dal.dalProduct.Delete(delID);
    }

    public DO.Product UpdateProduct(BO.Product product)
    {
        DO.Product newProduct = new DO.Product();
        int _id = -1;
        foreach(DO.Product prod in Dal.dalProduct.GetAll())
        {
            if (prod.ID == product.ID)
                _id = prod.ID;
        }
        if (_id == -1)
        {
            throw new BO.DoesNotExistException(newProduct);
        }
        if (product.Name == "" || product.Price <= 0 || product.InStock <= 0 || product.Category < BO.Enums.Category.MEDICINE || product.Category > BO.Enums.Category.BABIES)
        {
            throw new BO.InvalidInputException();
        }
        newProduct.ID = product.ID;
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.InStock = product.InStock;
        newProduct.Category = (DO.Enums.Category)product.Category;
        Dal.dalProduct.Update(newProduct);
        return newProduct; // need this??
    }

    public DO.Product GetProduct(int ID)
    {
        int _id = -1;
        DO.Product product = new DO.Product();
        foreach (DO.Product prod in Dal.dalProduct.GetAll())
        {
            if (prod.ID == ID)
            {
                product = prod;
                _id = prod.ID;
            }
        }
        if (_id == -1)
        {
            throw new BO.DoesNotExistException(product);
        }
        return product;
    }

    public IEnumerable<DO.Product> GetProductList()
    {
        return Dal.dalProduct.GetAll();
    }
}
