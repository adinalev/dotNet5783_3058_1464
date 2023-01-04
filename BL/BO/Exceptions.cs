namespace BO;

[Serializable] 
public class DoesNotExistException : Exception
{
    public DoesNotExistException() : base($"The item does not exist.") { }
}

[Serializable]
public class AlreadyExistsException : Exception
{
    public AlreadyExistsException() : base($"The item already exists. ") { }
}

[Serializable]
public class TooManyProductsException : Exception
{
    public TooManyProductsException() : base("Cannot have more than 4 types of products per order! ") { }
}

[Serializable]
public class InvalidInputException : Exception
{
    public InvalidInputException() : base("Invalid input detected. ") { }
}

public class OutOfStockException : Exception
{
    public OutOfStockException() : base("This product is currently out of stock.") { }
}

public class NotEnoughInStockException : Exception
{
    public NotEnoughInStockException() : base("This product does not have that amount in stock.") { }
}

public class NotInCartException : Exception
{
    public NotInCartException() : base("This product is not currently in the cart.") { }
}

public class AlreadyShippedException : Exception
{
    public AlreadyShippedException() : base("The shipping date cannot be changed because the order has already been shipped. \n") { }
}

public class OrderNotPlacedYetException : Exception
{
    public OrderNotPlacedYetException() : base("The order has not yet been placed. \n") { }
}

public class ShipDateOutOfRangeException : Exception
{
    public ShipDateOutOfRangeException() : base("The shipping date cannot be after the delivery date. \n") { }
}

public class AlreadyDeliveredException : Exception
{
    public AlreadyDeliveredException() : base("The delivery date cannot be changed because the order has already been delivered. \n") { }
}

public class DeliveryDateOutOfRangeException : Exception
{
    public DeliveryDateOutOfRangeException() : base("The delivery date cannot be before the shipping date. \n") { }
}

public class NoShipDateException : Exception
{
    public NoShipDateException() : base("Cannot change the delivery date without setting a shipping date first. \n") { }
}
