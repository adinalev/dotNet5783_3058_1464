using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{
    string orderItemPath = @"OrderItem.xml";
    string configPath = @"Config.xml";
    public int Add(DO.OrderItem item)
    {
        XElement orderItemRoot = XmlTools.LoadListFromXMLElement(orderItemPath);

        // check if the product exists in the file
        var productInFile = (from myItem in orderItemRoot.Elements()
                             where myItem.Element("ID").Value == item.ID.ToString()
                             select myItem).FirstOrDefault();

        // if product is nin the file, then throw an exception
        if (productInFile != null)
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

        orderItemRoot.Add(
            new XElement("OrderItem",
                new XElement("ID", temp),
                new XElement("ProductID", item.ProductID),
                new XElement("OrderID", item.OrderID),
                new XElement("Price", item.Price),
                new XElement("Quantity", item.Quantity)));


        XmlTools.SaveListToXMLElement(orderItemRoot, orderItemPath);
        orderItemRoot.Save(orderItemPath);
        return temp;
    }

    public OrderItem? GetByID(int _ID)
    {
        XElement orderItemRoot = XmlTools.LoadListFromXMLElement(orderItemPath);
        //List<DO.Product?> productList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(productPath).ToList();
        OrderItem? item = (OrderItem)from ordIt in orderItemRoot.Elements()
                              where Convert.ToInt32(ordIt.Element("ID").Value) == _ID
                              select ordIt;
        if (item == null)
        {
            throw new DO.DoesNotExistException();
        }
        return item;
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter)
    {
        XElement orderItemRoot = XmlTools.LoadListFromXMLElement(orderItemPath);
        List<DO.OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath).ToList();
        return (from item in orderItemList
                where filter(item)
                select (DO.OrderItem)item).ToList();
    }

    public void Delete(int _ID)
    {
        XElement orderItemRoot = XmlTools.LoadListFromXMLElement(orderItemPath);
        
        XElement orderItemElement;
        orderItemElement = (from item in orderItemRoot.Elements()
                        where Convert.ToInt32(item.Element("ID").Value) == _ID
                        select item).FirstOrDefault();
        if (orderItemElement == null)
        {
            throw new DO.DoesNotExistException();
        }
        orderItemElement.Remove();
        orderItemRoot.Save(orderItemPath);
    }

    public void Update(DO.OrderItem item)
    { 
        XElement orderItemRoot = XmlTools.LoadListFromXMLElement(orderItemPath);
        XElement orderItemElement = (from ordIt in orderItemRoot.Elements()
                                 where Convert.ToInt32(ordIt.Element("ID").Value) == item.ID
                                 select ordIt).FirstOrDefault();
        orderItemElement.Remove();
        orderItemElement.Element("ID").Value = item.ID.ToString();
        orderItemElement.Element("ProductID").Value = item.ProductID.ToString();
        orderItemElement.Element("OrderID").Value = item.OrderID.ToString();
        orderItemElement.Element("Quantity").Value = item.Quantity.ToString();
        orderItemElement.Element("Price").Value = item.Price.ToString();

        orderItemRoot.Save(orderItemPath);
    }

    public void UpdateByIDs(DO.OrderItem item)
    {

    }

    public OrderItem GetByIDs(int prodID, int ordID)
    {

    }

    public IEnumerable<OrderItem?> GetAllByID(int ordID)
    {

    }

    public OrderItem GetByFilter(Func<OrderItem?, bool>? filter)
    {
        List<DO.OrderItem?> orderItemList = GetAll().ToList();

        return (from item in orderItemList
                where filter(item)
                select (DO.OrderItem)item).FirstOrDefault();
    }
}
