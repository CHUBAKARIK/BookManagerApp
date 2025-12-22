using System;
using BookManagerApp.ConsoleUI;
using BookManagerApp.Presenter;
using BookManagerApp.Shared;
using BookManagerApp.WinFormsUI;
using System.Windows.Forms;

namespace BookManagerApp.Application
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления книгами и дарителями ===");
            Console.WriteLine("Архитектура: MVP с Dependency Injection");
            Console.WriteLine();

            // Определяем режим запуска
            bool useConsole = DetermineLaunchMode(args);

            if (useConsole)
                LaunchConsoleApp();
            else
                LaunchWinFormsApp();
        }

        private static bool DetermineLaunchMode(string[] args)
        {
            // Вариант 1: Через аргументы командной строки
            if (args.Length > 0)
            {
                if (args[0].ToLower() == "-console") return true;
                if (args[0].ToLower() == "-winforms") return false;
            }

            // Вариант 2: Спросить пользователя
            return AskUserForInterface();
        }

        private static bool AskUserForInterface()
        {
            Console.WriteLine("Выберите интерфейс для запуска:");
            Console.WriteLine("1 - Консольное приложение (Console)");
            Console.WriteLine("2 - Графический интерфейс (WinForms)");
            Console.Write("Ваш выбор (1 или 2): ");

            var choice = Console.ReadLine();

            if (choice == "1") return true;
            if (choice == "2") return false;

            Console.WriteLine("Неверный выбор. Запускаю консольный интерфейс по умолчанию.");
            return true;
        }

        private static void LaunchConsoleApp()
        {
            Console.WriteLine("\n=== Запуск консольного интерфейса ===");

            try
            {
                // 1. Создаем View
                var consoleView = new ConsoleView();

                // 2. Через фабрику создаем Presenter (View НЕ знает о PresenterFactory)
                var presenter = PresenterFactory.CreateLibraryPresenter(consoleView);

                // 3. Создаем и запускаем консольное приложение
                var consoleApp = new ConsoleApp(consoleView);
                consoleApp.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запуске: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }

        [STAThread]
        private static void LaunchWinFormsApp()
        {
            Console.WriteLine("\n=== Запуск графического интерфейса ===");

            try
            {
                // 1. Получаем фасад через фабрику
                var libraryFacade = PresenterFactory.GetLibraryFacade();

                // 2. Настраиваем Windows Forms
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // 3. Создаем форму (она создаст Presenter внутри себя)
                Application.Run(new MainForm(libraryFacade));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запуске WinForms: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }
    }
}