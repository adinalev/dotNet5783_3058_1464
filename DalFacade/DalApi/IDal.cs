﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
using DO;

public interface IDal
{
    IProduct Product { get; }
    IOrder Order { get; }
    IOrderItem OrderItem { get; }
}
