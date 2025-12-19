using BookManagerApp.DataAccessLayer;
using BookManagerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagerApp.Logic
{
    public class BookBusinesService : IBookBUSINESS
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса бизнес-логики для работы с книгами
        /// </summary>
        /// <param name="bookRepository">Репозиторий для доступа к данным книг</param>
        public BookBusinesService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Группирует книги по авторам
        /// </summary>
        /// <returns>Словарь, где ключ - имя автора, значение - список его книг</returns>
        /// <remarks>
        /// Возвращает пустой словарь, если в системе нет книг.
        /// Книги без автора будут сгруппированы под ключом null или пустой строкой.
        /// </remarks>
        public Dictionary<string, List<Book>> GroupBooksByAuthor()
        {
            var books = _bookRepository.ReadAll();
            return books.GroupBy(b => b.Author)
                       .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Находит книги, опубликованные после указанного года
        /// </summary>
        /// <param name="year">Год для сравнения. Будут возвращены книги с годом издания больше указанного</param>
        /// <returns>Массив книг, опубликованных после указанного года</returns>
        /// <remarks>
        /// Возвращает пустой массив, если нет книг, удовлетворяющих условию.
        /// Сравнение строгое: год издания должен быть больше указанного значения.
        /// </remarks>
        public Book[] GetBooksPublishedAfterYear(int year)
        {
            var books = _bookRepository.ReadAll();
            return books.Where(b => b.Year > year).ToArray();
        }
    }
}