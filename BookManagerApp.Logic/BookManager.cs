using BookManagerApp.Model;
namespace BookManagerApp.Logic

{
    public class BookManager
    {
        // Имитация базы данных (хранилище в оперативной памяти)
        private List<Book> _books = new List<Book>();
        private int _nextId = 1; // Счетчик для генерации ID

        /// <summary>
        ///  1. Создание сущности (Create)
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="year"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddBook(string title, string author, string abilitiesofthebook, int year)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название книги не может быть пустым.");           

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Автор не может быть пустым.");
            if (string.IsNullOrWhiteSpace(abilitiesofthebook))
                throw new ArgumentException("У книги обязательно должна быть способность");

            var book = new Book(_nextId++, title, author, abilitiesofthebook, year);
            _books.Add(book);
        }
        /// <summary>
        /// 2. Удаление сущности (Delete)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteBook(int id)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                return true;
            }
            return false; // Книга с таким ID не найдена
        }
        /// <summary>
        /// 3. Чтение сущности (Read) - Получить все книги
        /// </summary>
        /// <returns></returns>
        public List<Book> GetAllBooks()
        {
            return new List<Book>(_books);
        }
        /// <summary>
        /// 3. Чтение сущности (Read) - Получить книгу по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }
        /// <summary>
        /// 4. Изменение сущности (Update)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newTitle"></param>
        /// <param name="newAuthor"></param>
        /// <param name="newYear"></param>
        /// <returns></returns>
        public bool UpdateBook(int id, string newTitle, string newAuthor,string NewAbility, int newYear)
        {
            var bookToUpdate = _books.FirstOrDefault(b => b.Id == id);
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

                return true;
            }
            return false; 
        }
        /// <summary>
        /// 5. Бизнес-функция 1: Группировка книг по авторам
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<Book>> GroupBooksByAuthor()
        {
            return _books.GroupBy(b => b.Author)
                         .ToDictionary(g => g.Key, g => g.ToList());
        }
        /// <summary>
        /// 5. Бизнес-функция 2: Поиск книг, вышедших после указанного года
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<Book> GetBooksPublishedAfterYear(int year)
        {
            return _books.Where(b => b.Year > year).ToList();
        }
        public bool BookExists(int bookId)
        {
            return _books.Any(b => b.Id == bookId);
        }

        



    }
}
