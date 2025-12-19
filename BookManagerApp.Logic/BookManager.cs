

using System;                    
using BookManagerApp.Model;      
using BookManagerApp.DataAccessLayer;  


namespace BookManagerApp.Logic
{
    
    public class BookManager : IBookCRUD
    {
        /// <summary>
        /// ЗАМЕНЯЕМ старый List на репозиторий,private - поле доступно только внутри этого класса
        // IBookRepository - тип интерфейса (а не конкретной реализации)
        // _bookRepository - имя поля (хранит ссылку на репозиторий книг)
        /// </summary>

        private readonly IBookRepository _bookrepository;

        /// <summary>
        /// Конструктор класса - вызывается при создании объекта BookManager
        /// </summary>

        public BookManager(IBookRepository bookRepository)
        {
            _bookrepository = bookRepository;
        }

        /// <summary>
        /// 1. Создание сущности (Create) - добавление новой книги
        /// </summary>
        /// <param name="title">Название книги</param>
        /// <param name="author">Автор книги</param>
        /// <param name="abilitiesofthebook">Способность книги</param>
        /// <param name="year">Год издания</param>
        /// <exception cref="ArgumentException">Исключение при ошибках валидации</exception>
        public void AddBook(string title, string author, string abilitiesofthebook, int year)
        {
            
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название книги не может быть пустым.");

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Автор не может быть пустым.");

            if (string.IsNullOrWhiteSpace(abilitiesofthebook))
                throw new ArgumentException("У книги обязательно должна быть способность");

            
            var book = new Book
            {
                Title = title,
                Author = author,
                AbilitiesOfTheBook = abilitiesofthebook,
                Year = year
            };


            _bookrepository.Add(book);
        }

        /// <summary>
        /// 2. Удаление сущности (Delete) - удаление книги по ID
        /// </summary>
        /// <param name="id">ID книги для удаления</param>
        /// <returns>true если книга удалена, false если не найдена</returns>
        public bool DeleteBook(int id)
        {
            
            if (id <= 0)
            {
                throw new ArgumentException("ID книги должен быть положительным числом");
            }


            _bookrepository.Delete(id);

            
            return true;
        }

        /// <summary>
        /// 3. Чтение сущности (Read) - Получить все книги
        /// </summary>
        /// <returns>Список всех книг из базы данных</returns>
        public Book[] GetAllBooks()
        {
            
           
            return _bookrepository.ReadAll();
        }

        /// <summary>
        /// 3. Чтение сущности (Read) - Получить книгу по ID
        /// </summary>
        /// <param name="id">ID книги для поиска</param>
        /// <returns>Найденная книга или null</returns>
        public Book GetBookById(int id)
        {
            // Проверяем валидность ID
            if (id <= 0)
            {
                throw new ArgumentException("ID книги должен быть положительным числом");
            }

           
            return _bookrepository.ReadById(id);
        }

        /// <summary>
        /// 4. Изменение сущности (Update) - обновление информации о книге
        /// </summary>
        /// <param name="id">ID книги для обновления</param>
        /// <param name="newTitle">Новое название (если не null)</param>
        /// <param name="newAuthor">Новый автор (если не null)</param>
        /// <param name="NewAbility">Новая способность (если не null)</param>
        /// <param name="newYear">Новый год (если больше 0)</param>
        /// <returns>true если книга обновлена, false если не найдена</returns>
        public bool UpdateBook(int id, string newTitle, string newAuthor, string NewAbility, int newYear)
        {
            // Проверяем валидность ID
            if (id <= 0)
            {
                throw new ArgumentException("ID книги должен быть положительным числом");
            }

            
            var bookToUpdate = _bookrepository.ReadById(id);

          
            if (bookToUpdate != null)
            {
                
                if (!string.IsNullOrWhiteSpace(newTitle))
                    bookToUpdate.Title = newTitle;

                if (!string.IsNullOrWhiteSpace(newAuthor))
                    bookToUpdate.Author = newAuthor;

                if (!string.IsNullOrWhiteSpace(NewAbility))
                    bookToUpdate.AbilitiesOfTheBook = NewAbility;

                if (newYear > 0)
                    bookToUpdate.Year = newYear;


                _bookrepository.Update(bookToUpdate);
                return true;
            }
            return false; // Книга не найдена
        }

        /// <summary>
        /// 5. Бизнес-функция 1: Группировка книг по авторам
        /// </summary>
        /// <returns>Словарь где ключ - автор, значение - список его книг</returns>
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<Book>> GroupBooksByAuthor()
        {
            // Получаем все книги из базы данных через репозиторий
            var books = _bookrepository.ReadAll();

           
            return books.GroupBy(b => b.Author)
                         .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// 5. Бизнес-функция 2: Поиск книг, вышедших после указанного года
        /// </summary>
        /// <param name="year">Год для сравнения</param>
        /// <returns>Список книг выпущенных после указанного года</returns>
        public Book[] GetBooksPublishedAfterYear(int year)
        {
           
            var books = _bookrepository.ReadAll();

           
            return books.Where(b => b.Year > year).ToArray();
        }

        /// <summary>
        /// Проверка существования книги по ID
        /// </summary>
        /// <param name="bookId">ID книги для проверки</param>
        /// <returns>true если книга существует, false если нет</returns>
        public bool BookExists(int bookId)
        {
           
            if (bookId <= 0)
            {
                return false;
            }


            return _bookrepository.Exists(bookId);
        }
    }
}