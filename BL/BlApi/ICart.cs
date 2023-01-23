using BO;
namespace BlApi;
public interface ICart
{
    /// <summary>
    /// public method to add a product to the cart if it is in stock and not already in the cart
    /// </summary>
    public BO.Cart AddToCart(BO.Cart myCart, int ID, int amount);

    /// <summary>
    /// public method to update the amount of products in the cart and the total price
    /// </summary>
    public int InCart(BO.Cart cart, int prodID);

    /// <summary>
    /// public method to get a list of the names of the items in the cart
    /// </summary>
    public List<string?>? GetItemNames(BO.Cart cart); //added question mark here

    /// <summary>
    /// public method too update the amount of a pre-existing item in the cart
    /// </summary>
    public BO.Cart UpdateCart(BO.Cart myCart, int ID, int newQuantity);

    /// <summary>
    /// public method to approve the items in the cart and to make the order
    /// </summary>
    public int MakeOrder(BO.Cart myCart,string name, string email, string address);

    /// <summary>
    /// public method to delete the details of the cart
    /// </summary>
    public void DeleteCart(BO.Cart myCart);
    public IEnumerable<BO.OrderItem> GetItems(BO.Cart cart);
    public BO.Cart IncreaseCart(BO.Cart cart, int ID);
    public BO.Cart DecreaseCart(BO.Cart cart, int ID);
    //public int AmountInCart(BO.Cart cart);

}
