using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
using DO;
namespace Dal
{
    sealed internal class DalXml : IDal
    {
        #region Singleton
        public static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() { }
        static DalXml() { }
        #endregion
        public IOrder dalOrder { get; } = new Dal.Order(); // suspicious it should be DalOrder
        public IOrderItem dalOrderItem { get; } = new Dal.OrderItem();
        public IProduct dalProduct { get; } = new Dal.Product();
    }

}

