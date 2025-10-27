using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BookManagerApp.Model;




public class Giver
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BookId { get; set; } // Храним ID книги вместо названия
    public int YearOfCreation { get; set; }
    public string Team { get; set; }

    public Giver(int id, string name, int bookId, int yearOfCreation, string team)
    {
        Id = id;
        Name = name;
        BookId = bookId;
        YearOfCreation = yearOfCreation;
        Team = team;
    }

    // Метод для получения книги (нужен доступ к BookManager)
   

    public override string ToString()
    {
        return $"ID - {Id}, Имя Дарителя - {Name}, " +
               $"ID книги - {BookId}, " +
               $"Год создания - {YearOfCreation}, Команда - {Team}";
    }
}