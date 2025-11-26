using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerApp.Model;

namespace BookManagerApp.DataAccessLayer
{
    public interface IBookRepository: IReadRepository<Book>, IWriteRepository<Book>
    {

        bool Exists(int id);
    }
}
