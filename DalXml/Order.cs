using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class Order : IOrder
{
    string orderPath = @"Order.xml";
    string configPath = @"Config.xml";
    public int Add(DO.Order order)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath);

        // check if the product exists in the file
        var orderInFile = (from ord in orderRoot.Elements()
                             where ord.Element("ID").Value == order.ID.ToString()
                             select ord).FirstOrDefault();

        // if product is nin the file, then throw an exception
        if (orderInFile != null)
        {
            throw new DO.AlreadyExistsException();
        }

        // otherwise, add it to the file
        // get the auto incremental ID number
        List<IncrementalID> IDList = XmlTools.LoadListFromXMLSerializer<IncrementalID>(configPath);

        var runningNumber = (from number in IDList
                             where (number.typeOfnumber == "Order incremental ID")
                             select number).FirstOrDefault();
        IDList.Remove(runningNumber);//remove the saved number from list
        runningNumber.numberSaved++;//add one to the saved number
        IDList.Add(runningNumber);//add the number back to list
        int temp = (int)runningNumber.numberSaved;//save the running number

        orderRoot.Add(
            new XElement("Order",
                new XElement("ID", temp),
                new XElement("CustomerName", order.CustomerName),
                new XElement("Email", order.Email),
                new XElement("Address", order.Address),
                new XElement("OrderDate", order.OrderDate),
                new XElement("ShippingDate", order.ShippingDate),
                new XElement("DeliveryDate", order.DeliveryDate)));


        XmlTools.SaveListToXMLElement(orderRoot, orderPath);
        orderRoot.Save(orderPath);
        return temp;

    }

    public Order? GetByID(int _ID)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath);
        //List<DO.Product?> productList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(productPath).ToList();
        Order? order = (Order)from ord in orderRoot.Elements()
                                   where Convert.ToInt32(ord.Element("ID").Value) == _ID
                                   select ord;
        if (order == null)
        {
            throw new DO.DoesNotExistException();
        }
        return order;
    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath);
        List<DO.Order?> orderList = XmlTools.LoadListFromXMLSerializer<DO.Order?>(orderPath).ToList();
        return (from order in orderList
                where filter(order)
                select (DO.Order)order).ToList();
    }

    public void Delete(int _ID)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath);

        XElement orderElement;
        orderElement = (from ord in orderRoot.Elements()
                          where Convert.ToInt32(ord.Element("ID").Value) == _ID
                          select ord).FirstOrDefault();
        if (orderElement == null)
        {
            throw new DO.DoesNotExistException();
        }
        orderElement.Remove();
        orderRoot.Save(orderPath);
    }

    public void Update(DO.Order order)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath);
        XElement orderElement = (from ord in orderRoot.Elements()
                                   where Convert.ToInt32(ord.Element("ID").Value) == order.ID
                                   select ord).FirstOrDefault();
        orderElement.Remove();
        orderElement.Element("CustomerName").Value = order.CustomerName;
        orderElement.Element("Email").Value = order.Email;
        orderElement.Element("Address").Value = order.Address;
        orderElement.Element("OrderDate").Value = order.OrderDate.ToString();
        orderElement.Element("ShippingDate").Value = order.ShippingDate.ToString();
        orderElement.Element("DeliveryDate").Value = order.DeliveryDate.ToString();

        orderRoot.Save(orderPath);
    }

    public Order GetByFilter(Func<Order?, bool>? filter)
    {
        List<DO.Order?> orderList = GetAll().ToList();

        return (from ord in orderList
                where filter(ord)
                select (DO.Order)ord).FirstOrDefault();
    }
}
