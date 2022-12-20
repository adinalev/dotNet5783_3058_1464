using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Enums
{
    public enum ProductCategory { MEDICINE = 1, COSMETICS, HYGIENE, FOOD, OPTICS, BABIES }; // the categories in our store
    public enum OrderStatus { NEW = 1, INPROGRESS, COMPLETED, CANCELLED };
    public enum Action { ADD = 1, UPDATE, ORDER, RETURN }; // the type of actions that the user can take
    public enum Type { EXIT, PRODUCT, CART, ORDER }; // type of objects
}