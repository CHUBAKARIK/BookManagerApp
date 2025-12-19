using BookManagerApp.Logic;
using BookManagerApp.Presenter;
using BookManagerApp.Shared;
using Ninject;

namespace BookManagerApp.ConsoleUI
{
    /// <summary>
    /// Реализация View для консольного интерфейса в архитектуре MVP.
    /// Предоставляет UI для работы с книгами и дарителями через события.
    /// </summary>
    public class ConsoleView : ILibraryView
    {
        // События для взаимодействия с Presenter'ом
        public event EventHandler LoadBooks;
        public event EventHandler AddBook;
        public event EventHandler UpdateBook;
        public event EventHandler DeleteBook;
        public event EventHandler GroupBooksByAuthor;
        public event EventHandler BooksAfterYear;

        public event EventHandler LoadGivers;
        public event EventHandler AddGiver;
        public event EventHandler UpdateGiver;
        public event EventHandler DeleteGiver;
        public event EventHandler GroupGiversByTeam;
        public event EventHandler GiversWithPower;

        /// <summary>
        /// Внутренние поля для хранения данных формы
        /// </summary>
        private string _field1 = "";
        private string _field2 = "";
        private string _field3 = "";
        private string _field4 = "";
        private int _selectedBookId = 0;
        private int _selectedGiverId = 0;

        /// <summary>
        /// Установка значений полей формы
        /// </summary>
        /// <param name="value"></param>
        public void SetField1(string value) => _field1 = value;
        public void SetField2(string value) => _field2 = value;
        public void SetField3(string value) => _field3 = value;
        public void SetField4(string value) => _field4 = value;
        public void SetSelectedBookId(int id) => _selectedBookId = id;
        public void SetSelectedGiverId(int id) => _selectedGiverId = id;

        /// <summary>
        /// Получение данных для книг
        /// </summary>
        /// <returns></returns>
        public string GetBookTitle() => _field1;
        public string GetBookAuthor() => _field2;
        public string GetBookAbility() => _field3;
        public string GetBookYear() => _field4;

        /// <summary>
        /// Получение данных для дарителей
        /// </summary>
        /// <returns></returns>
        public string GetGiverName() => _field1;
        public string GetGiverBookId() => _field2;
        public string GetGiverPower() => _field3;
        public string GetGiverTeam() => _field4;

        /// <summary>
        /// Получение выбранных ID
        /// </summary>
        /// <returns></returns>
        public int GetSelectedBookId() => _selectedBookId;
        public int GetSelectedGiverId() => _selectedGiverId;

        /// <summary>
        /// Отображает список книг в консоли.
        /// </summary>
        public void ShowBooks(List<BookManagerApp.Shared.models.BookDto> books)
        {
            Console.Clear();
            if (books.Any())
            {
                Console.WriteLine("\nСПИСОК ВСЕХ КНИГ:");
                foreach (var book in books)
                {
                    Console.WriteLine($"   ID: {book.Id}, \"{book.Title}\" - {book.Author} ({book.Year}г., {book.AbilitiesOfTheBook})");
                }
            }
            else
            {
                Console.WriteLine("Список книг пуст");
            }
            WaitForKey();
        }

        /// <summary>
        /// Отображает список дарителей в консоли.
        /// </summary>
        public void ShowGivers(List<BookManagerApp.Shared.models.GiverDto> givers)
        {
            Console.Clear();
            if (givers.Any())
            {
                Console.WriteLine("\nСПИСОК ВСЕХ ДАРИТЕЛЕЙ:");
                foreach (var giver in givers)
                {
                    Console.WriteLine($"   ID: {giver.Id}, Имя: {giver.Name}, ID книги: {giver.BookId}, Очки силы: {giver.YearOfCreation}, Команда: {giver.Team}");
                }
            }
            else
            {
                Console.WriteLine("Список дарителей пуст.");
            }
            WaitForKey();
        }

        /// <summary>
        /// Выводит информационное сообщение в консоль.
        /// </summary>
        public void ShowMessage(string message)
        {
            Console.WriteLine("\n" + message);
            WaitForKey();
        }

        /// <summary>
        /// Очищает все поля ввода формы.
        /// </summary>
        public void ClearForm()
        {
            _field1 = _field2 = _field3 = _field4 = "";
        }

