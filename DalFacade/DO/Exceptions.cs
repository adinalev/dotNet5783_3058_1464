using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

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
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

