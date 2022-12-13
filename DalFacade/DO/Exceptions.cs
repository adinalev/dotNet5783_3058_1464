using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

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
