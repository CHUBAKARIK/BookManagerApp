using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerApp.Model;

namespace BookManagerApp.DataAccessLayer
{
    public class BookDbcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\treta\\Documents\\лабы по программированию\\1 курс\\BookManagerApp\\BookManagerApp.DataAccessLayer\\BookManagerDB.mdf\";Integrated Security=True");
            }
        }
        
        public DbSet<Book> Books { get; set; }

        
        public DbSet<Giver> Givers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
