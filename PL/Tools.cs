using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

static class Tools
{
    public static PO.Product CastBoProductToPo(BO.Product p)
    {
        PO.Product poProduct = new PO.Product();
        poProduct.Name = p.Name;
        poProduct.Price = p.Price;
        poProduct.ID = p.ID;
        poProduct.Price = p.Price;
        poProduct.InStock = p.InStock;
        poProduct.Category = (BO.Enums.ProductCategory)p.Category;
        return poProduct;
    }
    public static BO.Product CastPoProductToBo(PO.Product p)
    {
        BO.Product prod = new();
        prod.Name = p.Name;
        prod.Price = p.Price;
        prod.ID = p.ID;
        prod.Price = p.Price;
        prod.InStock = p.InStock;
        prod.Category = p.Category;
        return prod;
    }
    public static BO.Order CastPoOrderToBo(PO.Order p)
    {
        BO.Order ord = new();
        ord.Address = p.CustomerAddress;
        ord.CustomerName = p.CustomerName;
        ord.Email = p.CustomerEmail;
        ord.DeliveryDate = p.DeliveryDate;
        ord.OrderDate = p.OrderDate;
        ord.ShippingDate = p.ShipDate;
        ord.ID = p.ID;
        ord.Status = p.Status;
        ord.Items = p.OrderItems;
        ord.TotalPrice = p.TotalPrice;
        return ord;
    }
    public static PO.Order CastBoOrderToPo(BO.Order p)
    {
        PO.Order ord = new();
        ord.CustomerAddress = p.Address;
        ord.CustomerName = p.CustomerName;
        ord.CustomerEmail = p.Email;
        ord.DeliveryDate = p.DeliveryDate;
        ord.OrderDate = p.OrderDate;
        ord.ShipDate = p.ShippingDate;
        ord.ID = p.ID;
        ord.Status = (BO.Enums.OrderStatus)p.Status;
        ord.OrderItems = p.Items;
        ord.TotalPrice = p.TotalPrice;
        return ord;
    }
    public static PO.OrderForList CastBoOrderFLToPo(BO.OrderForList p)
    {
        PO.OrderForList ord = new();
        ord.Status = (BO.Enums.OrderStatus)p.Status;
        ord.ID = p.ID;
        ord.TotalPrice = p.TotalPrice;
        ord.Amount = p.AmountOfItems;
        ord.Name = p.CustomerName;
        return ord;
    }
    public static BO.OrderForList CastPoOrderFLToBo(PO.OrderForList p)
    {
        BO.OrderForList ord = new();
        ord.Status = p.Status;
        ord.ID = p.ID;
        ord.TotalPrice = p.TotalPrice;
        ord.AmountOfItems = p.Amount;
        ord.CustomerName = p.Name;
        return ord;
    }



    public static PO.ProductForList CastBoPFLToPo(BO.ProductForList p)
    {
        PO.ProductForList ord = new();
        ord.ID = p.ID;
        ord.ProductName = p.Name;
        ord.Price = p.Price;
        ord.Category = (BO.Enums.ProductCategory)p.Category;
        return ord;
    }

    public static BO.ProductForList CastPoPFLToBo(PO.ProductForList p)
    {
        BO.ProductForList ord = new();
        ord.ID = p.ID;
        ord.Name = p.ProductName;
        ord.Price = p.Price;
        ord.Category = p.Category;
        return ord;
    }

