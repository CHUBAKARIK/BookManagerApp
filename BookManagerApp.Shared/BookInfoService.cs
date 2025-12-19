using BookManagerApp.Shared.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Shared
{
    public static class BookInfoService
    {
        private static Func<int, BookDto> _bookInfoProvider;

        // Метод для инициализации (вызывается из CompositionRoot)
        public static void Initialize(Func<int, BookDto> provider)
        {
            _bookInfoProvider = provider;
        }

        // Метод для использования в View
        public static BookDto GetBookInfo(int bookId)
        {
            return _bookInfoProvider?.Invoke(bookId);
        }
    }
}
