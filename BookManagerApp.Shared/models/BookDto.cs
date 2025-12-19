using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Shared.models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AbilitiesOfTheBook { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, \"{Title}\" - {Author} ({Year}г., {AbilitiesOfTheBook})";
        }
    }
}
