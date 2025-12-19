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
        /// <summary>
        /// Инициализирует новый экземпляр репозитория для работы с книгами через Entity Framework
        /// </summary>
        public EntityBookRepository()
        {
            _context = new BookDbcontext();
        }
        /// <summary>
        /// Добавляет новую книгу в базу данных
        /// </summary>
        public void Add(Book item)
        {
            _context.Books.Add(item);
            _context.SaveChanges();
        }
        /// <summary>
        /// Удаляет книгу из базы данных по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления</param>
        /// <remarks>
        /// Если книга с указанным ID не найдена, метод завершается без ошибок
        /// </remarks>
        public void Delete(int id)
        {
            var book = _context.Books.Find(id);

            if(book!=null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// Получает все книги из базы данных
        /// </summary>
        /// <returns>Массив всех книг, отсортированный по идентификатору</returns>
        /// <remarks>
        /// Возвращает пустой массив, если в базе данных нет книг
        /// </remarks>
        public Book[] ReadAll()
        {
            return _context.Books.ToArray();
        }
        /// <summary>
        /// Находит книгу в базе данных по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги для поиска</param>
        /// <returns>Найденная книга или null, если книга не существует</returns>
        public Book ReadById(int id)
        {
            return _context.Books.Find(id);
        }
        /// <summary>
        /// Обновляет информацию о книге в базе данных
        /// </summary>
        /// <param name="item">Объект книги с обновленными данными</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если item равен null</exception>
        /// <exception cref="DbUpdateException">Выбрасывается при ошибке сохранения изменений</exception>
        /// <remarks>
        /// Книга должна существовать в базе данных, иначе будет создана новая запись
        /// </remarks>
        public void Update(Book item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        /// <summary>
        /// Проверяет существование книги с указанным идентификатором в базе данных
        /// </summary>
        /// <param name="bookId">Идентификатор книги для проверки</param>
        /// <returns>true если книга существует, false если нет</returns>
        public bool Exists(int bookId)
        {
            return _context.Books.Any(b => b.Id == bookId);
        }
    }
}
