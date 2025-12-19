

using System;                    
using System.Linq;               // Нужно для методов LINQ (Where, GroupBy, ToList)
using BookManagerApp.Model;      
using BookManagerApp.DataAccessLayer;  // Нужно для репозиториев


namespace BookManagerApp.Logic
{
    
    public class GiverManager : IGiverCRUD
    {
        /// <summary>
        /// ЗАМЕНЯЕМ старый List на репозиторий:
        /// </summary>
        
        private IGiverRepository _giverRepository;

        
        private IBookRepository _bookRepository;

        /// <summary>
        /// Конструктор класса - инициализирует репозитории
        /// </summary>
        /// <param name="bookManager">Менеджер книг для проверки существования книг</param>
        public GiverManager(IGiverRepository giverRepository, IBookRepository bookRepository)
        {
            _giverRepository = giverRepository;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Добавить дарителя
        /// </summary>
        /// <param name="name">Имя дарителя</param>
        /// <param name="bookId">ID книги дарителя</param>
        /// <param name="yearOfCreation">Год создания/очки силы</param>
        /// <param name="team">Команда дарителя</param>
        /// <exception cref="ArgumentException">Исключение при ошибках валидации</exception>
        public void AddGiver(string name, int bookId, int yearOfCreation, string team)
        {
            
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Даритель не может существовать без имени");

            
            if (!_bookRepository.Exists(bookId))
                throw new ArgumentException($"Книги с ID {bookId} не существует!");

            if (string.IsNullOrWhiteSpace(team))
                throw new ArgumentException("Даритель умрет на просторах мира без команды");

            
            var giver = new Giver
            {
                Name = name,
                BookId = bookId,
                YearOfCreation = yearOfCreation,
                Team = team
            };
                
            
            _giverRepository.Add(giver);
        }

        /// <summary>
        /// Удалить дарителя
        /// </summary>
        /// <param name="id">ID дарителя для удаления</param>
        /// <returns>true если даритель удален, false если не найден</returns>
        public bool DeleteGiver(int id)
        {
           
            if (id <= 0)
            {
                throw new ArgumentException("ID дарителя должен быть положительным числом");
            }

           
            _giverRepository.Delete(id);

          
            return true;
        }

        /// <summary>
        /// Получить всех дарителей
        /// </summary>
        /// <returns>Список всех дарителей из базы данных</returns>
        public Giver[] GetAllGivers()
        {
            // Вызываем метод ReadAll репозитория для получения всех дарителей из базы данных
            // Вместо старого кода: return new List<Giver>(_givers);
            // Метод возвращает массив Giver[]
            return _giverRepository.ReadAll();
        }

        /// <summary>
        /// Обновить дарителя
        /// </summary>
        /// <param name="id">ID дарителя для обновления</param>
        /// <param name="newName">Новое имя (если не null)</param>
        /// <param name="newBookId">Новый ID книги (если больше 0)</param>
        /// <param name="newYearOfCreation">Новые очки силы (если больше 0)</param>
        /// <param name="newTeam">Новая команда (если не null)</param>
        /// <returns>true если даритель обновлен, false если не найден</returns>
        /// <exception cref="ArgumentException">Исключение при ошибках валидации</exception>
        public bool UpdateGiver(int id, string newName, int newBookId, int newYearOfCreation, string newTeam)
        {
            
            if (id <= 0)
            {
                throw new ArgumentException("ID дарителя должен быть положительным числом");
            }

            
            var giverToUpdate = _giverRepository.ReadById(id);

            
            if (giverToUpdate != null)
            {
                
                if (!string.IsNullOrWhiteSpace(newName))
                    giverToUpdate.Name = newName;

                
                if (newBookId > 0 && !_bookRepository.Exists(newBookId))
                    throw new ArgumentException($"Книги с ID {newBookId} не существует!");

                
                if (newBookId > 0)
                    giverToUpdate.BookId = newBookId;

                
                if (newYearOfCreation > 0)
                    giverToUpdate.YearOfCreation = newYearOfCreation;

                
                if (!string.IsNullOrWhiteSpace(newTeam))
                    giverToUpdate.Team = newTeam;

                // Сохраняем изменения в базе данных через репозиторий
                _giverRepository.Update(giverToUpdate);
                return true;
            }
            return false; // Даритель не найден
        }

        
        
    }
}