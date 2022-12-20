//using BlApi;
//using DalApi;
//using Dal;
//namespace BlImplementation;

///// <summary>
///// Implementation of class OrderItem that inherits
///// from Iorder interface under BlAPi. 
///// </summary>
//internal class OrderItem : BlApi.IOrderItem 
//{
//    private IDal Dal = new DalList(); //private field Dal

//    //Implementing methods declared in the interface:

//    public DO.OrderItem AddOrderItem(BO.OrderItem item) //Add Method, this time with logic checks
//    {
//        if (item.ProductID < 100000 || item.Price <= 0 || item.Quantity <= 0)
//        {
//            throw new BO.InvalidInputException(); //if any input is empty or out of bound, it is invalid
//        }
//    }
//    //if input is valid, set the fields of newItem and return it
//    // ARE WE SUPPOSED TO CHECK FOR THE 4 HERE?
//    DO.OrderItem newItem = new DO.OrderItem();
//        newItem.ProductID = item.ProductID;
//        newItem.Price = item.Price;
//        newItem.Quantity = item.Quantity;
//        newItem.ID = Dal.dalOrderItem.Add(newItem);
//        return newItem;
//    }

//    public void DeleteOrderItem(int _id) //Delete Method, this time with logic checks
//{
//        DO.OrderItem delItem = new DO.OrderItem();
//        int delID = -1; //set a check to see if the _id will be found
//        foreach (DO.OrderItem item in Dal.dalOrderItem.GetAll())
//        { //loop through orders to check IDs
//            if (_id == item.ID)
//                delID = item.ID; //if found, set the found ID to delID
//        }
//        if (delID == -1) //that means it was not found in the foreach loop
//        {
//            throw new BO.DoesNotExistException(delItem); //throw exception
//        }
//        //if ID was found, then delete the order with that ID:
//        Dal.dalOrderItem.Delete(delID);
//    }

//    public DO.OrderItem UpdateOrderItem(BO.OrderItem item) //Update Method, this time with logic checks
//    {
//        DO.OrderItem newItem = new DO.OrderItem();
//        int _id = -1;  //set a check to see if the id will be found
//        foreach (DO.OrderItem it in Dal.dalOrderItem.GetAll())
//        { //loop through orders to check IDs
//            if (it.ID == item.ID)
//                _id = it.ID; //if found, set the found ID to _id 
//        }
//        if (_id == -1) //id not found
//        {
//            throw new BO.DoesNotExistException(newItem); //throw exception
//        }
//        //otherwise move on to next data check:
//        if (item.ProductID < 100000 || item.Price <= 0 || item.Quantity <= 0)
//        {
//            throw new BO.InvalidInputException(); //if data is invalid throw exception
//        }
//        //else update the orderItem
//        newItem.ID = item.ID;
//        newItem.ProductID = item.ProductID;
//        newItem.Price = item.Price;
//        newItem.Quantity = item.Quantity;      
//        Dal.dalOrderItem.Update(newItem);
//        return newItem; // need this??
//    }

//    public DO.OrderItem GetOrderItem(int ID) //GetOrderItem method with logic checks
//    {
//        int _id = -1; //set a check to see if the id will be found
//        DO.OrderItem item = new DO.OrderItem();
//        foreach (DO.OrderItem it in Dal.dalOrderItem.GetAll())
//        { //loop to check if the ID exists
//            if (it.ID == ID)
//            {
//                item = it;
//                _id = it.ID; //set the ID check to the found ID
//            }
//        }
//        if (_id == -1) //desired ID not found
//        {
//            throw new BO.DoesNotExistException(item); //so throw an exception
//        }
//        //otherwise return the orderItem
//        return item;
//    }

//    public IEnumerable<DO.OrderItem> GetOrderItemList() //method to get the list of orderItems
//{
//        return Dal.dalOrderItem.GetAll();
//    }
//}
