using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;
public struct Enums
{
    public enum Category { MEDICINE, COSMETICS, HYGIENE, FOOD, OPTICS, BABIES };
    public enum Action { EXIT, ADD, GET , GETLIST, UPDATE, DELETE };
    public enum Type {  EXIT, PRODUCT, ORDER, ORDERITEM };

}