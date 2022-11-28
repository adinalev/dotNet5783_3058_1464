using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal;

public class DalProduct //inherit from product?
{
    DataSource ds = DataSource.ds_instance; // to access the data -- HOW DO WE GET THIS?
    public int addProduct(Product prod)
    {
        if (prod.ID == 0)
        {
            prod.ID = DataSource.Config.nextProductNumber;
            ds.productList.Add(prod);
            return prod.ID;
        }
        //Product existingProd = ds.productList.Find(x => x.ID == prod.ID);
        if (ds.productList.Find(x => x.ID == prod.ID) != null)
        {
            throw new Exception("unauthorized overrried\n"); //throw an error message
        }

           



        int oldID = productList[productList.Count - 1].ID; // getting the ID of the last product in the list
        prod.ID = oldID++;
        // IS THE ID A RUNNING NUMBER?!?!?!
        productList.Add(prod);
        return prod.ID;
    }
}
