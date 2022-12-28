namespace BO;
public class Enums
{
    public enum ProductCategory { MEDICINE = 1, COSMETICS, HYGIENE, FOOD, OPTICS, BABIES }; // the categories in our store
    public enum OrderStatus { New = 1, BeingProcessed, Shipped, Delivered, Unknown };
    public enum Action { ADD = 1, UPDATE, ORDER, RETURN }; // the type of actions that the user can take
    public enum Type { EXIT, PRODUCT, CART, ORDER }; // type of objects
}