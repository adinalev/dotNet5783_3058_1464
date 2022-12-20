﻿

// NEED TO WORK ON THESE!!!!
//ADD OUT OF STCOK EXCEPTIONS!!


namespace BO;
[Serializable] // NEED THIS??
public class DoesNotExistException : Exception
{
    public DoesNotExistException(Object obj) : base($"The {obj.GetType().Name} does not exist.") { }
}

[Serializable]
public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(Object obj) : base($"The {obj.GetType().Name} already exists. ") { }
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
