using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;                  // Нужно для методов ToArray(), Any(), FirstOrDefault()
using Microsoft.EntityFrameworkCore; // Нужно для Entity Framework Core (DbContext, DbSet, EntityState)
using BookManagerApp.Model;

namespace BookManagerApp.DataAccessLayer
{
    public class EntityBookRepository:IBookRepository
    {
        private readonly BookDbcontext _context;
        public EntityBookRepository()
        {
            _context = new BookDbcontext();
        }
        public void Add(Book item)
        {
            _context.Books.Add(item);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var book = _context.Books.Find(id);

            if(book!=null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
        public Book[] ReadAll()
        {
            return _context.Books.ToArray();
        }
        public Book ReadById(int id)
        {
            return _context.Books.Find(id);
        }
        public void Update(Book item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public bool Exists(int bookId)
        {
            return _context.Books.Any(b => b.Id == bookId);
        }
    }
}
