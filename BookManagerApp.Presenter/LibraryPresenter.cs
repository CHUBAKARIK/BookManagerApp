using BookManagerApp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerApp.Shared;
using BookManagerApp.Shared.models;


namespace BookManagerApp.Presenter
{
    public  class LibraryPresenter
    {
        private readonly ILibraryView _view;
        private readonly ILibraryFacade _library;

        /// <summary>
        /// Инициализирует новый экземпляр Presenter'а
        /// </summary>
        public LibraryPresenter(ILibraryView view, ILibraryFacade library)
        {
            _view = view;
            _library = library;
            SubscribeToEvents();
        }

        /// <summary>
        /// Подписывается на все события от View
        /// </summary>
        private void SubscribeToEvents()
        {
            _view.LoadBooks += OnLoadBooks;
            _view.AddBook += OnAddBook;
            _view.UpdateBook += OnUpdateBook;
            _view.DeleteBook += OnDeleteBook;
            _view.GroupBooksByAuthor += OnGroupBooksByAuthor;
            _view.BooksAfterYear += OnBooksAfterYear;

            _view.LoadGivers += OnLoadGivers;
            _view.AddGiver += OnAddGiver;
            _view.UpdateGiver += OnUpdateGiver;
            _view.DeleteGiver += OnDeleteGiver;
            _view.GroupGiversByTeam += OnGroupGiversByTeam;
            _view.GiversWithPower += OnGiversWithPower;
        }

        /// <summary>
        /// Обработчик события загрузки книг
        /// </summary>
        private void OnLoadBooks(object sender, EventArgs e)
        {
            try
            {
                var books = _library.GetAllBooks();
                var bookDtos = books.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    AbilitiesOfTheBook = b.AbilitiesOfTheBook,
                    Year = b.Year
                }).ToList();

