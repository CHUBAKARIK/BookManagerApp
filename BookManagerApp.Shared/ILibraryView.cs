using System;
using System.Collections.Generic;
using BookManagerApp.Shared.models;


namespace BookManagerApp.Shared
{
    /// <summary>
    /// Интерфейс View для архитектуры MVP
    /// Определяет контракт между View (формой) и Presenter
    /// </summary>
    public interface ILibraryView
    {
        /// <summary>
        /// Событие запроса загрузки списка книг
        /// </summary>
        event EventHandler LoadBooks;

        /// <summary>
        /// Событие запроса добавления новой книги
        /// </summary>
        event EventHandler AddBook;

        /// <summary>
        /// Событие запроса обновления существующей книги
        /// </summary>
        event EventHandler UpdateBook;

        /// <summary>
        /// Событие запроса удаления книги
        /// </summary>
        event EventHandler DeleteBook;

        /// <summary>
        /// Событие запроса группировки книг по авторам
        /// </summary>
        event EventHandler GroupBooksByAuthor;

        /// <summary>
        /// Событие запроса поиска книг, изданных после указанного года
        /// </summary>
        event EventHandler BooksAfterYear;

        /// <summary>
        /// Событие запроса загрузки списка дарителей
        /// </summary>
        event EventHandler LoadGivers;

        /// <summary>
        /// Событие запроса добавления нового дарителя
        /// </summary>
        event EventHandler AddGiver;

        /// <summary>
        /// Событие запроса обновления существующего дарителя
        /// </summary>
        event EventHandler UpdateGiver;

        /// <summary>
        /// Событие запроса удаления дарителя
        /// </summary>
        event EventHandler DeleteGiver;

        /// <summary>
        /// Событие запроса группировки дарителей по командам
        /// </summary>
        event EventHandler GroupGiversByTeam;

        /// <summary>
        /// Событие запроса поиска дарителей с очками силы больше указанного значения
        /// </summary>
        event EventHandler GiversWithPower;

        /// <summary>
        /// Возвращает название книги из формы
        /// </summary>
        string GetBookTitle();

        /// <summary>
        /// Возвращает автора книги из формы
        /// </summary>
        string GetBookAuthor();

        /// <summary>
        /// Возвращает способность книги из формы
        /// </summary>
        string GetBookAbility();

        /// <summary>
        /// Возвращает год издания книги из формы
        /// </summary>
        string GetBookYear();

        /// <summary>
        /// Возвращает имя дарителя из формы
        /// </summary>
        string GetGiverName();

        /// <summary>
        /// Возвращает ID книги дарителя из формы
        /// </summary>
        string GetGiverBookId();

        /// <summary>
        /// Возвращает очки силы дарителя из формы
        /// </summary>
        string GetGiverPower();

        /// <summary>
        /// Возвращает команду дарителя из формы
        /// </summary>
        string GetGiverTeam();

        /// <summary>
        /// Возвращает ID выбранной книги в DataGridView
        /// </summary>
        int GetSelectedBookId();

        /// <summary>
        /// Возвращает ID выбранного дарителя в DataGridView
        /// </summary>
        int GetSelectedGiverId();

        /// <summary>
        /// Отображает список книг в DataGridView
        /// </summary>
        void ShowBooks(List<BookDto> books);

        /// <summary>
        /// Отображает список дарителей в DataGridView
        /// </summary>
        void ShowGivers(List<GiverDto> givers);

        /// <summary>
        /// Показывает информационное сообщение пользователю
        /// </summary>
        void ShowMessage(string message);

        /// <summary>
        /// Очищает все поля ввода на форме
        /// </summary>
        void ClearForm();
    }
}