using BookManagerApp.DataAccessLayer;
using BookManagerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Logic
{
    public class BookBusinesService
    {
        private IBookRepository _bookRepository;

        public BookBusinesService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public Dictionary<string, List<Book>> GroupBooksByAuthor()
        {
            var books = _bookRepository.ReadAll();
            return books.GroupBy(b => b.Author)
                       .ToDictionary(g => g.Key, g => g.ToList());
        }

        public Book[] GetBooksPublishedAfterYear(int year)
        {
            var books = _bookRepository.ReadAll();
            return books.Where(b => b.Year > year).ToArray();
        }
    }
}
