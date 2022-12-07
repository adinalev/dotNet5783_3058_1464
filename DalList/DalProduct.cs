﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace Dal;

public class DalProduct
{
    /// <summary>
    /// public method to add a Product
    /// </summary>
    public int AddProduct(Product prod)
    {
        // case 1: Product does not exist yet. Need to intialize and add it.
        if (prod.ID == 0)
        {
            prod.ID = DataSource.Config.NextProductNumber; // set the product ID equal to the next auto-incremental ID from the static variable in DataSource
            DataSource.productList.Add(prod);
            return prod.ID;
        }

        // case 2: Product already exists, throw an exception
        int index = DataSource.productList.IndexOf(prod);
        if (index != -1)
        {
            throw new Exception("Product exists already!\n");
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
    public Product ReadProduct(int _ID)
    {
        Product prod = DataSource.productList.Find(x => x.ID == _ID); // find a product with a matching ID#
        if (prod.ID == 0)
        {
            // if there is no product with a matching ID#
            throw new Exception("Product does not exist!\n");
        }
        return prod;
    }

    /// <summary>
    /// public method to read the Product list
    /// </summary>
    public List<Product> ReadProductList()
    {
        return DataSource.productList.ToList();
    }

    /// <summary>
    /// public method to delete a Product
    /// </summary>
    public void DeleteProduct(int _ID)
    {
        int ind = -1;
        // traverse through the product list and find a product with a matching ID#
        foreach (Product prod in DataSource.productList)
        {
            if (prod.ID == _ID)
            {
                ind = DataSource.productList.IndexOf(prod); // save the index of the product with the matching ID#
                break;
            }
        }
        Product DelProd = DataSource.productList[ind]; // save the product in the found index
        DataSource.productList.Remove(DelProd); // remove the product
    }

    /// <summary>
    /// public method to update a Product
    /// </summary>
    public void UpdateProduct(Product prod)
    {
        int _ID = prod.ID;
        Product OldProd = DataSource.productList.Find(x => x.ID == _ID); // find a product with a matching ID
        if (OldProd.ID == 0) // prod.ID != OldProd.ID
        {
            // if a product with a matching ID was not found
            throw new Exception("Product does not exist!\n");
        }
        int index = DataSource.productList.IndexOf(OldProd); // save the index of the product with matching ID
        DataSource.productList[index] = prod; // add the updated product to the found location of the old product
    }
}