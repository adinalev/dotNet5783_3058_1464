using DalApi;
using Dal;
using BlApi;
namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    static IDal? dal = new DalList();
    public IEnumerable<BO.ProductForList?> GetProductsForList()
    {
        return from DO.Product? item in dal.dalProduct.GetAll()
               where item != null
               select new BO.ProductForList
               {
                   ID = item.Value.ID,
                   Name = item?.Name,
                   Price = (double)item?.Price,
                   Category = (BO.Enums.ProductCategory)item?.Category
               };

    }//returns a list of products for the manager


    public BO.Product GetProduct(int _ID)
    {
        BO.Product prod = new BO.Product();//create a BO product
        DO.Product product = new DO.Product();//create a DO product
        try
        {
            product = dal.dalProduct.GetByID(_ID);//get the matching product for the ID
        }
        catch
        {
            throw new BO.DoesNotExistException(prod); // PROD? PRODUCT?
        }

        prod.ID = _ID;
        prod.Name = product.Name;
        prod.Price = product.Price;
        prod.Category = (BO.Enums.ProductCategory)product.Category;
        prod.InStock = product.InStock;

        return prod;
    }//return a BO product of DO product with id

    public void AddProduct(BO.Product product)
    {
        if (product.Name == "" || product.Price <= 0 || product.InStock < 0 || product.Category < BO.Enums.ProductCategory.MEDICINE || product.Category > BO.Enums.ProductCategory.BABIES)
        {
            throw new BO.InvalidInputException();
        }
        //DO.Product prod = DOList.Product.GetById(p.ID);//get product with id
        //if (prod.ID == p.ID)//already exists 
        //    throw new BO.IdExistException();

        DO.Product newProduct = new DO.Product(); //create new DO product
        //newProduct.ID = 0;
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.InStock = product.InStock;
        newProduct.Category = (DO.Enums.Category)product.Category;

        dal.dalProduct.Add(newProduct);//add to product list
    }//gets a BO product, check if right and add a DO product 

    public void DeleteProduct(int _ID)
    {
    //    var v = from orders in dal.dalOrder.GetAll()
    //            where orders != null
    //            select from items in dal.dalOrderItem.GetAll()
    //                   where items != null && items?.OrderID == orders?.ID && items?.ProductID == _ID
    //                   select items;
    //    if (v.Any() == false)//no matching order items were found
    //    {
    //        throw new BO.DoesNotExistException(orders); // FIGURE THIS ONE OUT
    //        //throw new BO.UnfoundException();//id not found
    //    }
        dal.dalProduct.Delete(_ID);//delete product
        //DOList.Product.Delete(id);//remove orderItem
    }
    public void UpdateProduct(BO.Product product)
    {
        if (product.ID < 100 || product.Name == "" || product.Price <= 0 || product.InStock < 0)
        {
            throw new BO.InvalidInputException();
        }
        DO.Product newProduct = new DO.Product();
        newProduct.ID = product.ID;
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.InStock = product.InStock;
        newProduct.Category = (DO.Enums.Category)product.Category;
        dal.dalProduct.Update(newProduct);
    }//get BO product, check if right and updates DO product


    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        var v = from prods in dal.dalProduct.GetAll()
                //where prods != null
                select new BO.ProductItem()
                {
                    ID = prods.ID,
                    Name = prods.Name,
                    Price = (double)prods.Price,
                    Amount = (int)prods.InStock,
                    Category = (BO.Enums.ProductCategory)prods.Category
                };
        foreach (BO.ProductItem item in v)
        {
            if (item.Amount > 0)
                item.InStock = true;
            item.InStock = false;
        }
        return v;
    }//go over DO products and build BO product item list 
}


//internal class Product : BlApi.IProduct
//{
//    private IDal Dal = new DalList();

//    // if we're returning a BO.Product then how do we convert the BO to the DO?
//    public DO.Product AddProduct(BO.Product product)
//    {
//        if (product.Name == "" || product.Price <= 0 || product.InStock <= 0 || product.Category < BO.Enums.ProductCategory.MEDICINE || product.Category > BO.Enums.ProductCategory.BABIES)
//        {
//            throw new BO.InvalidInputException();
//        }
//        DO.Product newProduct = new DO.Product();
//        newProduct.Name = product.Name;
//        newProduct.Price = product.Price;
//        newProduct.InStock = product.InStock;
//        newProduct.Category = (DO.Enums.Category)product.Category;
//        newProduct.ID = Dal.dalProduct.Add(newProduct);
//        return newProduct; // need this?
//    }

//    public void DeleteProduct(int _id)
//    {
//        DO.Product delProd = new DO.Product();
//        int delID = -1;
//        foreach(DO.Product product in Dal.dalProduct.GetAll())
//        {
//            if (_id == product.ID)
//                delID = product.ID;
//        }
//        if (delID == -1)
//        {
//            throw new BO.DoesNotExistException(delProd);
//        }
//        Dal.dalProduct.Delete(delID);
//    }

//    public DO.Product UpdateProduct(BO.Product product)
//    {
//        DO.Product newProduct = new DO.Product();
//        int _id = -1;
//        foreach(DO.Product prod in Dal.dalProduct.GetAll())
//        {
//            if (prod.ID == product.ID)
//                _id = prod.ID;
//        }
//        if (_id == -1)
//        {
//            throw new BO.DoesNotExistException(newProduct);
//        }
//        if (product.Name == "" || product.Price <= 0 || product.InStock <= 0 || product.Category < BO.Enums.ProductCategory.MEDICINE || product.Category > BO.Enums.ProductCategory.BABIES)
//        {
//            throw new BO.InvalidInputException();
//        }
//        newProduct.ID = product.ID;
//        newProduct.Name = product.Name;
//        newProduct.Price = product.Price;
//        newProduct.InStock = product.InStock;
//        newProduct.Category = (DO.Enums.Category)product.Category;
//        Dal.dalProduct.Update(newProduct);
//        return newProduct; // need this??
//    }

//    public DO.Product GetProduct(int ID)
//    {
//        int _id = -1;
//        DO.Product product = new DO.Product();
//        foreach (DO.Product prod in Dal.dalProduct.GetAll())
//        {
//            if (prod.ID == ID)
//            {
//                product = prod;
//                _id = prod.ID;
//            }
//        }
//        if (_id == -1)
//        {
//            throw new BO.DoesNotExistException(product);
//        }
//        return product;
//    }

//    public IEnumerable<DO.Product> GetProductList()
//    {
//        return Dal.dalProduct.GetAll();
//    }
//}
