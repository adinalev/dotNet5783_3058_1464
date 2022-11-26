using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalProduct
{
    public int addProduct(Product prod)
    {
        int oldID = productList[productList.Count - 1].ID; // getting the ID of the last product in the list
        prod.ID = oldID++;
        // IS THE ID A RUNNING NUMBER?!?!?!
        productList.Add(prod);
        return prod.ID;
    }
}
