using System;
using System.Collections.Generic;
using System.Linq;
using BookManagerApp.Logic;
using BookManagerApp.Model;

namespace BookManagerApp.ConsoleUI
{
    class Program
    {
        static BookManager _bookManager = new BookManager();
        static GiverManager _giverManager = new GiverManager(_bookManager);

        static void Main(string[] args)
        {
            Console.WriteLine(" Добро пожаловать в систему управления! ");

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
        /// ГЛАВНОЕ МЕНЮ - выбор сущности
        /// </summary>
        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("ГЛАВНОЕ МЕНЮ ");
            Console.WriteLine("1.Работа с книгами ");
            Console.WriteLine("2.Работа с дарителями");
            Console.WriteLine("0. Выход ");

            Console.Write("Выберите сущность для работы: ");
        }

        /// <summary>
        /// МЕНЮ ДЛЯ РАБОТЫ С КНИГАМИ
        /// </summary>
        static void WorkWithBooks()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("РАБОТА С КНИГАМИ");
                Console.WriteLine("1.Добавить книгу");
                Console.WriteLine("2.Показать все книги");
                Console.WriteLine("3.Обновить книгу ");
                Console.WriteLine("4.Удалить книгу");
                Console.WriteLine("5.Группировка по авторам");
                Console.WriteLine("6.Книги после указанного года");
                Console.WriteLine("0.Назад в главное меню");
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
        /// МЕНЮ ДЛЯ РАБОТЫ С ДАРИТЕЛЯМИ
        /// </summary>
        static void WorkWithGivers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("РАБОТА С ДАРИТЕЛЯМИ");
                Console.WriteLine("1.Добавить дарителя ");
                Console.WriteLine("2.Показать всех дарителей");
                Console.WriteLine("3.Обновить дарителя");
                Console.WriteLine("4.Удалить дарителя");
                Console.WriteLine("5.Группировка по командам");
                Console.WriteLine("6.Сортировка по очкам силы");
                Console.WriteLine("0.Назад в главное меню");
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
                        FindThingsAfterYearUI();
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
        /// МЕТОДЫ ДЛЯ РАБОТЫ С КНИГАМИ
        /// </summary>
        static void AddBookUI()
        {
            Console.WriteLine("\nДОБАВЛЕНИЕ НОВОЙ КНИГИ");
            Console.Write("Введите название книги: ");
            var title = Console.ReadLine();
            Console.Write("Введите автора: ");
            var author = Console.ReadLine();
            Console.Write("Введите способность книги: ");
            var abilitiesofthebook = Console.ReadLine();
            Console.Write("Введите год издания: ");

            if (int.TryParse(Console.ReadLine(), out int year))
            {
                try
                {
                    _bookManager.AddBook(title, author, abilitiesofthebook, year);
                    Console.WriteLine("Книга успешно добавлена!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат года.");
            }
            WaitForKey();
        }
        /// <summary>
        /// получить все книги 
        /// </summary>
        static void ShowAllBooksUI()
        {
            var books = _bookManager.GetAllBooks();
            if (books.Any())
            {
                Console.WriteLine("\nСПИСОК ВСЕХ КНИГ:");
                foreach (var book in books)
                {
                    Console.WriteLine($"   {book}");
                }
            }
            else
            {
                Console.WriteLine("Список книг пуст");
            }
            WaitForKey();
        }
        /// <summary>
        /// обновить книги
        /// </summary>
        static void UpdateBookUI()
        {
            Console.Write("\n✏Введите ID книги для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Введите новое название (оставьте пустым, чтобы не менять): ");
                var newTitle = Console.ReadLine();
                Console.Write("Введите нового автора (оставьте пустым, чтобы не менять): ");
                var newAuthor = Console.ReadLine();
                Console.Write("Введите новую способность (оставьте пустым, чтобы не менять): ");
                var NewAbility = Console.ReadLine();
                Console.Write("Введите новый год (оставьте 0, чтобы не менять): ");
                int.TryParse(Console.ReadLine(), out int newYear);

                if (_bookManager.UpdateBook(id, newTitle, newAuthor, NewAbility, newYear))
                {
                    Console.WriteLine("Книга успешно обновлена!");
                }
                else
                {
                    Console.WriteLine("Ошибка: Книга с таким ID не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
            }
            WaitForKey();
        }
        /// <summary>
        /// удалить книги
        /// </summary>
        static void DeleteBookUI()
        {
            Console.Write("\nВведите ID книги для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_bookManager.DeleteBook(id))
                {
                    Console.WriteLine("Книга успешно удалена!");
                }
                else
                {
                    Console.WriteLine("Ошибка: Книга с таким ID не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
            }
            WaitForKey();
        }
        /// <summary>
        /// группировка по авторам
        /// </summary>
        static void GroupBooksByAuthorUI()
        {
            var groups = _bookManager.GroupBooksByAuthor();
            if (groups.Any())
            {
                Console.WriteLine("\nКНИГИ, СГРУППИРОВАННЫЕ ПО АВТОРАМ:");
                foreach (var group in groups)
                {
                    Console.WriteLine($"\nАвтор: {group.Key}");
                    foreach (var book in group.Value)
                    {
                        Console.WriteLine($"   - {book.Title} ({book.Year}г. , {book.AbilitiesOfTheBook})");
                    }
                }
            }
            else
            {
                Console.WriteLine("ℹСписок книг пуст.");
            }
            WaitForKey();
        }
        /// <summary>
        /// сортирвока по году
        /// </summary>
        static void FindBooksAfterYearUI()
        {
            Console.Write("\nВведите год: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var books = _bookManager.GetBooksPublishedAfterYear(year);
                if (books.Any())
                {
                    Console.WriteLine($"\nКНИГИ, ВЫШЕДШИЕ ПОСЛЕ {year} ГОДА:");
                    foreach (var book in books)
                    {
                        Console.WriteLine($"   {book}");
                    }
                }
                else
                {
                    Console.WriteLine($"ℹКниг, вышедших после {year} года, не найдено.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат года.");
            }
            WaitForKey();
        }

        /// <summary>
        /// МЕТОДЫ ДЛЯ РАБОТЫ С ДАРИТЕЛЯМИ (ОБНОВЛЕННЫЕ)
        /// </summary>
        static void AddGiverUI()
        {
            Console.WriteLine("\nДОБАВЛЕНИЕ НОВОГО ДАРИТЕЛЯ");
            Console.Write("Введите имя дарителя: ");
            var name = Console.ReadLine();

            Console.Write("Введите ID книги: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Ошибка: Неверный формат ID книги.");
                WaitForKey();
                return;
            }

            Console.Write("Введите очик силы: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Ошибка: Неверный формат очков силы.");
                WaitForKey();
                return;
            }

            Console.Write("Введите команду дарителя: ");
            var team = Console.ReadLine();

            try
            {
                _giverManager.AddGiver(name, bookId, year, team);
                Console.WriteLine("Даритель успешно добавлен!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            WaitForKey();
        }

        static void ShowAllGiversUI()
        {
            var givers = _giverManager.GetAllGivers();
            if (givers.Any())
            {
                Console.WriteLine("\nСПИСОК ВСЕХ ДАРИТЕЛЕЙ:");
                foreach (var giver in givers)
                {
                    var book = _bookManager.GetBookById(giver.BookId);
                    string bookInfo = book != null ? $"{book.Title} ({book.AbilitiesOfTheBook})" : "Книга не найдена";
                    Console.WriteLine($"   ID: {giver.Id}, Имя: {giver.Name}, Книга: {bookInfo}, очки силы: {giver.YearOfCreation}, Команда: {giver.Team}");
                }
            }
            else
            {
                Console.WriteLine("Список дарителей пуст.");
            }
            WaitForKey();
        }

        static void UpdateGiverUI()
        {
            Console.Write("\n✏Введите ID дарителя для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Введите новое имя (оставьте пустым, чтобы не менять): ");
                var newName = Console.ReadLine();
                Console.Write("Введите новый ID книги (оставьте 0, чтобы не менять): ");
                int.TryParse(Console.ReadLine(), out int newBookId);
                Console.Write("Введите новые очки силы (оставьте 0, чтобы не менять): ");
                int.TryParse(Console.ReadLine(), out int newYear);
                Console.Write("Введите новую команду (оставьте пустым, чтобы не менять): ");
                var newTeam = Console.ReadLine();

                try
                {
                    if (_giverManager.UpdateGiver(id, newName, newBookId, newYear, newTeam))
                    {
                        Console.WriteLine("Даритель успешно обновлен!");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Даритель с таким ID не найден.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
            }
            WaitForKey();
        }

        static void DeleteGiverUI()
        {
            Console.Write("\nВведите ID дарителя для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_giverManager.DeleteGiver(id))
                {
                    Console.WriteLine("Даритель успешно удален!");
                }
                else
                {
                    Console.WriteLine("Ошибка: Даритель с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
            }
            WaitForKey();
        }

        static void GroupGiversByTeamsUI()
        {
            var groups = _giverManager.GroupGiversByTeams();
            if (groups.Any())
            {
                Console.WriteLine("\nДАРИТЕЛИ, СГРУППИРОВАННЫЕ ПО КОМАНДАМ:");
                foreach (var group in groups)
                {
                    Console.WriteLine($"\n Команда : {group.Key}");
                    foreach (var giver in group.Value)
                    {
                        var book = _bookManager.GetBookById(giver.BookId);
                        string bookTitle = book?.Title ?? "Неизвестная книга";
                        Console.WriteLine($"   - {giver.Name} (книга: {bookTitle})");
                    }
                }
            }
            else
            {
                Console.WriteLine("Список дарителей пуст.");
            }
            WaitForKey();
        }

        static void FindThingsAfterYearUI()
        {
            Console.Write("\nВведите очки силы: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var givers = _giverManager.GetGiversWithBooksAfterYear(year);
                if (givers.Any())
                {
                    Console.WriteLine($"\n информация о тех у кого очков силы больше чем {year} :");
                    foreach (var giver in givers)
                    {
                        var book = _bookManager.GetBookById(giver.BookId);
                        string bookTitle = book?.Title ?? "Неизвестная книга";
                        Console.WriteLine($"   {giver.Name} - {bookTitle} ({giver.YearOfCreation} очков силы.)");
                    }
                }
                else
                {
                    Console.WriteLine($"Очков силы больше {year}  не найдено.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат очков силы.");
            }
            WaitForKey();
        }

        /// <summary>
        /// ключ ожидания
        /// </summary>
        static void WaitForKey()
        {
            Console.WriteLine("\n⏎ Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}