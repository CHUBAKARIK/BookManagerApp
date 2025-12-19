using Microsoft.EntityFrameworkCore;
using BookManagerApp.Model;
using BookManagerApp.DataAccessLayer;

namespace BookManagerApp.DataAccessLayer
{
    public class DatabaseInitializer
    {
        public static void Initialize(BookDbcontext context)
        {
            
            //context.Database.EnsureCreated();

            // Проверяем есть ли уже данные в таблице Books
            if (!context.Books.Any())
            {
                // тест1
                var books = new Book[]
                {
                    new Book { Title = "Огненный клинок", Author = "Мастер Огня",
                              AbilitiesOfTheBook = "Контроль над пламенем", Year = 2020 },
                    new Book { Title = "Ледяной щит", Author = "Повелитель Льда",
                              AbilitiesOfTheBook = "Создание ледяных барьеров", Year = 2019 },
                    new Book { Title = "Ветряной посох", Author = "Хранитель Ветров",
                              AbilitiesOfTheBook = "Управление воздушными потоками", Year = 2021 }
                };

                context.Books.AddRange(books);
                context.SaveChanges();

                //тест
                var givers = new Giver[]
                {
                    new Giver { Name = "Аэлин", BookId = 1,
                               YearOfCreation = 1500, Team = "Орден Феникса" },
                    new Giver { Name = "Эльрик", BookId = 2,
                               YearOfCreation = 1700, Team = "Братство Стали" },
                    new Giver { Name = "Сильвана", BookId = 3,
                               YearOfCreation = 1600, Team = "Стражи Света" }
                };

                context.Givers.AddRange(givers);
                context.SaveChanges();

                Console.WriteLine("Тестовые данные добавлены в базу данных!");
            }
            else
            {
                Console.WriteLine("База данных уже содержит данные.");
            }
        }
    }
}