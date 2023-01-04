using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
public interface ICrud<T> where T : struct // do we need this??
{
    int Add(T entity);
    void Delete(int ID);
    void Update(T entity);
    T? GetByID(int ID);
    IEnumerable<T?> GetAll(Func<T?, bool>? filter = null); // method that returns a list of objects for an entity
    T GetByFilter(Func<T?, bool>? filter);

}
