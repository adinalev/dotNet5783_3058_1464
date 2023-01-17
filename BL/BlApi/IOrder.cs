using BO;
namespace BlApi;
/// <summary>
/// Interface for the BoEntity "Order"
/// </summary>
public interface IOrder
{
    /// <summary>
    /// public method to get all the orders in list form
    /// </summary>
    public List<OrderForList?>? GetAllOrderForList();

    /// <summary>
    /// public method to get a BO order using the inputted ID
    /// </summary>
    public BO.Order? GetBoOrder(int ID);

    /// <summary>
    /// public method for the manager to update the shipping date of an order
    /// </summary>
    public BO.Order? UpdateShippingDate(int orderID, DateTime date);

    /// <summary>
    /// public method for the manager to update the delivery date of an order
    /// </summary>
    public BO.Order? UpdateDeliveryDate(int orderID, DateTime date);

    /// <summary>
    /// public method to update and return the status of anorder
    /// </summary>
    public BO.Enums.OrderStatus GetStatus(DO.Order? order);

    /// <summary>
    /// public method to track an order
    /// </summary>
    public OrderTracking TrackOrder(int orderID);
    public List<string> GetItemNames(int orderID);

}