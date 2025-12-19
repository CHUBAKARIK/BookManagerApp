using BookManagerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Logic
{
    public interface IBookBUSINESS 
    {
        Book[] GetBooksPublishedAfterYear(int year);
        Dictionary<string, List<Book>> GroupBooksByAuthor();
    }
}
