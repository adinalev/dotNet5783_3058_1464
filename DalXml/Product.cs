using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class Product : IProduct
{
    string productPath = @"Product.xml";
    string configPath = @"Config.xml";
    public int Add(DO.Product product)
    {
        XElement productRoot = XmlTools.LoadListFromXMLElement(productPath);

        // check if the product exists in the file
        var productInFile = (from prod in productRoot.Elements()
                             where prod.Element("ID").Value == product.ID.ToString()
                             select prod).FirstOrDefault();

        // if product is nin the file, then throw an exception
        if (productInFile != null)
        {
            throw new DO.AlreadyExistsException();
        }

        // otherwise, add it to the file
        // get the auto incremental ID number
        List<IncrementalID> IDList = XmlTools.LoadListFromXMLSerializer<IncrementalID>(configPath);

        var runningNumber = (from number in IDList
                          where (number.typeOfnumber == "Product incremental ID")
                          select number).FirstOrDefault();
        IDList.Remove(runningNumber);//remove the saved number from list
        runningNumber.numberSaved++;//add one to the saved number
        IDList.Add(runningNumber);//add the number back to list
        int temp = (int)runningNumber.numberSaved;//save the running number

        productRoot.Add(
            new XElement("Product",
                new XElement("ID", temp),
                new XElement("Name", product.Name),
                new XElement("Price", product.Price),
                new XElement("Category", product.Category),
                new XElement("InStock", product.InStock)));

        XmlTools.SaveListToXMLElement(productRoot, productPath);        
        productRoot.Save(productPath);
        return temp;

    }

    public Product? GetByID(int _ID)
    {
        XElement productRoot = XmlTools.LoadListFromXMLElement(productPath);
        //List<DO.Product?> productList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(productPath).ToList();
        Product product = (Product)from prod in productRoot.Elements()
                                where Convert.ToInt32(prod.Element("ID").Value) == _ID
                                select prod;     
        if (product == null)
        {
            throw new DO.DoesNotExistException();
        }
        return product;
    }

    public IEnumerable<DO.Product?> GetAll(Func<Product?, bool>? filter)
    {
        // should it be the LoadData()?
        XElement productRoot = XmlTools.LoadListFromXMLElement(productPath);
        List<DO.Product?> productList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(productPath).ToList();
        return (from product in productList
                where filter(product)
                select (DO.Product)product).ToList();
    }

    public void Delete(int _ID)
    {
        XElement productRoot = XmlTools.LoadListFromXMLElement(productPath);

        XElement productElement;
        productElement = (from prod in productRoot.Elements()
                            where Convert.ToInt32(prod.Element("ID").Value ) == _ID
                            select prod).FirstOrDefault();
        if (productElement == null)
        {
            throw new DO.DoesNotExistException();
        }
        productElement.Remove();
        productRoot.Save(productPath);
    }
    

    public void Update(DO.Product product)
    {
        // insert exceptions!!

        XElement productRoot = XmlTools.LoadListFromXMLElement(productPath);
        XElement productElement = (from prod in productRoot.Elements()
                                   where Convert.ToInt32(prod.Element("ID").Value) == product.ID
                                   select prod).FirstOrDefault();
        productElement.Remove();
        productElement.Element("Name").Value = product.Name;
        productElement.Element("Category").Value = product.Category.ToString();
        productElement.Element("Price").Value = product.Price.ToString();
        productRoot.Save(productPath);

    }

    public Product GetByFilter(Func<Product?, bool>? filter)
    {
        List<DO.Product?> productList = GetAll().ToList();

        return (from item in productList
                where filter(item)
                select (DO.Product)item).FirstOrDefault();
    }
}