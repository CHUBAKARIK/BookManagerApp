using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BookManagerApp.Model;


namespace BookManagerApp.Logic
{
    public class GiverManager
    {
        private List<Giver> _givers = new List<Giver>();
        private int _nextId = 1;
        private BookManager _bookManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookManager"></param>
        public GiverManager(BookManager bookManager)
        {
            _bookManager = bookManager;
        }
        /// <summary>
        /// добавить дарителя
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bookId"></param>
        /// <param name="yearOfCreation"></param>
        /// <param name="team"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddGiver(string name, int bookId, int yearOfCreation, string team)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Даритель не может существовать без имени");

            // ПРОВЕРКА: существует ли книга с таким ID
            if (!_bookManager.BookExists(bookId))
                throw new ArgumentException($"Книги с ID {bookId} не существует!");

            if (string.IsNullOrWhiteSpace(team))
                throw new ArgumentException("Даритель умрет на просторах мира без команды");

            var giver = new Giver(_nextId++, name, bookId, yearOfCreation, team);
            _givers.Add(giver);
        }
        /// <summary>
        /// удалить дарителя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteGiver(int id)
        {
            var giverToRemove = _givers.FirstOrDefault(x => x.Id == id);
            if (giverToRemove != null)
            {
                _givers.Remove(giverToRemove);
                return true;
            }
            return false;
        }
        /// <summary>
        /// получить всех дарителей
        /// </summary>
        /// <returns></returns>
        public List<Giver> GetAllGivers()
        {
            return new List<Giver>(_givers);
        }
        /// <summary>
        /// обновить дарителя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        /// <param name="newBookId"></param>
        /// <param name="newYearOfCreation"></param>
        /// <param name="newTeam"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool UpdateGiver(int id, string newName, int newBookId, int newYearOfCreation, string newTeam)
        {
            var giverToUpdate = _givers.FirstOrDefault(x => x.Id == id);
            if (giverToUpdate != null)
            {
                if (!string.IsNullOrWhiteSpace(newName))
                    giverToUpdate.Name = newName;

                // ПРОВЕРКА: существует ли новая книга
                if (newBookId > 0 && !_bookManager.BookExists(newBookId))
                    throw new ArgumentException($"Книги с ID {newBookId} не существует!");

                if (newBookId > 0)
                    giverToUpdate.BookId = newBookId;

                if (newYearOfCreation > 0)
                    giverToUpdate.YearOfCreation = newYearOfCreation;

                if (!string.IsNullOrWhiteSpace(newTeam))
                    giverToUpdate.Team = newTeam;

                return true;
            }
            return false;
        }
        /// <summary>
        /// группировка по командам
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<Giver>> GroupGiversByTeams()
        {
            return _givers.GroupBy(g => g.Team)
                         .ToDictionary(g => g.Key, g => g.ToList());
        }
        /// <summary>
        /// сортировка по году
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<Giver> GetGiversWithBooksAfterYear(int year)
        {
            return _givers.Where(g => g.YearOfCreation > year).ToList();
        }

        /// <summary>
        /// Метод для получения информации о книге дарителя
        /// </summary>
        /// <param name="giverId"></param>
        /// <returns></returns>
        public string GetGiverBookInfo(int giverId)
        {
            var giver = _givers.FirstOrDefault(g => g.Id == giverId);
            if (giver != null)
            {
                var book = _bookManager.GetBookById(giver.BookId);
                return book != null ? $"{book.Title} ({book.AbilitiesOfTheBook})" : "Книга не найдена";
            }
            return "Даритель не найден";
        }
    }
}
