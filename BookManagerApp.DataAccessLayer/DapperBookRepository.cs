using BookManagerApp.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BookManagerApp.DataAccessLayer
{
    public class DapperBookRepository : IBookRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Устанавливаем строку подключения к базе данных
        /// </summary>
        public DapperBookRepository()
        {
            
            
            _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\treta\\Documents\\лабы по программированию\\1 курс\\BookManagerApp\\BookManagerApp.DataAccessLayer\\BookManagerDB.mdf\";Integrated Security=True";
        }
        /// <summary>
        /// метод добавления
        /// </summary>
        /// <param name="item"></param>
        public void Add(Book item)
        {
            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                // @Title, @Author и т.д. - параметры, которые будут заменены значениями из объекта
                string query = @"INSERT INTO Books (Title, Author, AbilitiesOfTheBook, Year) 
                               VALUES (@Title, @Author, @AbilitiesOfTheBook, @Year)";

               
                db.Execute(query, item);
            }
            
        }

        /// <summary>
        /// Реализация метода Delete из интерфейса IBookRepository
        /// </summary>
        /// <param name="id"></param>
        
        public void Delete(int id)
        {
       
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
               
                string query = "DELETE FROM Books WHERE ID = @Id";

                
                db.Execute(query, new { Id = id });
            }
        }

        /// <summary>
        /// Реализация метода ReadAll из интерфейса IBookRepository
        /// </summary>
        /// <returns></returns>
        
        public Book[] ReadAll()
        {
            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Books";

                
                return db.Query<Book>(query).ToArray();
            }
        }

        /// <summary>
        /// Реализация метода ReadById из интерфейса IBookRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public Book ReadById(int id)
        {
           
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
               
                string query = "SELECT * FROM Books WHERE ID = @Id";

               

                return db.QueryFirstOrDefault<Book>(query, new { Id = id });
            }
        }

        /// <summary>
        /// Реализация метода Update из интерфейса IBookRepository
        /// </summary>
        /// <param name="item"></param>
       
        public void Update(Book item)
        {
            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
               
                string query = @"UPDATE Books 
                               SET Title = @Title, Author = @Author, 
                                   AbilitiesOfTheBook = @AbilitiesOfTheBook, Year = @Year 
                               WHERE ID = @ID";

                
                db.Execute(query, item);
            }
        }


        /// <summary>
        /// Реализация метода BookExists из интерфейса IBookRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
           
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                string query = "SELECT COUNT(1) FROM Books WHERE ID = @Id";

                
                return db.ExecuteScalar<int>(query, new { Id = id }) > 0;
            }

        }
    }
}
