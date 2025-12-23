// BookManagerApp.ConsoleUI/ConsoleView.cs
using BookManagerApp.Shared;
using BookManagerApp.Shared.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagerApp.ConsoleUI
{
    /// <summary>
    /// Реализация View для консольного интерфейса
    /// НЕ содержит ссылок на Presenter!
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

        // Поля для данных формы
        private string _field1 = "";
        private string _field2 = "";
        private string _field3 = "";
        private string _field4 = "";
        private int _selectedBookId = 0;
        private int _selectedGiverId = 0;

        // Методы для установки значений
        public void SetField1(string value) => _field1 = value;
        public void SetField2(string value) => _field2 = value;
        public void SetField3(string value) => _field3 = value;
        public void SetField4(string value) => _field4 = value;
        public void SetSelectedBookId(int id) => _selectedBookId = id;
        public void SetSelectedGiverId(int id) => _selectedGiverId = id;

        // Методы для получения данных книг
        public string GetBookTitle() => _field1;
        public string GetBookAuthor() => _field2;
        public string GetBookAbility() => _field3;
        public string GetBookYear() => _field4;

        // Методы для получения данных дарителей
        public string GetGiverName() => _field1;
        public string GetGiverBookId() => _field2;
        public string GetGiverPower() => _field3;
        public string GetGiverTeam() => _field4;

        // Получение выбранных ID
        public int GetSelectedBookId() => _selectedBookId;
        public int GetSelectedGiverId() => _selectedGiverId;

        // Отображение книг
        public void ShowBooks(List<BookDto> books)
        {
            Console.Clear();
            if (books.Any())
            {
                Console.WriteLine("\n СПИСОК ВСЕХ КНИГ:");
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
        /// Отображение дарителей
        /// </summary>
        /// <param name="givers"></param>
        public void ShowGivers(List<GiverDto> givers)
        {
            Console.Clear();
            if (givers.Any())
            {
                Console.WriteLine("\n🎁 СПИСОК ВСЕХ ДАРИТЕЛЕЙ:");
                foreach (var giver in givers)
                {
                    
                    var bookInfo = BookInfoService.GetBookInfo(giver.BookId);
                    var bookTitle = bookInfo?.Title ?? $"Книга ID:{giver.BookId}";

                    Console.WriteLine($"   ID: {giver.Id}, Имя: {giver.Name}, Книга: {bookTitle}, Сила: {giver.YearOfCreation}, Команда: {giver.Team}");
                }
            }
            else
            {
                Console.WriteLine("Список дарителей пуст.");
            }
            WaitForKey();
        }

        /// <summary>
        /// Показ сообщения
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            Console.WriteLine("\n" + message);
            WaitForKey();
        }

        /// <summary>
        /// Очистка формы
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

        // Методы для вызова событий дарителей
        public void InvokeLoadGivers() => LoadGivers?.Invoke(this, EventArgs.Empty);
        public void InvokeAddGiver() => AddGiver?.Invoke(this, EventArgs.Empty);
        public void InvokeUpdateGiver() => UpdateGiver?.Invoke(this, EventArgs.Empty);
        public void InvokeDeleteGiver() => DeleteGiver?.Invoke(this, EventArgs.Empty);
        public void InvokeGroupGiversByTeam() => GroupGiversByTeam?.Invoke(this, EventArgs.Empty);
        public void InvokeGiversWithPower() => GiversWithPower?.Invoke(this, EventArgs.Empty);

        private void WaitForKey()
        {
            Console.WriteLine("\n⏎ Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}