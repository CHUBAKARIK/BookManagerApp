using BookManagerApp.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Logic
{
    public class GiverBusinessService : IGiverBUSINESS
    {
        private readonly IGiverRepository _giverRepository;
        private readonly IBookRepository _bookRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="giverRepository"></param>
        /// <param name="bookRepository"></param>
        public GiverBusinessService(IGiverRepository giverRepository, IBookRepository bookRepository)
        {
            _giverRepository = giverRepository;
            _bookRepository = bookRepository;
        }
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<Giver>> GroupGiversByTeams()


        {
            
            var givers = _giverRepository.ReadAll();

            
            return givers.GroupBy(g => g.Team)
                         .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Сортировка по году/очкам силы
        /// </summary>
        /// <param name="year">Год/очки силы для сравнения</param>
        /// <returns>Список дарителей с очками силы больше указанного значения</returns>
        public Giver[] GetGiversWithBooksAfterYear(int year)
        {
            // Получаем всех дарителей из базы данных через репозиторий
            var givers = _giverRepository.ReadAll();

            
            return givers.Where(g => g.YearOfCreation > year).ToArray();
        }

        /// <summary>
        /// Метод для получения информации о книге дарителя
        /// </summary>
        /// <param name="giverId">ID дарителя</param>
        /// <returns>Информация о книге дарителя</returns>
        public string GetGiverBookInfo(int giverId)
        {
            // Проверяем валидность ID дарителя
            if (giverId <= 0)
            {
                return "Неверный ID дарителя";
            }

            
            var giver = _giverRepository.ReadById(giverId);

            if (giver != null)
            {
                
                var book = _bookRepository.ReadById(giver.BookId);

                
                return book != null ? $"{book.Title} ({book.AbilitiesOfTheBook})" : "Книга не найдена";
            }
            return "Даритель не найден"; // Если даритель не найден
        }
    }
}
