using BookManagerApp.ConsoleUI;
using BookManagerApp.Presenter;
using BookManagerApp.WinFormsUI;
using System;
using System.Windows.Forms;
using WinFormsApp = System.Windows.Forms.Application; // Алиас, чтобы не конфликтовать с namespace BookManagerApp.Application

namespace BookManagerApp.Application
{
    /// <summary>
    /// Точка входа приложения.
    /// Отвечает только за старт программы и выбор интерфейса (Console или WinForms).
    /// Вся бизнес-логика и работа с данными находятся в других проектах.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Главная точка входа в приложение.
        /// Печатает приветствие, определяет какой интерфейс запускать и запускает его.
        /// </summary>
        /// <param name="args">
        /// Аргументы командной строки:
        /// <list type="bullet">
        /// <item><description><c>-console</c> — запустить консольный интерфейс</description></item>
        /// <item><description><c>-winforms</c> — запустить WinForms интерфейс</description></item>
        /// </list>
        /// Если аргументы не указаны — приложение спрашивает пользователя.
        /// </param>
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("Система управления книгами и дарителями");
            Console.WriteLine();

            bool useConsole = ShouldUseConsole(args);

            if (useConsole)
                LaunchConsoleApp();
            else
                LaunchWinFormsApp();
        }

        /// <summary>
        /// Определяет, какой интерфейс нужно запускать: консольный или графический.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        /// <returns>
        /// <c>true</c> — запускать консольный интерфейс,
        /// <c>false</c> — запускать WinForms интерфейс.
        /// </returns>
        /// <remarks>
        /// Логика:
        /// <list type="number">
        /// <item><description>Если передан <c>-console</c> — выбирается консоль.</description></item>
        /// <item><description>Если передан <c>-winforms</c> — выбирается WinForms.</description></item>
        /// <item><description>Если ничего не передано — спрашиваем пользователя через консоль.</description></item>
        /// </list>
        /// </remarks>
        private static bool ShouldUseConsole(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower() == "-console")
                return true;

            if (args.Length > 0 && args[0].ToLower() == "-winforms")
                return false;

            return AskUserForInterface();
        }

        /// <summary>
        /// Спрашивает пользователя в консоли, какой интерфейс запускать.
        /// </summary>
        /// <returns>
        /// <c>true</c> — консоль,
        /// <c>false</c> — WinForms.
        /// Если введено некорректно — по умолчанию выбирается консоль.
        /// </returns>
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

        /// <summary>
        /// Запускает консольную версию приложения.
        /// Создаёт ConsoleView, затем Presenter через PresenterFactory,
        /// затем запускает ConsoleApp (меню и цикл работы).
        /// </summary>
        /// <remarks>
        /// Важно: Presenter создаётся обязательно, потому что он подписывается на события View
        /// (LoadBooks, AddBook и т.д.). Без Presenter события View никто не обработает.
        /// </remarks>
        private static void LaunchConsoleApp()
        {
            Console.WriteLine("\nЗапуск консольного интерфейса ");

            try
            {
                var consoleView = new ConsoleView();

                
                var presenter = PresenterFactory.CreateLibraryPresenter(consoleView);

                var consoleApp = new ConsoleApp(consoleView);
                consoleApp.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запуске: {ex.Message}");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Запускает WinForms версию приложения.
        /// Включает визуальные стили, получает фасад через PresenterFactory,
        /// создаёт MainForm и запускает WinForms message-loop.
        /// </summary>
        /// <remarks>
        /// Здесь мы используем алиас <c>WinFormsApp</c>, чтобы явно обратиться к
        /// <see cref="System.Windows.Forms.Application"/> и не конфликтовать с namespace
        /// <c>BookManagerApp.Application</c>.
        /// </remarks>
        [STAThread]
        private static void LaunchWinFormsApp()
        {
            WinFormsApp.EnableVisualStyles();
            WinFormsApp.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();

            // ВОТ ГДЕ ПОДПИСКА на события делается ОДИН РАЗ
            PresenterFactory.CreateLibraryPresenter(mainForm);

            WinFormsApp.Run(mainForm);
        }

    }
}
