using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
public interface ICrud<T> // SHOULD WE ADD WHERE T : STRUCT
{
    // ARE WE SUPPOSED TO BE <T>
    int Add(T entity);
    void Delete(int ID);
    void Update(T entity);
    T GetByID(int ID);
    IEnumerable<T> GetAll(); // method that returns a list of objects for an entity
}
