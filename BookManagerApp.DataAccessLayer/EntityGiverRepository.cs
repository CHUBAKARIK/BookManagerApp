using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookManagerApp.Model;

namespace BookManagerApp.DataAccessLayer
{
    public class EntityGiverRepository : IGiverRepository
    {
        private readonly BookDbcontext _context;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория для работы с дарителями через Entity Framework
        /// </summary>
        public EntityGiverRepository()
        {
            _context = new BookDbcontext();
        }

        /// <summary>
        /// Добавляет нового дарителя в базу данных
        /// </summary>
        /// <param name="item">Объект дарителя для добавления</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если item равен null</exception>
        /// <exception cref="DbUpdateException">Выбрасывается при ошибке сохранения в базу данных</exception>
        public void Add(Giver item)
        {
            _context.Givers.Add(item);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет дарителя из базы данных по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор дарителя для удаления</param>
        /// <remarks>
        /// Если даритель с указанным ID не найден, метод завершается без ошибок.
        /// Выполняет SQL запрос: DELETE FROM Givers WHERE ID = @id
        /// </remarks>
        public void Delete(int id)
        {
            var giver = _context.Givers.Find(id);

            if (giver != null)
            {
                _context.Givers.Remove(giver);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Получает всех дарителей из базы данных
        /// </summary>
        /// <returns>Массив всех дарителей</returns>
        /// <remarks>
        /// Возвращает пустой массив, если в базе данных нет дарителей.
        /// Выполняет SQL запрос: SELECT * FROM Givers
        /// </remarks>
        public Giver[] ReadAll()
        {
            return _context.Givers.ToArray();
        }

        /// <summary>
        /// Находит дарителя в базе данных по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор дарителя для поиска</param>
        /// <returns>Найденный даритель или null, если даритель не существует</returns>
        /// <remarks>
        /// Выполняет SQL запрос: SELECT * FROM Givers WHERE ID = @id
        /// </remarks>
        public Giver ReadById(int id)
        {
            return _context.Givers.Find(id);
        }

        /// <summary>
        /// Обновляет информацию о дарителе в базе данных
        /// </summary>
        /// <param name="item">Объект дарителя с обновленными данными</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если item равен null</exception>
        /// <exception cref="DbUpdateException">Выбрасывается при ошибке сохранения изменений</exception>
        /// <remarks>
        /// Даритель должен существовать в базе данных, иначе будет создана новая запись.
        /// Выполняет SQL запрос: UPDATE Givers SET ... WHERE ID = @ID
        /// </remarks>
        public void Update(Giver item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}