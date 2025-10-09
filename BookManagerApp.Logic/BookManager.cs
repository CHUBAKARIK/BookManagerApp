using BookManagerApp.Model;
namespace BookManagerApp.Logic

{
    public class BookManager
    {
        // Имитация базы данных (хранилище в оперативной памяти)
        private List<Book> _books = new List<Book>();
        private int _nextId = 1; // Счетчик для генерации ID

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="year"></param>
        /// <exception cref="ArgumentException"></exception>
        // 1. Создание сущности (Create)
        public void AddBook(string title, string author, int year)
        {
            // Проверка входных данных (простая бизнес-логика)
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название книги не может быть пустым.");
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Автор не может быть пустым.");

            var book = new Book(_nextId++, title, author, year);
            _books.Add(book);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // 2. Удаление сущности (Delete)
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
        /// 
        /// </summary>
        /// <returns></returns>
        // 3. Чтение сущности (Read) - Получить все книги
        public List<Book> GetAllBooks()
        {
            // Возвращаем копию списка, чтобы исходный нельзя было изменить извне
            return new List<Book>(_books);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // 3. Чтение сущности (Read) - Получить книгу по ID
        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newTitle"></param>
        /// <param name="newAuthor"></param>
        /// <param name="newYear"></param>
        /// <returns></returns>
        // 4. Изменение сущности (Update)
        public bool UpdateBook(int id, string newTitle, string newAuthor, int newYear)
        {
            var bookToUpdate = _books.FirstOrDefault(b => b.Id == id);
            if (bookToUpdate != null)
            {
                // Простая бизнес-логика при обновлении
                if (!string.IsNullOrWhiteSpace(newTitle))
                    bookToUpdate.Title = newTitle;
                if (!string.IsNullOrWhiteSpace(newAuthor))
                    bookToUpdate.Author = newAuthor;
                if (newYear > 0) // Простая проверка года
                    bookToUpdate.Year = newYear;

                return true;
            }
            return false; // Книга с таким ID не найдена
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // 5. Бизнес-функция 1: Группировка книг по авторам
        public Dictionary<string, List<Book>> GroupBooksByAuthor()
        {
            return _books.GroupBy(b => b.Author)
                         .ToDictionary(g => g.Key, g => g.ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        // 5. Бизнес-функция 2: Поиск книг, вышедших после указанного года
        public List<Book> GetBooksPublishedAfterYear(int year)
        {
            return _books.Where(b => b.Year > year).ToList();
        }
    }
}
