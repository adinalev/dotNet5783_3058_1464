using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class OrderTracking
{
    public int ID { get; set; }
    // status of type orderstatus!!!
    public override string ToString() => $@"
            ID = {ID}
            Status: {} // add the status here!!!
        ";
}
