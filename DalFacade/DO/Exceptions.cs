using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

[Serializable] // NEED THIS??
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
