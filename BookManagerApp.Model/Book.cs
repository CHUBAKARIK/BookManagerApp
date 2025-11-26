using System;
using System.Collections.Generic;
using System.Linq;
using BookManagerApp.Model;
namespace BookManagerApp.Model
{
    public class Book :IDomainObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AbilitiesOfTheBook { get; set; }
        public int Year { get; set; }
        


        // Конструктор для удобства
        public Book(int id, string title, string author,string abilitiesofthebook, int year)
        {
            Id = id;
            Title = title;
            Author = author;
            AbilitiesOfTheBook = abilitiesofthebook;
            Year = year;
           

        }
        public Book() { }

        // Для красивого отображения в консоли и др.
        public override string ToString()
        {
            return $"ID: {Id}, \"{Title}\" - {Author} ({Year}г., {AbilitiesOfTheBook})";
        }
    }
}
