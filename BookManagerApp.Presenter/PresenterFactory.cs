// BookManagerApp.Presenter/PresenterFactory.cs
using BookManagerApp.Logic;
using BookManagerApp.Shared;
using Ninject;
using System;

namespace BookManagerApp.Presenter
{
    /// <summary>
    /// Фабрика для создания Presenter'ов
    /// Изолирует создание зависимостей от View
    /// </summary>
    public static class PresenterFactory
    {
        private static IKernel _kernel;

        static PresenterFactory()
        {
            _kernel = new StandardKernel(new SimpleConfigModule());
            InitializeSharedServices();
        }

        /// <summary>
        /// Создает LibraryPresenter для переданного View
        /// </summary>
        public static LibraryPresenter CreateLibraryPresenter(ILibraryView view)
        {
            var library = _kernel.Get<ILibraryFacade>();
            return new LibraryPresenter(view, library);
        }

        /// <summary>
        /// Получает фасад библиотеки (для запускающего кода)
        /// </summary>
        public static ILibraryFacade GetLibraryFacade()
        {
            return _kernel.Get<ILibraryFacade>();
        }

        /// <summary>
        /// Инициализирует сервисы в Shared слое
        /// </summary>
        private static void InitializeSharedServices()
        {
            BookInfoService.Initialize(GetBookInfoInternal);
        }

        private static Shared.models.BookDto GetBookInfoInternal(int bookId)
        {
            try
            {
                var library = _kernel.Get<ILibraryFacade>();
                var book = library.GetBookById(bookId);
                if (book != null)
                {
                    return new Shared.models.BookDto
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
                
            }
            return null;
        }
    }
}