                _view.ShowBooks(bookDtos);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка загрузки книг: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события добавления книги
        /// </summary>
        private void OnAddBook(object sender, EventArgs e)
        {
            try
            {
                string title = _view.GetBookTitle();
                string author = _view.GetBookAuthor();
                string ability = _view.GetBookAbility();
                string yearStr = _view.GetBookYear();

                if (!int.TryParse(yearStr, out int year))
                    throw new ArgumentException("Неверный формат года");

                _library.AddBook(title, author, ability, year);
                _view.ShowMessage("Книга добавлена!");
                _view.ClearForm();
                OnLoadBooks(sender, e);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события обновления книги
        /// </summary>
        private void OnUpdateBook(object sender, EventArgs e)
        {
            try
            {
                int bookId = _view.GetSelectedBookId();
                if (bookId <= 0)
                    throw new ArgumentException("Выберите книгу для обновления");

                string title = _view.GetBookTitle();
                string author = _view.GetBookAuthor();
                string ability = _view.GetBookAbility();
                string yearStr = _view.GetBookYear();

                int year = 0;
                if (!string.IsNullOrEmpty(yearStr))
                    int.TryParse(yearStr, out year);

                if (_library.UpdateBook(bookId, title, author, ability, year))
                {
                    _view.ShowMessage("Книга обновлена!");
                    OnLoadBooks(sender, e);
                }
                else
                {
                    _view.ShowMessage("Книга не найдена!");
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события удаления книги
        /// </summary>
        private void OnDeleteBook(object sender, EventArgs e)
        {
            try
            {
                int bookId = _view.GetSelectedBookId();
                if (bookId <= 0)
                    throw new ArgumentException("Выберите книгу для удаления");

                if (_library.DeleteBook(bookId))
                {
                    _view.ShowMessage("Книга удалена!");
                    OnLoadBooks(sender, e);
                }
                else
                {
                    _view.ShowMessage("Книга не найдена!");
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события группировки книг по авторам
        /// </summary>
        private void OnGroupBooksByAuthor(object sender, EventArgs e)
        {
            try
            {
                var groups = _library.GroupBooksByAuthor();
                string message = "Книги по авторам:\n\n";

                foreach (var group in groups)
                {
                    message += $"Автор: {group.Key}:\n";
                    foreach (var book in group.Value)
                    {
                        message += $"  - {book.Title} ({book.Year}г.)\n";
                    }
                    message += "\n";
                }

                _view.ShowMessage(message);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события поиска книг после указанного года
        /// </summary>
        private void OnBooksAfterYear(object sender, EventArgs e)
        {
            try
            {
                int year = DateTime.Now.Year - 5;
                var books = _library.GetBooksPublishedAfterYear(year);

                var bookDtos = books.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    AbilitiesOfTheBook = b.AbilitiesOfTheBook,
                    Year = b.Year
                }).ToList();

                if (bookDtos.Any())
                {
                    _view.ShowBooks(bookDtos);
                    _view.ShowMessage($"Показаны книги после {year} года");
                }
                else
                {
                    _view.ShowMessage($"Книг после {year} года не найдено");
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }
        public BookDto GetBookInfo(int bookId)
        {
            try
            {
                var book = _library.GetBookById(bookId);
                if (book != null)
                {
                    return new BookDto
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        AbilitiesOfTheBook = book.AbilitiesOfTheBook,
                        Year = book.Year
                    };
                }
            }
            catch (Exception)
            {
                // Игнорируем ошибку
            }
            return null;
        }
        /// <summary>
        /// Обработчик события загрузки дарителей
        /// </summary>
        private void OnLoadGivers(object sender, EventArgs e)
        {
            try
            {
                var givers = _library.GetAllGivers();
                var giverDtos = givers.Select(g => new GiverDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    BookId = g.BookId,
                    YearOfCreation = g.YearOfCreation,
                    Team = g.Team
                }).ToList();

                _view.ShowGivers(giverDtos);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка загрузки дарителей: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события добавления дарителя
        /// </summary>
        private void OnAddGiver(object sender, EventArgs e)
        {
            try
            {
                string name = _view.GetGiverName();
                string bookIdStr = _view.GetGiverBookId();
                string powerStr = _view.GetGiverPower();
                string team = _view.GetGiverTeam();

                if (!int.TryParse(bookIdStr, out int bookId))
                    throw new ArgumentException("Неверный ID книги");

                if (!int.TryParse(powerStr, out int power))
                    throw new ArgumentException("Неверные очки силы");

                _library.AddGiver(name, bookId, power, team);
                _view.ShowMessage("Даритель добавлен!");
                _view.ClearForm();
                OnLoadGivers(sender, e);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события обновления дарителя
        /// </summary>
        private void OnUpdateGiver(object sender, EventArgs e)
        {
            try
            {
                int giverId = _view.GetSelectedGiverId();
                if (giverId <= 0)
                    throw new ArgumentException("Выберите дарителя для обновления");

                string name = _view.GetGiverName();
                string bookIdStr = _view.GetGiverBookId();
                string powerStr = _view.GetGiverPower();
                string team = _view.GetGiverTeam();

                int bookId = 0, power = 0;

                if (!string.IsNullOrEmpty(bookIdStr))
                    int.TryParse(bookIdStr, out bookId);

                if (!string.IsNullOrEmpty(powerStr))
                    int.TryParse(powerStr, out power);

                if (_library.UpdateGiver(giverId, name, bookId, power, team))
                {
                    _view.ShowMessage("Даритель обновлен!");
                    OnLoadGivers(sender, e);
                }
                else
                {
                    _view.ShowMessage("Даритель не найден!");
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события удаления дарителя
        /// </summary>
        private void OnDeleteGiver(object sender, EventArgs e)
        {
            try
            {
                int giverId = _view.GetSelectedGiverId();
                if (giverId <= 0)
                    throw new ArgumentException("Выберите дарителя для удаления");

                if (_library.DeleteGiver(giverId))
                {
                    _view.ShowMessage("Даритель удален!");
                    OnLoadGivers(sender, e);
                }
                else
                {
                    _view.ShowMessage("Даритель не найден!");
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события группировки дарителей по командам
        /// </summary>
        private void OnGroupGiversByTeam(object sender, EventArgs e)
        {
            try
            {
                var groups = _library.GroupGiversByTeams();
                string message = "Дарители по командам:\n\n";

                foreach (var group in groups)
                {
                    message += $"Команда: {group.Key}:\n";
                    foreach (var giver in group.Value)
                    {
                        var book = _library.GetBookById(giver.BookId);
                        string bookTitle = book?.Title ?? "Неизвестная книга";
                        message += $"  - {giver.Name} (книга: {bookTitle})\n";
                    }
                    message += "\n";
                }

                _view.ShowMessage(message);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события поиска дарителей с очками силы больше указанного значения
        /// </summary>
        private void OnGiversWithPower(object sender, EventArgs e)
        {
            try
            {
                int minPower = 1000;
                var givers = _library.GetGiversWithBooksAfterYear(minPower);

                var giverDtos = givers.Select(g => new GiverDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    BookId = g.BookId,
                    YearOfCreation = g.YearOfCreation,
                    Team = g.Team
                }).ToList();

                if (giverDtos.Any())
                {
                    _view.ShowGivers(giverDtos);
                    _view.ShowMessage($"Показаны дарители с силой > {minPower}");
                }
                else
                {
                    _view.ShowMessage($"Дарителей с силой > {minPower} не найдено");
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка: {ex.Message}");
            }

        }
    }
}
