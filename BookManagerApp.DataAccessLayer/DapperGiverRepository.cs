using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BookManagerApp.DataAccessLayer
{
    public class DapperGiverRepository: IGiverRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// для строки подключения
        /// </summary>
        public DapperGiverRepository()
        {
            
            _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\treta\\Documents\\лабы по программированию\\1 курс\\BookManagerApp\\BookManagerApp.DataAccessLayer\\BookManagerDB.mdf\";Integrated Security=True";
        }

        /// <summary>
        /// Реализация метода Add из интерфейса IGiverRepository
        /// </summary>
        /// <param name="item"></param>
        
        public void Add(Giver item)
        {
            // using - автоматическое управление соединением с базой данных
            // Создаёт блок кода, после выполнения которого соединение автоматически закрывается
            // IDbConnection - интерфейс для работы с соединением к БД
            // new SqlConnection(_connectionString) - создаём новое соединение с SQL Server
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // SQL запрос для вставки новой записи в таблицу Givers
                // INSERT INTO Givers - команда вставки в таблицу Givers
                // (Name, BookId, YearOfCreation, Team) - перечисляем столбцы для вставки
                // VALUES (@Name, @BookId, @YearOfCreation, @Team) - значения для вставки
                // @Name, @BookId и т.д. - параметры, которые будут заменены значениями из объекта
                string query = @"INSERT INTO Givers (Name, BookId, YearOfCreation, Team) 
                               VALUES (@Name, @BookId, @YearOfCreation, @Team)";

                // db.Execute(query, item) - выполняет SQL запрос через Dapper
               
                db.Execute(query, item);
            }
            // После выхода из блока using соединение автоматически закрывается
        }

        /// <summary>
        /// Реализация метода Delete из интерфейса IGiverRepository
        /// </summary>
        /// <param name="id"></param>
        
        public void Delete(int id)
        {
            // using - автоматическое управление соединением
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                
                string query = "DELETE FROM Givers WHERE ID = @Id";

               
                db.Execute(query, new { Id = id });
            }
        }

        /// <summary>
        /// Реализация метода ReadAll из интерфейса IGiverRepository
        /// </summary>
        /// <returns></returns>
        
        public Giver[] ReadAll()
        {
            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                string query = "SELECT * FROM Givers";

                
                return db.Query<Giver>(query).ToArray();
            }
        }

        /// <summary>
        /// Реализация метода ReadById из интерфейса IGiverRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public Giver ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                string query = "SELECT * FROM Givers WHERE ID = @Id";

                
                return db.QueryFirstOrDefault<Giver>(query, new { Id = id });
            }
        }

        /// <summary>
        /// Реализация метода Update из интерфейса IGiverRepository
        /// </summary>
        /// <param name="item"></param>
       
        public void Update(Giver item)
        {
            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                string query = @"UPDATE Givers 
                               SET Name = @Name, BookId = @BookId, 
                                   YearOfCreation = @YearOfCreation, Team = @Team 
                               WHERE ID = @ID";

                
                db.Execute(query, item);
            }
        }
    }
}
