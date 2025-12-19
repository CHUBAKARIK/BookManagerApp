using BookManagerApp.DataAccessLayer;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Logic
{
    public class SimpleConfigModule: NinjectModule
    {
        public override void Load()
        {
            //Entity Framework
            Bind<IBookRepository>().To<EntityBookRepository>().InSingletonScope();
            Bind<IGiverRepository>().To<EntityGiverRepository>().InSingletonScope();

            //  Dapper 
            // Bind<IBookRepository>().To<DapperBookRepository>().InSingletonScope();
            // Bind<IGiverRepository>().To<DapperGiverRepository>().InSingletonScope();

            // регистрируем бизнес-логику
            Bind<IBookCRUD>().To<BookManager>().InSingletonScope();
            Bind<IBookBUSINESS>().To<BookBusinesService>().InSingletonScope();
            Bind<IGiverCRUD>().To<GiverManager>().InSingletonScope();
            Bind<IGiverBUSINESS>().To<GiverBusinessService>().InSingletonScope();

            // Фасад - через ИНТЕРФЕЙС (D принцип)
            Bind<ILibraryFacade>().To<LibraryFacade>().InSingletonScope();



        }
    }
}
