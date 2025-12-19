using BookManagerApp.Logic;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerApp.Shared;
using BookManagerApp.Shared.models;


namespace BookManagerApp.Presenter
{
    public static class CompositionRoot
    {
        private static IKernel _kernel;

        static CompositionRoot()
        {
            _kernel = new StandardKernel(new SimpleConfigModule());

            // Инициализируем BookInfoService в Shared
            BookInfoService.Initialize(GetBookInfoInternal);
        }

        public static LibraryPresenter CreatePresenter(ILibraryView view)
        {
            var library = _kernel.Get<ILibraryFacade>();
            return new LibraryPresenter(view, library);
        }

        private static BookDto GetBookInfoInternal(int bookId)
        {
            try
            {
                var library = _kernel.Get<ILibraryFacade>();
                var book = library.GetBookById(bookId);
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
    }
}
