using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
using DO;
namespace Dal;
internal class DalProduct : IProduct
{
    /// <summary>
    /// public method to add a Product
    /// </summary>
    public int Add(DO.Product prod)
    {
        // case 1: Product does not exist yet. Need to intialize and add it.
        if (prod.ID == -2)
        {
            Product product = new Product(); // this will use the init to get the next ID#
            product.ID = Product.productCounter++;
            product.Name = prod.Name;
            product.Price = prod.Price;   
            product.InStock = prod.InStock;
            product.Category = prod.Category;
            DataSource.productList.Add(product);
            return product.ID; 
        }

        // case 2: Product already exists, throw an exception
        int index = DataSource.productList.IndexOf(prod);
        if (index != -1)
        {
            throw new AlreadyExistsException();
        }
        else
        // Product is initialized but it's not in the list yet
        {
            DataSource.productList.Add(prod);
            return prod.ID;
        }
    }

    /// <summary>
    /// public method to read a Product
    /// </summary>
    public DO.Product? GetByID(int _ID) 
    {
        DO.Product? prod = DataSource.productList.Find(x => x?.ID == _ID); // find a product with a matching ID#
        if (prod == null)
        {
            // if there is no product with a matching ID#
            throw new DoesNotExistException();
        }
        return prod;
    }

    /// <summary>
    /// public method to read the Product list
    /// </summary>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter)
    {
        if (filter == null)//select whole list
        {
            return from prod in DataSource.productList
                   where prod != null
                   select prod;
        }
        return from myProd in DataSource.productList//select with filter
               where myProd != null && filter(myProd)
               select myProd;

        // return DataSource.productList.ToList(); //-- this was the entire function!!!
    }

    /// <summary>
    /// public method to delete a Product
    /// </summary>
    public void Delete(int _ID) 
    {
        int ind = -1;
        // traverse through the product list and find a product with a matching ID#
        foreach (DO.Product prod in DataSource.productList)
        {
            if (prod.ID == _ID)
            {
                ind = DataSource.productList.IndexOf(prod); // save the index of the product with the matching ID#
                break;
            }
        }
        if (ind == -1)
        {
            throw new DoesNotExistException();
        }
        DO.Product DelProd = (Product)DataSource.productList[ind]!; // save the product in the found index // ADDED A CAST!!!!
        DataSource.productList.Remove(DelProd); // remove the product
    }

    /// <summary>
    /// public method to update a Product
    /// </summary>
    public void Update(DO.Product prod) 
    {
        int _ID = prod.ID;
        DO.Product? OldProd = DataSource.productList.Find(x => x?.ID == _ID); // find a product with a matching ID
        if (OldProd == null) // prod.ID != OldProd.ID // used to be OldProd?.ID == 0
        {
            // if a product with a matching ID was not found
            throw new DoesNotExistException();
        }
        int index = DataSource.productList.IndexOf(OldProd); // save the index of the product with matching ID
        DataSource.productList[index] = prod; // add the updated product to the found location of the old product
    }

    public Product GetByFilter(Func<Product?, bool>? filter)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));//filter is null
        }
        foreach (Product? product in DataSource.productList)
        {
            if (product != null  && filter(product))
            {
                return (Product)product;
            }
        }
        throw new DoesNotExistException();
    }
}