    public static BO.ProductItem CastPoPIToBo(PO.ProductItem p)
    {
        BO.ProductItem prod = new();
        prod.Amount = p.Amount;
        prod.Name = p.ProductName;
        prod.Price = p.Price;
        prod.ID = p.ID;
        prod.InStock = p.InStock;
        prod.Category = p.Category;
        prod.Price = p.Price;
        return prod;
    }
    public static PO.ProductItem CastBoPIToPo(BO.ProductItem p)
    {
        PO.ProductItem prod = new();
        prod.Amount = p.Amount;
        prod.ProductName = p.Name;
        prod.Price = p.Price;
        prod.ID = p.ID;
        prod.InStock = p.InStock;
        prod.Category = (BO.Enums.ProductCategory)p.Category;
        prod.Price = p.Price;
        return prod;
    }
    public static PO.OrderItem CastBoOIToPo(BO.OrderItem p)
    {
        PO.OrderItem oi = new();
        oi.ProductPrice = p.ProductPrice;
        oi.ID = p.ID;
        oi.Amount = p.Quantity;
        oi.Price = p.Price;
        oi.ProductID = p.ProductID;
        oi.ProductPrice = p.ProductPrice;
        oi.ProductName = p.ProductName;
        return oi;
    }
    public static BO.OrderItem CastPoOIToBo(PO.OrderItem p)
    {
        BO.OrderItem oi = new();
        oi.ProductPrice = p.ProductPrice;
        oi.ID = p.ID;
        oi.Quantity = p.Amount;
        oi.Price = p.Price;
        oi.ProductID = p.ProductID;
        oi.ProductPrice = p.ProductPrice;
        oi.ProductName = p.ProductName;
        return oi;
    }
    public static PO.OrderTracking CastBoOTToPo(BO.OrderTracking p)
    {
        PO.OrderTracking o = new()
        {
            //Tracking = p.Tracking,
            ID = p.ID,
            Status = (BO.Enums.OrderStatus)p.Status
        };
        return o;
    }
    public static BO.OrderTracking CastPoOTToBo(PO.OrderTracking p)
    {
        BO.OrderTracking o = new()
        {
            //Tracking = p.Tracking,
            ID = p.ID,
            Status = p.Status
        };
        return o;
    }
    public static IEnumerable<PO.ProductItem> CastBoOPILToPo(IEnumerable<BO.ProductItem> prods)
    {
        IEnumerable<PO.ProductItem> p = from item in prods
                                        select new PO.ProductItem()
                                        {
                                            ID = item.ID,
                                            ProductName = item.Name,
                                            Price = item.Price,
                                            Amount = item.Amount,
                                            Category = (BO.Enums.ProductCategory)item.Category,
                                            InStock = item.InStock
                                        };
        return p;
    }
    public static PO.Cart CastBoCToPo(BO.Cart c)
    {
        PO.Cart cart = new()
        {
            OrderItems = c.Items,
            CustomerAddress = c.CustomerAddress,
            CustomerEmail = c.CustomerEmail,
            CustomerName = c.CustomerName,
            Price = c.TotalPrice
        };
        return cart;
    }

    public static BO.Cart CastPoCToBo(PO.Cart c)
    {
        BO.Cart cart = new()
        {
            Items = c.OrderItems,
            CustomerAddress = c.CustomerAddress,
            CustomerEmail = c.CustomerEmail,
            CustomerName = c.CustomerName,
            TotalPrice = c.Price
        };
        return cart;
    }

    /// <summary>
    /// convert from ienumerable to an observable collection
    /// </summary>
    /// <param name="listTOConvert">IEnumerable to convert</param>
    public static ObservableCollection<PO.ProductItem> IEnumerableToObservable(IEnumerable<BO.ProductItem?> listTOConvert)
    {
        ObservableCollection<PO.ProductItem> newList = new();
        foreach (var item in listTOConvert)
            newList.Add(CastBoPIToPo(item!));
        return newList;
    }
    public static ObservableCollection<PO.OrderItem> IEnumerableToObservable(IEnumerable<BO.OrderItem?> listTOConvert)
    {
        ObservableCollection<PO.OrderItem> newList = new();
        foreach (var item in listTOConvert)
            newList.Add(CastBoOIToPo(item!));
        return newList;
    }
    public static ObservableCollection<PO.ProductForList> IEnumerableToObservable(IEnumerable<BO.ProductForList?> listTOConvert)
    {
        ObservableCollection<PO.ProductForList> newList = new();
        foreach (var item in listTOConvert)
            newList.Add(CastBoPFLToPo(item!));
        return newList;
    }
    public static ObservableCollection<PO.OrderForList> IEnumerableToObservable(IEnumerable<BO.OrderForList?> listTOConvert)
    {
        ObservableCollection<PO.OrderForList> newList = new();
        foreach (var item in listTOConvert)
            newList.Add(CastBoOrderFLToPo(item!));
        return newList;
    }


}
