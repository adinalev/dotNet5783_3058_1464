//using BlApi;
//using DalApi;
//using Dal;
namespace BlImplementation;
internal class Cart : BlApi.ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    static BlApi.IBl bl = new Bl();

    /// <summary>
    /// method to add to the cart
    /// </summary>
    public BO.Cart AddToCart(BO.Cart cart, int _ID, int amount)
    {
        if (cart.Items == null)
        {
            DO.Product? prod = new DO.Product();
            try
            {
                prod = dal.dalProduct.GetByID(_ID);
            }
            catch
            {
                throw new BO.DoesNotExistException();
            }
            if (amount < 0) // if the amount is negative
            {
                throw new BO.InvalidInputException();
            }
            if (prod?.InStock < 1) // if the product has nothing left in stock
            {
                throw new BO.OutOfStockException();
            }
            if (prod?.InStock < amount) // if there is not enough products in stock
            {
                throw new BO.NotEnoughInStockException();
            }
            BO.OrderItem myItem = new BO.OrderItem//create new orderitem that is being added 
            { // Quantity used to be equal to 1 but i dont think it makes a difference anymore
                ID = _ID,
                ProductName = prod?.Name!,
                ProductPrice = (double)prod?.Price!,
                Quantity = amount,
                Price = (double)prod?.Price!,
                ProductID = (int)prod?.ID,
                OrderID = bl.Order.GetNextID()
            };
            cart.Items = new List<BO.OrderItem?>();//new list in cart bcs first order item
            cart.Items!.Add(myItem);//add the orderitem to cart
            cart.TotalPrice = (double)prod?.Price!;//add the prudoct price to cart price
            cart.AmountOfItems += amount;
            return cart;
        }

        int index = cart.Items.FindIndex(x => x.ID == _ID); // find the index of where the product is sitting in the Items list
        DO.Product? product = new DO.Product(-1); // create a DO product
        product = dal!.dalProduct.GetByID(_ID); // get the DO product with the matching ID.
        if (amount < 0) // if the amount is negative
        {
            throw new BO.InvalidInputException();
        }
        if (product?.InStock < 1) // if the product has nothing left in stock
        {
            throw new BO.OutOfStockException();
        }
        if (product?.InStock < amount) // if there is not enough products in stock
        {
            throw new BO.NotEnoughInStockException();
        }
        if (index != -1) // if the product really is in the cart
        {
            cart.Items[index]!.Quantity += amount; // make the quantity of that product equal to the amount inputted 
            cart.TotalPrice += cart.Items[index]!.Price * amount; // adjust the total price accordingly
            cart.AmountOfItems += amount;
            return cart;
        }
        BO.OrderItem item = new BO.OrderItem // create new orderitem that is being added to the list
        {
            ID = _ID,
            ProductName = product?.Name!,
            Price = (double)product?.Price!,
            Quantity = amount,
            ProductID = (int)product?.ID!,
            OrderID = bl.Order.GetNextID()
        };
        cart.Items.Add(item); // add the orderitem to the item list in the cart
        cart.TotalPrice += item.Price * amount; // update price of the cart accordingly
        cart.AmountOfItems += amount;
        return cart;
    }

    /// <summary>
    /// method to return the names of all the items that are currently in the cart
    /// </summary>
    public List<string?> GetItemNames(BO.Cart cart)
    {
        int productID;
        DO.Product? product = new DO.Product(-1); // create a new DO product                              
        List<string?> list = new List<string?>(); // create a list to hold a string of names
        // loop to get all the names of the products in the cart and to add them to the list of strings
        foreach(BO.OrderItem? item in cart.Items!)
        {
            productID = item!.ID;
            product = dal!.dalProduct.GetByID(productID);
            list.Add(product?.Name);
        }
        return list; // return the list of names
    }

    /// <summary>
    /// method to return how much of a specific product is sitting in the cart
    /// </summary>
    public int InCart(BO.Cart cart, int prodID)
    {
        int amount = 0;
        bool exists = false;
        // loop which checks how much of a product is already in the cart
        //cart.Items.ForEach(item => item.ProductID == prodID );
        var v = from item in cart.Items
                where item.ProductID == prodID
                select item;
        foreach(var item in v)
        {
            exists = true;
            amount = item.Quantity;
        }
        //foreach(BO.OrderItem? item in cart.Items!)
        //{
        //    if (item?.ProductID == prodID)
        //    {
        //        exists = true;
        //        amount = item.Quantity;
        //        break;
        //    }
        //}
        if (!exists) throw new BO.NotInCartException(); // if the product is not in the cart yet, throw an exception
        return amount;
    }

    /// <summary>
    /// method to adjust the amount of a product is in the cart
    /// </summary>
    public BO.Cart IncreaseCart(BO.Cart cart, int _ID)
    {
        int index = cart.Items!.FindIndex(x => x?.ProductID == _ID); // find the index in the items list where the product sits
        DO.Product? product = new DO.Product(-1); // create a new DO product
        try
        {
            product = dal!.dalProduct.GetByID(_ID); //retrieve the product with the matching ID
        }
        catch
        {
            throw new BO.DoesNotExistException();
        }
        if (index != -1) // this means the product is in the cart
        {
            if (product?.InStock < cart.Items[index].Quantity + 1)
            {
                throw new BO.NotEnoughInStockException();
            }
            //cart.TotalPrice -= cart.Items[index]!.Price * cart.Items[index]!.Quantity; // subtract the cost of any of this specific product sitting in the cart
            //cart.Items[index]!.Quantity = quantity; // change the quantity of that product to the user's input
            cart.Items[index].Quantity += 1;
            cart.TotalPrice += cart.Items[index]!.ProductPrice; // adjust the price accordingly // CHANGED THIS
            cart.Items[index].Price = cart.Items[index].Price + cart.Items[index].ProductPrice;
            cart.AmountOfItems += 1;
            return cart;
        }
        throw new BO.DoesNotExistException();
    }

    public BO.Cart DecreaseCart(BO.Cart cart, int ID)
    {
        int index = cart.Items!.FindIndex(x => x?.ProductID == ID); // find the index in the items list where the product sits
        DO.Product? product = new DO.Product(-1); // create a new DO product
        try
        {
            product = dal!.dalProduct.GetByID(ID); //retrieve the product with the matching ID
        }
        catch
        {
            throw new BO.DoesNotExistException();
        }
        if (index != -1) // this means the product is in the cart
        {
            if(cart.Items[index].Quantity == 1)
            {
                cart.Items.RemoveAt(index);
                //cart.Items[index].Quantity = 0;
                return cart;
            }
            cart.Items[index].Quantity -= 1;
            cart.TotalPrice -= cart.Items[index]!.ProductPrice; // adjust the price accordingly
            cart.Items[index].Price = cart.Items[index].Price - cart.Items[index].ProductPrice;
            cart.AmountOfItems -= 1;
            return cart;
        }
        throw new BO.DoesNotExistException();
    }


    /// <summary>
    /// method to adjust the amount of a product is in the cart
    /// </summary>
    public BO.Cart UpdateCart(BO.Cart cart, int _ID, int quantity)
    {
        int index = cart.Items!.FindIndex(x => x?.ProductID == _ID); // find the index in the items list where the product sits
        DO.Product? product = new DO.Product(-1); // create a new DO product
        try
        {
            product = dal!.dalProduct.GetByID(_ID); //retrieve the product with the matching ID
        }
        catch
        {
            throw new BO.DoesNotExistException();
        }
        if (index != -1) // this means the product is in the cart
        {
            if (quantity == 0) // if there user wants 0 of this product in the cart
            {
                BO.OrderItem temp = cart.Items[index]!; // save the order item that's sitting in the found index
                cart.TotalPrice -= cart.Items[index]!.Price; // adjust the price accordingly
                cart.Items.Remove(temp); // remove order item from cart
                return cart;
            }
            if (quantity > product?.InStock) // if there's not enough in stock to fulfill the user's request
            {
                throw new BO.OutOfStockException();
            }
            cart.TotalPrice -= cart.Items[index]!.Price * cart.Items[index]!.Quantity; // subtract the cost of any of this specific product sitting in the cart
            cart.Items[index]!.Quantity = quantity; // change the quantity of that product to the user's input
            cart.TotalPrice += cart.Items[index]!.Price * quantity; // adjust the price accordingly
            return cart;
        }
        throw new BO.DoesNotExistException();
    }

    //public int MakeOrder(BO.Cart cart, string name, string email, string address)
    //{
    //    if (name == "" || email == "" || address == "")//check input
    //    {
    //        throw new BO.InvalidInputException();
    //    }
    //    cart.CustomerName = name;
    //    cart.CustomerEmail = email;
    //    cart.CustomerAddress = address;
    //    DO.Product product = new DO.Product();
    //    DO.Order order = new DO.Order(); // create an instance of order
    //    BO.Order orderBO = new BO.Order();
    //    int ordID = dal.dalOrder.Add(order); // adding a new order to the list (this is the new order)
    //    order.OrderDate = DateTime.Now;
    //    orderBO.ID = ordID;
    //    orderBO.CustomerName = order.CustomerName;
    //    orderBO.Email = order.Email;
    //    orderBO.Address = order.Address;
    //    orderBO.PaymentDate = order.OrderDate;
    //    orderBO.ShippingDate = order.ShippingDate;
    //    orderBO.DeliveryDate = order.DeliveryDate;
    //    orderBO.Status = bl.Order.GetStatus(order); // SET THE STATUS!!!
    //    orderBO.TotalPrice = cart.TotalPrice;
    //    int quantity = 0;
    //    foreach (BO.OrderItem? item in cart.Items!)
    //    {
    //        orderBO.Items.Add(item);
    //        quantity++;
    //    }
    //    try
    //    {
    //        foreach (BO.OrderItem it in cart.Items)
    //        {
    //            int quant = it.Quantity;
    //            DO.OrderItem item = new DO.OrderItem();
    //            item.ProductID = it.ProductID;
    //            item.OrderID = ordID;
    //            product = (DO.Product)dal?.dalProduct.GetByID(it.ProductID);
    //            if (product.InStock < quant)
    //            {
    //                throw new BO.NotEnoughInStockException();
    //            }
    //            product.InStock -= quant;
    //            dal.dalProduct.Update(product);
    //        }
    //    }
    //    catch
    //    {
    //        throw new DO.DoesNotExistException();
    //    }

    //    return ordID;
    //}


    /// <summary>
    /// method to place an order
    /// </summary>
    public int MakeOrder(BO.Cart cart, string name, string email, string address)
    {
        if (name == "" || email == "" || address == "") // validating the user's input
        {
            throw new BO.InvalidInputException();
        }
        cart.CustomerName = name;
        cart.CustomerEmail = email;
        cart.CustomerAddress = address;

        // add a new order for the cart and get its ID
        IEnumerable<DO.Product?> productList = dal?.dalProduct.GetAll()!;//get all products from dal
        //IEnumerable<string> check = from BO.OrderItem item in cart.Items!
        //                            let prod = productList.FirstOrDefault(x => x?.ID == item.ID)
        //                            where item.Quantity < 1 || prod?.InStock < item.Quantity
        //                            select item.ProductName + " is not in stock\n";//check if all of the products in cart are in stock
        //if (check.Any())//if no products are available 
        //    throw new BO.NotEnoughInStockException();
        //int counter = 0;
        //foreach(BO.OrderItem? item in cart?.Items)
        //{
        //    counter++;
        //}
        //var v = from item in cart.Items
        //        select item;
        //foreach(var item in v)
        //{
        //    counter += item.Quantity;
        //}

        int ? ordID = dal?.dalOrder.Add(new DO.Order()
        {
            CustomerName = cart.CustomerName!,
            Email = cart.CustomerEmail!,
            Address = cart.CustomerAddress!,
            OrderDate = DateTime.Now,
            TotalPrice = cart.TotalPrice,
            //AmountOfItems = cart.AmountOfItems
        }) ;

        try
        {
            cart.Items?.ForEach(x => dal?.dalOrderItem.Add(new DO.OrderItem()
            {
                Quantity = x.Quantity,
                ID = x.ID,
                OrderID = (int)ordID!,
                Price = x.Price,
                ProductID = x.ProductID,
            }));
        }
        catch (DO.AlreadyExistsException exc)
        {
            throw new BO.AlreadyExistsException();
        }
        catch (DO.TooManyProductsException exc)
        {
            throw new BO.TooManyProductsException();
        }
        try
        {

            //cart.Items.ForEach(x => (DO.Product)dal.dalProduct.GetByID(x!.ProductID).InStock -= x!.Quantity);
            cart?.Items?.ForEach(x => dal?.dalProduct.Update((DO.Product)dal?.dalProduct?.GetByID(x.ProductID)));
        }
        catch (DO.DoesNotExistException exc)
        {
            throw new BO.DoesNotExistException();
        }
        return (int)ordID!;
    }

    /// <summary>
    /// public method to delete a cart (set all the values to null)
    /// </summary>
    public void DeleteCart(BO.Cart myCart)
    {
        // set all the values to null/zero and clear the items list
        myCart.CustomerName = "";
        myCart.CustomerEmail = "";
        myCart.CustomerAddress = "";
        myCart.Items!.Clear();
        myCart.TotalPrice = 0;
    }

    public IEnumerable<BO.OrderItem> GetItems(BO.Cart cart)
    {
        return from items in cart.Items select items;
    }


}



