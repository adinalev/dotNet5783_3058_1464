using BO;
namespace BlApi;

public interface ICart
{
    /// <summary>
    /// public method to add a product to the cart if it is in stock and not already in the cart
    /// </summary>
    public BO.Cart AddToCart(BO.Cart myCart, int ID);
    /// <summary>
    /// public method to update the amount of products in the cart and the total price
    /// </summary>
    public BO.Cart UpdateCart(BO.Cart myCart, int ID, int newQuantity);
    /// <summary>
    /// public method to approve the items int he cart and to make the order
    /// </summary>
    public void MakeOrder(BO.Cart myCart, string name, string email, string address);
}
