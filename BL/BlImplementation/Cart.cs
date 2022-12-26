using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;
internal class Cart : ICart
{
    static IDal? dal = new DalList();
    public BO.Cart AddToCart(BO.Cart cart, int _ID)
    {
        int index = cart.Items.FindIndex(x => x != null && x.ID == _ID); //save index of order with ID in cart
        DO.Product? product = new DO.Product?();//create a DO product
        product = dal.dalProduct.GetByID(_ID);//get the matching product for the ID
        if (product?.InStock < 1)
        {
            throw new BO.OutOfStockException();
        }
        if (index != -1)//exists in cart
        {
            cart.Items[index].Quantity++; //add another product to the cart
            cart.Items[index].Price += cart.Items[index].Price;//add to the total price 
            cart.TotalPrice += cart.Items[index].Price;//update cart price
            return cart;
        }
        BO.OrderItem item = new BO.OrderItem//create new orderitem that is being added 
        {
            ID = _ID,
            Price = (double)product.Value.Price,
            Quantity = 1,
            ProductID = product.Value.ID
        };
        cart.Items.Add(item);//add the orderitem to cart
        cart.TotalPrice += item.Price;//update price of the cart
        return cart;
    }

    public Cart ViewCart(Cart myCart)
    {


    }

    //public Cart GetCart(string email)
    //{
    //    int index
    //}
    public BO.Cart UpdateCart(BO.Cart cart, int _ID, int quantity)
    {


        int index = cart.Items.FindIndex(x => x.ProductID == _ID); //save index of product with ID in cart
        DO.Product product = new DO.Product();//create a DO product
        try
        {
            product = dal.dalProduct.GetByID(_ID);//get the matching product for the ID
        }
        catch
        {
            throw new BO.DoesNotExistException(product);
        }
        if (index != -1)//if in cart
        {
            if (quantity == 0)
            {
                BO.OrderItem temp = cart.Items[index];//save the orderitem with id
                cart.Items.Remove(temp);//remove orderItem from cart
                cart.TotalPrice -= cart.Items[index].Price;
                return cart;
            }
            cart.TotalPrice -= cart.Items[index].Price * cart.Items[index].Quantity; //substract price of product from cart
            cart.Items[index].Quantity = quantity;//set new amount
            cart.TotalPrice += cart.Items[index].Price * quantity;//add the new price
            return cart;
        }
        throw new BO.DoesNotExistException(product); // IS IT SUPPOSED TO TAKE IN A PRODUCT?
    }
  
    //SEE IF ALL THE IT'S AND ITEM'S MATCH UP!!!!!
    public void MakeOrder(BO.Cart cart)
    {
        if (cart.CustomerName == "" || cart.CustomerEmail == "" || cart.CustomerAddress == "")//check input
        {
            throw new BO.InvalidInputException();
        }
        DO.OrderItem item = new();//create order item
        foreach (BO.OrderItem? it in cart.Items)//go over orderItems in the cart
        {
            try
            {
                if (it.ProductID == dal.dalProduct.GetByID(it.ProductID).ID && it.Quantity > 0 && it.Quantity <= dal.dalProduct.GetByID(it.ProductID).InStock)//if orderItem exists and is instock
                {
                    DO.Order order = new DO.Order();//new DO order
                    order.OrderDate = DateTime.Now;//ordered now
                    int num = dal.dalOrder.Add(order);//add to DO orderlist and get order id
                    item.ProductID = item.ProductID;//save product id
                    item.OrderID = num;//save order id
                    dal.dalOrderItem.Add(item);//add to DO order item list 
                    DO.Product prod = dal.dalProduct.GetByID(item.ProductID);//get matching product
                    prod.InStock -= item.Quantity;//subtract the amount of products in stock
                    dal.dalProduct.Update(prod);//update product in DO
                }
            }
            catch
            {
                throw new BO.DoesNotExistException(item); // is it supposed to be item as an input? 
            }
        }

    }

    /// <summary>
    /// public method to delete a cart (set all the values to null)
    /// </summary>
    public void DeleteCart(BO.Cart myCart)
    {
        myCart.CustomerName = "";
        myCart.CustomerEmail = "";
        myCart.CustomerAddress = "";
        foreach(BO.OrderItem item in myCart.Items)
        {
            myCart.Items.Remove(item);
        }
        myCart.TotalPrice = 0;
    }
}


