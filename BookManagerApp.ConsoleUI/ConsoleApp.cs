using System;

namespace BookManagerApp.ConsoleUI
{
    public class ConsoleApp
    {
        private readonly ConsoleView _view;

        public ConsoleApp(ConsoleView view)
        {
            _view = view;
        }

        /// <summary>
        /// Запускает главный цикл консольного приложения и обрабатывает выбор пользователя.
        /// </summary>
        public void Run()
        {
            Console.Title = " Система управления книгами";
            Console.WriteLine(" Система Управления ");

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
                        Console.WriteLine(" Выход из системы");
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню");
                        WaitForKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Показывает главное меню приложения.
        /// </summary>
        private void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("ГЛАВНОЕ МЕНЮ ");
            Console.WriteLine("1. Работа с книгами ");
            Console.WriteLine("2. Работа с дарителями");
            Console.WriteLine("0. Выход ");

            Console.Write("Выберите сущность для работы: ");
        }

        /// <summary>
        /// Меню и обработка действий для раздела "Книги".
        /// </summary>
        private void WorkWithBooks()
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
        /// Меню и обработка действий для раздела "Дарители".
        /// </summary>
        private void WorkWithGivers()
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
        /// Считывает данные книги из консоли и вызывает добавление через View.
        /// </summary>
        private void AddBookUI()
        {
            Console.Clear();
            Console.WriteLine("\nДОБАВЛЕНИЕ НОВОЙ КНИГИ");
            Console.Write("Введите название книги: ");
            _view.SetField1(Console.ReadLine());
            Console.Write("Введите автора: ");
            _view.SetField2(Console.ReadLine());
            Console.Write("Введите способность книги: ");
            _view.SetField3(Console.ReadLine());
            Console.Write("Введите год издания: ");
            _view.SetField4(Console.ReadLine());

            _view.InvokeAddBook();
        }

        /// <summary>
        /// Запрашивает вывод всех книг через View.
        /// </summary>
        private void ShowAllBooksUI()
        {
            _view.InvokeLoadBooks();
        }

        /// <summary>
        /// Считывает ID и новые значения книги и вызывает обновление через View.
        /// </summary>
        private void UpdateBookUI()
        {
            Console.Clear();
            Console.Write("\n✏ Введите ID книги для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _view.SetSelectedBookId(id);

                Console.Write("Введите новое название (оставьте пустым, чтобы не менять): ");
                _view.SetField1(Console.ReadLine());
                Console.Write("Введите нового автора (оставьте пустым, чтобы не менять): ");
                _view.SetField2(Console.ReadLine());
                Console.Write("Введите новую способность (оставьте пустым, чтобы не менять): ");
                _view.SetField3(Console.ReadLine());
                Console.Write("Введите новый год (оставьте пустым, чтобы не менять): ");
                _view.SetField4(Console.ReadLine());

                _view.InvokeUpdateBook();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Считывает ID книги и вызывает удаление через View.
        /// </summary>
        private void DeleteBookUI()
        {
            Console.Clear();
            Console.Write("\nВведите ID книги для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _view.SetSelectedBookId(id);
                _view.InvokeDeleteBook();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Запрашивает группировку книг по авторам через View.
        /// </summary>
        private void GroupBooksByAuthorUI()
        {
            _view.InvokeGroupBooksByAuthor();
        }

        /// <summary>
        /// Считывает год и запрашивает поиск книг после указанного года через View.
        /// </summary>
        private void FindBooksAfterYearUI()
        {
            Console.Clear();
            Console.Write("\nВведите год: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine($"Книги после {year} года будут показаны...");
                _view.InvokeBooksAfterYear();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат года.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Считывает данные дарителя из консоли и вызывает добавление через View.
        /// </summary>
        private void AddGiverUI()
        {
            Console.Clear();
            Console.WriteLine("\nДОБАВЛЕНИЕ НОВОГО ДАРИТЕЛЯ");
            Console.Write("Введите имя дарителя: ");
            _view.SetField1(Console.ReadLine());
            Console.Write("Введите ID книги: ");
            _view.SetField2(Console.ReadLine());
            Console.Write("Введите очки силы: ");
            _view.SetField3(Console.ReadLine());
            Console.Write("Введите команду дарителя: ");
            _view.SetField4(Console.ReadLine());

            _view.InvokeAddGiver();
        }

        /// <summary>
        /// Запрашивает вывод всех дарителей через View.
        /// </summary>
        private void ShowAllGiversUI()
        {
            _view.InvokeLoadGivers();
        }

        /// <summary>
        /// Считывает ID и новые значения дарителя и вызывает обновление через View.
        /// </summary>
        private void UpdateGiverUI()
        {
            Console.Clear();
            Console.Write("\n✏ Введите ID дарителя для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _view.SetSelectedGiverId(id);

                Console.Write("Введите новое имя (оставьте пустым, чтобы не менять): ");
                _view.SetField1(Console.ReadLine());
                Console.Write("Введите новый ID книги (оставьте пустым, чтобы не менять): ");
                _view.SetField2(Console.ReadLine());
                Console.Write("Введите новые очки силы (оставьте пустым, чтобы не менять): ");
                _view.SetField3(Console.ReadLine());
                Console.Write("Введите новую команду (оставьте пустым, чтобы не менять): ");
                _view.SetField4(Console.ReadLine());

                _view.InvokeUpdateGiver();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Считывает ID дарителя и вызывает удаление через View.
        /// </summary>
        private void DeleteGiverUI()
        {
            Console.Clear();
            Console.Write("\nВведите ID дарителя для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _view.SetSelectedGiverId(id);
                _view.InvokeDeleteGiver();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат ID.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Запрашивает группировку дарителей по командам через View.
        /// </summary>
        private void GroupGiversByTeamsUI()
        {
            _view.InvokeGroupGiversByTeam();
        }

        /// <summary>
        /// Считывает минимальную силу и запрашивает список дарителей с силой выше порога через View.
        /// </summary>
        private void FindGiversWithPowerUI()
        {
            Console.Clear();
            Console.Write("\nВведите минимальные очки силы: ");
            if (int.TryParse(Console.ReadLine(), out int minPower))
            {
                Console.WriteLine($"Дарители с очками силы больше {minPower} будут показаны...");
                _view.InvokeGiversWithPower();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат очков силы.");
                WaitForKey();
            }
        }

        /// <summary>
        /// Ожидает нажатия клавиши для продолжения.
        /// </summary>
        private void WaitForKey()
        {
            Console.WriteLine("\n⏎ Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}
