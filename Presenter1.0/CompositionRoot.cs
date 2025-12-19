using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Presenter
{
    public static class CompositionRoot
    {
        private static IKernel _kernel;

        static CompositionRoot()
        {
            // SimpleConfigModule берется из Logic проекта
            _kernel = new StandardKernel(new SimpleConfigModule());
        }

        public static LibraryPresenter CreatePresenter(ILibraryView view)
        {
            var library = _kernel.Get<ILibraryFacade>();
            return new LibraryPresenter(view, library);
        }
    }
}
