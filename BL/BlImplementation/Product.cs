﻿//using DalApi;
//using Dal;
//using 
using System.Collections.ObjectModel;
namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// public method to retrive a list of products for display
    public IEnumerable<BO.ProductForList?> GetProductsForList()
    {
        return from DO.Product item in dal?.dalProduct?.GetAll() // traversing through the list of DO products
               /*where item != null*/
               select new BO.ProductForList //for each of these DO items we are creating a new ProductForList with corresponding criteria
               {
                   ID = item.ID,
                   Name = item.Name,
                   Price = (double)item.Price,
                   Category = (BO.Enums.ProductCategory)item.Category!
               };
    }

    /// <summary>
    /// method to get a BO product
    /// </summary>
    public BO.Product GetProduct(int _ID)
    {
        BO.Product prod = new BO.Product(); // create a BO product
        DO.Product? product = new DO.Product(-1); // create a DO product
        try
        {
            product = dal!.dalProduct.GetByID(_ID); // retrieve the corresponding DO product
        }
        catch
        {
            throw new BO.DoesNotExistException();
        }
        // set the BO product to the same values as the DO product
        prod.ID = _ID;
        prod.Name = product?.Name;
        prod.Price = (int)product?.Price!;
        prod.Category = (BO.Enums.ProductCategory)product?.Category!;
        prod.InStock = (int)product?.InStock!;
        return prod; // return the BO product
    }

    /// <summary>
    /// method to add a product to the product list
    /// </summary>
    public int AddProduct(BO.Product product)
    {
        if (product.Name == "" || product.Price <= 0 || product.InStock < 0 || product.Category < BO.Enums.ProductCategory.MEDICINE || product.Category > BO.Enums.ProductCategory.BABIES) // validating the user input
        {
            throw new BO.InvalidInputException();
        }
        bool check = product.Name.All(Char.IsLetter);
        if (!check)
        {

            throw new BO.InvalidInputException();
        }
        if (product.Category == BO.Enums.ProductCategory.NO_CATEGORY)
        {
            throw new BO.InvalidInputException();
        }
        DO.Product newProduct = new DO.Product(-2); //create new DO product
        // set the DO product attributes equal to the values of the BO product attributes
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.InStock = product.InStock;
        newProduct.Category = (DO.Enums.Category?)product.Category;
        return dal.dalProduct.Add(newProduct); // add to product list
    } 

    /// <summary>
    /// method to delete a product from the product list
    /// </summary>
    public void DeleteProduct(int _ID)
    {
        //var v = from orders in dal?.dalOrder.GetAll()
        //        where orders != null 
        //        select from item in dal?.dalOrderItem.GetAll()
        //               where item != null && item?.OrderID == orders?.ID && item?.ProductID == _ID
        //               select item;
        //if (!v.Any())
        //{
        //    throw new BO.DoesNotExistException();
        //}
        //try
        //{
        //    dal?.dalProduct.Delete(_ID);
        //}
        //catch
        //{
        //    throw new BO.DoesNotExistException();
        //}
        DO.Product? product = new DO.Product(); // create a DO product
        try
        {
            product = dal!.dalProduct.GetByID(_ID); // retrieve the corresponding DO product
        }
        catch
        {
            throw new BO.DoesNotExistException();
        }
        dal?.dalProduct.Delete(_ID); // delete product
    }

    /// <summary>
    /// method to update a product on the product list
    /// </summary>
    public void UpdateProduct(BO.Product product)
    {
        if (product.Name == "" || product.Price <= 0 || product.InStock < 0) // validating user input
        {
            throw new BO.InvalidInputException();
        }
        bool isLetters = product.Name.All(Char.IsLetter);
        if(!isLetters)
        {
            throw new BO.InvalidInputException();
        }
        if (product.Category == BO.Enums.ProductCategory.NO_CATEGORY)
        {
            throw new BO.InvalidInputException();
        }
        int ID = product.ID;
        DO.Product newProduct = new DO.Product(ID); // create a new DO product
        // set the DO product attributes equal to the values of the BO product attributes
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.InStock = product.InStock;
        newProduct.Category = (DO.Enums.Category)product.Category!;
        dal?.dalProduct.Update(newProduct); // send this new DO product to the product update function
    }
    //public IEnumerable<BO.ProductItem> GetCatalog()
    //{
    //    var v = from prods in dal.dalProduct.GetAll()
    //            //where prods != null
    //            select new BO.ProductItem()
    //            {
    //                ID = prods.ID,
    //                Name = prods.Name,
    //                Price = (double)prods.Price,
    //                Amount = (int)prods.InStock,
    //                Category = (BO.Enums.ProductCategory)prods.Category
    //            };
    //    foreach (BO.ProductItem item in v)
    //    {
    //        if (item.Amount > 0)
    //            item.InStock = true;
    //        item.InStock = false;
    //    }
    //    return v;
    //}//go over DO products and build BO product item list 

    /// <summary>
    /// method to get the amount of a certain product in stock
    /// </summary>
    public int GetStockNumber(int ID)
    {
        DO.Product? product = new DO.Product(-1);
        try
        {
            product = dal!.dalProduct.GetByID(ID); // retrieve the corresponding DO product
        }
        catch
        {
            throw new BO.DoesNotExistException(); 
        }
        return (int)product?.InStock!; // return the stock number
    }

    public int GetNextID()
    {
        return DO.Product.productCounter;
    }

    public IEnumerable<BO.ProductItem?> GetCatalog()
    {
        //ObservableCollection<BO.ProductItem?> catalog = new ObservableCollection<BO.ProductItem?>();
        //foreach(DO.Product product in dal.dalProduct.GetAll())
        //{
        //    catalog.Add(new()
        //    {
        //        ID = product.ID,
        //        Name = product.Name!,
        //        Price = (double)product.Price!,
        //        Amount = (int)product.InStock!,
        //        Category = (BO.Enums.ProductCategory)product.Category!,
        //        InStock = product.InStock == 0 ? false : true
        //    });
        //};
        //return catalog;
        var v = from prods in dal?.dalProduct.GetAll()
                where prods != null
                select new BO.ProductItem()
                {
                    ID = prods?.ID ?? throw new BO.DoesNotExistException(),
                    Name = prods?.Name!,
                    Price = (double)prods?.Price!,
                    Amount = (int)prods?.InStock!,
                    Category = (BO.Enums.ProductCategory)prods?.Category!,
                    InStock = prods?.InStock == 0 ? false : true
                };
        return v;
    }
}