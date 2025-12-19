using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Logic
{
    public interface ILibraryFacade : IBookCRUD, IBookBUSINESS, IGiverCRUD, IGiverBUSINESS
    {

    }
}
