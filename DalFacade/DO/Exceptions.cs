using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

[Serializable]
public class DuplicateID : Exception
{
    public DuplicateID() { } // default ctor
    public DuplicateID(string message) : base(message) { } // ctor
    public DuplicateID(string message, Exception inner) : base(message, inner) { }

}

[Serializable]
public class DoesNotExist : Exception
{
    public DoesNotExist() { } // default ctor
    public DoesNotExist(string message) : base(message) { } // ctor
    public DoesNotExist(string message, Exception inner) : base(message, inner) { }

}

[Serializable]
public class IDMissing : Exception
{
    public IDMissing() { } // default ctor
    public IDMissing(string message) : base(message) { } // ctor
    public IDMissing(string message, Exception inner) : base(message, inner) { }

}