        /// <summary>
        /// Методы для вызова событий книг
        /// </summary>
        public void InvokeLoadBooks() => LoadBooks?.Invoke(this, EventArgs.Empty);
        public void InvokeAddBook() => AddBook?.Invoke(this, EventArgs.Empty);
        public void InvokeUpdateBook() => UpdateBook?.Invoke(this, EventArgs.Empty);
        public void InvokeDeleteBook() => DeleteBook?.Invoke(this, EventArgs.Empty);
        public void InvokeGroupBooksByAuthor() => GroupBooksByAuthor?.Invoke(this, EventArgs.Empty);
        public void InvokeBooksAfterYear() => BooksAfterYear?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Методы для вызова событий дарителей
        /// </summary>
        public void InvokeLoadGivers() => LoadGivers?.Invoke(this, EventArgs.Empty);
        public void InvokeAddGiver() => AddGiver?.Invoke(this, EventArgs.Empty);
        public void InvokeUpdateGiver() => UpdateGiver?.Invoke(this, EventArgs.Empty);
        public void InvokeDeleteGiver() => DeleteGiver?.Invoke(this, EventArgs.Empty);
        public void InvokeGroupGiversByTeam() => GroupGiversByTeam?.Invoke(this, EventArgs.Empty);
        public void InvokeGiversWithPower() => GiversWithPower?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Ожидает нажатия любой клавиши для продолжения.
        /// </summary>
        private void WaitForKey()
        {
            Console.WriteLine("\n⏎ Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }



    }

    /// <summary>
    /// Основной класс консольного приложения.
    /// Управляет пользовательским интерфейсом и взаимодействием с View.
    /// </summary>
    class Program
    {
        private static ConsoleView _consoleView;
        

        /// <summary>
        /// Точка входа в приложение. Инициализирует компоненты MVP и запускает главный цикл.
        /// </summary>
        static void Main(string[] args)
        {
            // Настройка Dependency Injection
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            var library = ninjectKernel.Get<ILibraryFacade>();

            // Создание View и Presenter'а
            _consoleView = new ConsoleView();
             var presenter = new LibraryPresenter(_consoleView, library);

            Console.WriteLine(" Добро пожаловать в систему управления! ");

            // Главный цикл приложения
            while (true)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        WorkWithBooks();
                        break;
                    case "2":
                        WorkWithGivers();
                        break;
                    case "0":
                        Console.WriteLine(" Выход из системы...");
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню");
                        WaitForKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Отображает главное меню приложения.
        /// </summary>
        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("ГЛАВНОЕ МЕНЮ ");
            Console.WriteLine("1. Работа с книгами ");
            Console.WriteLine("2. Работа с дарителями");
            Console.WriteLine("0. Выход ");

            Console.Write("Выберите сущность для работы: ");
        }

        /// <summary>
        /// Меню для работы с книгами.
        /// </summary>
        static void WorkWithBooks()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("РАБОТА С КНИГАМИ");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Показать все книги");
                Console.WriteLine("3. Обновить книгу ");
                Console.WriteLine("4. Удалить книгу");
                Console.WriteLine("5. Группировка по авторам");
                Console.WriteLine("6. Книги после указанного года");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBookUI();
                        break;
                    case "2":
                        ShowAllBooksUI();
                        break;
                    case "3":
                        UpdateBookUI();
                        break;
                    case "4":
                        DeleteBookUI();
                        break;
                    case "5":
                        GroupBooksByAuthorUI();
                        break;
                    case "6":
                        FindBooksAfterYearUI();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню");
                        WaitForKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Меню для работы с дарителями.
        /// </summary>
        static void WorkWithGivers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("РАБОТА С ДАРИТЕЛЯМИ");
                Console.WriteLine("1. Добавить дарителя ");
                Console.WriteLine("2. Показать всех дарителей");
                Console.WriteLine("3. Обновить дарителя");
                Console.WriteLine("4. Удалить дарителя");
                Console.WriteLine("5. Группировка по командам");
                Console.WriteLine("6. Сортировка по очкам силы");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Выберите действие:");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGiverUI();
                        break;
                    case "2":
                        ShowAllGiversUI();
                        break;
                    case "3":
                        UpdateGiverUI();
                        break;
                    case "4":
                        DeleteGiverUI();
                        break;
                    case "5":
                        GroupGiversByTeamsUI();
                        break;
                    case "6":
                        FindGiversWithPowerUI();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню");
                        WaitForKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Добавление новой книги через пользовательский ввод.
        /// </summary>
        static void AddBookUI()
        {
            Console.Clear();
            Console.WriteLine("\nДОБАВЛЕНИЕ НОВОЙ КНИГИ");
            Console.Write("Введите название книги: ");
            _consoleView.SetField1(Console.ReadLine());
            Console.Write("Введите автора: ");
            _consoleView.SetField2(Console.ReadLine());
            Console.Write("Введите способность книги: ");
            _consoleView.SetField3(Console.ReadLine());
            Console.Write("Введите год издания: ");
            _consoleView.SetField4(Console.ReadLine());

            _consoleView.InvokeAddBook();
        }

        /// <summary>
        /// Отображение всех книг.
        /// </summary>
        static void ShowAllBooksUI()
        {
            _consoleView.InvokeLoadBooks();
        }

        /// <summary>
        /// Обновление информации о книге.
        /// </summary>
        static void UpdateBookUI()
        {
            Console.Clear();
            Console.Write("\n✏ Введите ID книги для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _consoleView.SetSelectedBookId(id);

                Console.Write("Введите новое название (оставьте пустым, чтобы не менять): ");
                _consoleView.SetField1(Console.ReadLine());
                Console.Write("Введите нового автора (оставьте пустым, чтобы не менять): ");
                _consoleView.SetField2(Console.ReadLine());
                Console.Write("Введите новую способность (оставьте пустым, чтобы не менять): ");
                _consoleView.SetField3(Console.ReadLine());
                Console.Write("Введите новый год (оставьте пустым, чтобы не менять): ");
                _consoleView.SetField4(Console.ReadLine());

                _consoleView.InvokeUpdateBook();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Удаление книги по ID.
        /// </summary>
        static void DeleteBookUI()
        {
            Console.Clear();
            Console.Write("\nВведите ID книги для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _consoleView.SetSelectedBookId(id);
                _consoleView.InvokeDeleteBook();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Группировка книг по авторам.
        /// </summary>
        static void GroupBooksByAuthorUI()
        {
            _consoleView.InvokeGroupBooksByAuthor();
        }

        /// <summary>
        /// Поиск книг, изданных после указанного года.
        /// </summary>
        static void FindBooksAfterYearUI()
        {
            Console.Clear();
            Console.Write("\nВведите год: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine($"Книги после {year} года будут показаны...");
                _consoleView.InvokeBooksAfterYear();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат года.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Добавление нового дарителя.
        /// </summary>
        static void AddGiverUI()
        {
            Console.Clear();
            Console.WriteLine("\nДОБАВЛЕНИЕ НОВОГО ДАРИТЕЛЯ");
            Console.Write("Введите имя дарителя: ");
            _consoleView.SetField1(Console.ReadLine());
            Console.Write("Введите ID книги: ");
            _consoleView.SetField2(Console.ReadLine());
            Console.Write("Введите очки силы: ");
            _consoleView.SetField3(Console.ReadLine());
            Console.Write("Введите команду дарителя: ");
            _consoleView.SetField4(Console.ReadLine());

            _consoleView.InvokeAddGiver();
        }

        /// <summary>
        /// Отображение всех дарителей.
        /// </summary>
        static void ShowAllGiversUI()
        {
            _consoleView.InvokeLoadGivers();
        }

        /// <summary>
        /// Обновление информации о дарителе.
        /// </summary>
        static void UpdateGiverUI()
        {
            Console.Clear();
            Console.Write("\n✏ Введите ID дарителя для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _consoleView.SetSelectedGiverId(id);

                Console.Write("Введите новое имя (оставьте пустым, чтобы не менять): ");
                _consoleView.SetField1(Console.ReadLine());
                Console.Write("Введите новый ID книги (оставьте пустым, чтобы не менять): ");
                _consoleView.SetField2(Console.ReadLine());
                Console.Write("Введите новые очки силы (оставьте пустым, чтобы не менять): ");
                _consoleView.SetField3(Console.ReadLine());
                Console.Write("Введите новую команду (оставьте пустым, чтобы не менять): ");
                _consoleView.SetField4(Console.ReadLine());

                _consoleView.InvokeUpdateGiver();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Удаление дарителя по ID.
        /// </summary>
        static void DeleteGiverUI()
        {
            Console.Clear();
            Console.Write("\nВведите ID дарителя для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _consoleView.SetSelectedGiverId(id);
                _consoleView.InvokeDeleteGiver();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Группировка дарителей по командам.
        /// </summary>
        static void GroupGiversByTeamsUI()
        {
            _consoleView.InvokeGroupGiversByTeam();
        }

        /// <summary>
        /// Поиск дарителей с очками силы больше указанного значения.
        /// </summary>
        static void FindGiversWithPowerUI()
        {
            Console.Clear();
            Console.Write("\nВведите минимальные очки силы: ");
            if (int.TryParse(Console.ReadLine(), out int minPower))
            {
                Console.WriteLine($"Дарители с очками силы больше {minPower} будут показаны...");
                _consoleView.InvokeGiversWithPower();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат очков силы.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Ожидание нажатия клавиши для продолжения работы.
        /// </summary>
        static void WaitForKey()
        {
            Console.WriteLine("\n⏎ Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}