using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Model
{
    public class Giver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thing { get; set; }
        
        public int YearOfCreation { get; set; }
        public string AbilitiesOfTheThing { get; set; }
        public string Team {  get; set; }
        public Giver(int id, string name , string thing, string abilitiesofthething,int yearofcreation,string team)
        {
            Id = id;
            Name = name;
            Thing = thing;           
            AbilitiesOfTheThing = abilitiesofthething;
            YearOfCreation = yearofcreation;
            Team = team;
        }
        public override string ToString()
        {
            return $"ID - {Id}, Имя Дарителя - {Name}, Любимая вещь - {Thing},Способности вещи -{AbilitiesOfTheThing}, Год создания вещи - {YearOfCreation}, Команда -{Team} ";
        }
    }
}
    
