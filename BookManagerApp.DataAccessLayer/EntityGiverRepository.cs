using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;                  
using Microsoft.EntityFrameworkCore; 
using BookManagerApp.Model;
using BookManagerApp.DataAccessLayer;

namespace BookManagerApp.DataAccessLayer
{
    public class EntityGiverRepository : IGiverRepository
    {
        private readonly BookDbcontext _context;
        public EntityGiverRepository()
        {
            
            _context = new BookDbcontext();
        }
        public void Add(Giver item)
        {
            _context.Givers.Add(item);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            
            var giver = _context.Givers.Find(id);

            
            if (giver != null)
            {
                
                
                _context.Givers.Remove(giver);

                
                

                _context.SaveChanges();
            }


        }
        public Giver[] ReadAll()
        {
           
            
            // ToArray() - метод расширения из пространства имён System.Linq
            // Создаёт новый массив и копирует в него все элементы коллекции
            return _context.Givers.ToArray();
        }
        public Giver ReadById(int id)
        {
           
            // Find() - выполняет SQL запрос: SELECT * FROM Givers WHERE ID = @id
            
            return _context.Givers.Find(id);
        }
        public void Update(Giver item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
       


    }
}
