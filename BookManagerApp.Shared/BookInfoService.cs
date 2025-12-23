// BookManagerApp.Shared/BookInfoService.cs
using BookManagerApp.Shared.models;
using System;

namespace BookManagerApp.Shared
{
    /// <summary>
    /// Сервис для получения информации о книгах
    /// Позволяет View получать данные без зависимости от Presenter'а
    /// </summary>
    public static class BookInfoService
    {
        private static Func<int, BookDto> _bookInfoProvider;

        /// <summary>
        /// Инициализация (вызывается из PresenterFactory при старте приложения)
        /// </summary>
        public static void Initialize(Func<int, BookDto> provider)
        {
            _bookInfoProvider = provider;
        }

        /// <summary>
        /// Получение информации о книге по ID
        /// </summary>
        public static BookDto GetBookInfo(int bookId)
        {
            return _bookInfoProvider?.Invoke(bookId);
        }
    }
}