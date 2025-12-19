using BookManagerApp.Model;
using System;
using System.Collections.Generic;

namespace BookManagerApp.Logic
{
    /// <summary>
    /// Фасад для предоставления единого интерфейса доступа ко всей функциональности системы управления книгами и дарителями
    /// </summary>
    /// <remarks>
    /// Реализует паттерн Фасад, предоставляя упрощенный API для клиентского кода.
    /// Объединяет CRUD операции и бизнес-функции для книг и дарителей.
    /// </remarks>
    public class LibraryFacade : ILibraryFacade
    {
        private readonly IBookCRUD _bookCRUD;
        private readonly IBookBUSINESS _bookBusiness;
        private readonly IGiverCRUD _giverCRUD;
        private readonly IGiverBUSINESS _giverBusiness;

        /// <summary>
        /// Инициализирует новый экземпляр фасада библиотеки
        /// </summary>
        /// <param name="bookCRUD">Сервис CRUD операций для книг</param>
        /// <param name="bookBusiness">Сервис бизнес-операций для книг</param>
        /// <param name="giverCRUD">Сервис CRUD операций для дарителей</param>
        /// <param name="giverBusiness">Сервис бизнес-операций для дарителей</param>
        public LibraryFacade(IBookCRUD bookCRUD, IBookBUSINESS bookBusiness, IGiverCRUD giverCRUD, IGiverBUSINESS giverBusiness)
        {
            _bookCRUD = bookCRUD;
            _bookBusiness = bookBusiness;
            _giverCRUD = giverCRUD;
            _giverBusiness = giverBusiness;
        }

        // Book CRUD operations

        /// <summary>
        /// Добавляет новую книгу в систему
        /// </summary>
        /// <param name="title">Название книги</param>
        /// <param name="author">Автор книги</param>
        /// <param name="abilities">Способности книги</param>
        /// <param name="year">Год издания</param>
        /// <exception cref="ArgumentException">Выбрасывается при невалидных входных данных</exception>
        public void AddBook(string title, string author, string abilities, int year)
            => _bookCRUD.AddBook(title, author, abilities, year);

        /// <summary>
        /// Удаляет книгу из системы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления</param>
        /// <returns>true если книга удалена, false если книга не найдена</returns>
        public bool DeleteBook(int id) => _bookCRUD.DeleteBook(id);

        /// <summary>
        /// Получает все книги из системы
        /// </summary>
        /// <returns>Массив всех книг</returns>
        public Book[] GetAllBooks() => _bookCRUD.GetAllBooks();

        /// <summary>
        /// Находит книгу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <returns>Найденная книга или null если книга не существует</returns>
        public Book GetBookById(int id) => _bookCRUD.GetBookById(id);

        /// <summary>
        /// Обновляет информацию о книге
        /// </summary>
        /// <param name="id">Идентификатор книги для обновления</param>
        /// <param name="newTitle">Новое название книги (null или пустая строка чтобы не менять)</param>
        /// <param name="newAuthor">Новый автор (null или пустая строка чтобы не менять)</param>
        /// <param name="newAbility">Новая способность (null или пустая строка чтобы не менять)</param>
        /// <param name="newYear">Новый год издания (0 чтобы не менять)</param>
        /// <returns>true если книга обновлена, false если книга не найдена</returns>
        public bool UpdateBook(int id, string newTitle, string newAuthor, string newAbility, int newYear)
            => _bookCRUD.UpdateBook(id, newTitle, newAuthor, newAbility, newYear);

        /// <summary>
        /// Проверяет существование книги по идентификатору
        /// </summary>
        /// <param name="bookId">Идентификатор книги для проверки</param>
        /// <returns>true если книга существует, false если нет</returns>
        public bool BookExists(int bookId) => _bookCRUD.BookExists(bookId);

        // Book business operations

        /// <summary>
        /// Группирует книги по авторам
        /// </summary>
        /// <returns>Словарь где ключ - автор, значение - список его книг</returns>
        public Dictionary<string, List<Book>> GroupBooksByAuthor()
            => _bookBusiness.GroupBooksByAuthor();

        /// <summary>
        /// Находит книги, опубликованные после указанного года
        /// </summary>
        /// <param name="year">Год для сравнения</param>
        /// <returns>Массив книг, изданных после указанного года</returns>
        public Book[] GetBooksPublishedAfterYear(int year)
            => _bookBusiness.GetBooksPublishedAfterYear(year);

        // Giver CRUD operations

        /// <summary>
        /// Добавляет нового дарителя в систему
        /// </summary>
        /// <param name="name">Имя дарителя</param>
        /// <param name="bookId">Идентификатор книги дарителя</param>
        /// <param name="yearOfCreation">Год создания/очки силы</param>
        /// <param name="team">Команда дарителя</param>
        /// <exception cref="ArgumentException">Выбрасывается при невалидных данных или если книга не существует</exception>
        public void AddGiver(string name, int bookId, int yearOfCreation, string team)
            => _giverCRUD.AddGiver(name, bookId, yearOfCreation, team);

        /// <summary>
        /// Удаляет дарителя из системы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор дарителя для удаления</param>
        /// <returns>true если даритель удален, false если даритель не найден</returns>
        public bool DeleteGiver(int id) => _giverCRUD.DeleteGiver(id);

        /// <summary>
        /// Получает всех дарителей из системы
        /// </summary>
        /// <returns>Массив всех дарителей</returns>
        public Giver[] GetAllGivers() => _giverCRUD.GetAllGivers();

        /// <summary>
        /// Обновляет информацию о дарителе
        /// </summary>
        /// <param name="id">Идентификатор дарителя для обновления</param>
        /// <param name="newName">Новое имя (null или пустая строка чтобы не менять)</param>
        /// <param name="newBookId">Новый идентификатор книги (0 чтобы не менять)</param>
        /// <param name="newYearOfCreation">Новые очки силы (0 чтобы не менять)</param>
        /// <param name="newTeam">Новая команда (null или пустая строка чтобы не менять)</param>
        /// <returns>true если даритель обновлен, false если даритель не найден</returns>
        /// <exception cref="ArgumentException">Выбрасывается если новая книга не существует</exception>
        public bool UpdateGiver(int id, string newName, int newBookId, int newYearOfCreation, string newTeam)
            => _giverCRUD.UpdateGiver(id, newName, newBookId, newYearOfCreation, newTeam);

        // Giver business operations

        /// <summary>
        /// Группирует дарителей по командам
        /// </summary>
        /// <returns>Словарь где ключ - команда, значение - список дарителей в команде</returns>
        public Dictionary<string, List<Giver>> GroupGiversByTeams()
            => _giverBusiness.GroupGiversByTeams();

        /// <summary>
        /// Находит дарителей с очками силы больше указанного значения
        /// </summary>
        /// <param name="year">Пороговое значение очков силы</param>
        /// <returns>Массив дарителей с очками силы больше указанного значения</returns>
        public Giver[] GetGiversWithBooksAfterYear(int year)
           => _giverBusiness.GetGiversWithBooksAfterYear(year);

        /// <summary>
        /// Получает информацию о книге дарителя
        /// </summary>
        /// <param name="giverId">Идентификатор дарителя</param>
        /// <returns>
        /// Информация о книге в формате "Название (способность)" 
        /// или сообщение об ошибке если даритель или книга не найдены
        /// </returns>
        public string GetGiverBookInfo(int giverId)
            => _giverBusiness.GetGiverBookInfo(giverId);
    }
}