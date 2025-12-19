using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerApp.DataAccessLayer;
using BookManagerApp.Model;

namespace BookManagerApp.Logic
{
    public interface IBookCRUD
    {
        void AddBook(string title, string author, string abilitiesofthebook, int year);
        bool DeleteBook(int id);

        Book[] GetAllBooks();
        Book GetBookById(int id);
        bool UpdateBook(int id, string newTitle, string newAuthor, string NewAbility, int newYear);
        bool BookExists(int bookId);
    }
}
