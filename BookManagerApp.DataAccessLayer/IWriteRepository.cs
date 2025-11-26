using BookManagerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.DataAccessLayer
{
    public interface IWriteRepository<T> where T : IDomainObject
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
