using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BookManagerApp.Model;


namespace BookManagerApp.Logic
{
    public class GiverManager
    {
        private List<Giver> _givers = new List<Giver> ();

        private int NextId = 1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="thing"></param>
        /// <param name="abilitiesofthething"></param>
        /// <param name="yearofcreation"></param>
        /// <exception cref="ArgumentException"></exception>
        // создание сущности
        public void AddGiver(string name,string thing,string abilitiesofthething, int yearofcreation,string team)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Даритель не может существовать без имени");
            if (string.IsNullOrWhiteSpace(thing))
                throw new ArgumentException("У каждого дариетля должна быть своя любимая вещь");
            if (string.IsNullOrWhiteSpace(abilitiesofthething))
                throw new ArgumentException("Любимая вещь обязательно должна давать какую то силу");
            if (string.IsNullOrWhiteSpace(team))
                throw new ArgumentException("Даритель умрет на просторах мира без команды");

            var giver = new Giver(NextId++,name,thing,abilitiesofthething,yearofcreation,team);
            _givers.Add(giver);
        }
        // удаление сущности
        public bool GiverToDelete(int Id)
        {
            var givertoremove = _givers.FirstOrDefault(x => x.Id == Id);
            if (givertoremove != null)
            {
                _givers.Remove(givertoremove);
                return true;
            }
            else
                return false;
                

        }
        public List<Giver> GetAllGivers()
        {
            return new List<Giver>(_givers);
        }
        public bool UpdateGiver(int id, string new_name, string new_thing, string new_abilitiesofthething, int new_yearofcreation, string new_team)
        {
            var givertoupdate = _givers.FirstOrDefault(x=>x.Id == id);
            if (givertoupdate != null)
            {
                if (!string.IsNullOrWhiteSpace(new_name))
                    givertoupdate.Name = new_name;
                if (!string.IsNullOrWhiteSpace(new_thing))
                    givertoupdate.Thing = new_thing;
                if (!string.IsNullOrWhiteSpace(new_abilitiesofthething))
                    givertoupdate.AbilitiesOfTheThing = new_abilitiesofthething;
                if (new_yearofcreation>0)
                    givertoupdate.YearOfCreation = new_yearofcreation;
                if (!string.IsNullOrWhiteSpace(new_team))
                    givertoupdate.AbilitiesOfTheThing = new_team;
                return true;
            }  
            return false;
        }
        public Dictionary<string,List<Giver>> GroupGiversByTeams()
        {
            return _givers.GroupBy(b=> b.Team)
                .ToDictionary(g=>g.Key,g=>g.ToList());
        }
        public List<Giver> GetThingsAfterTheYear(int yearofcreation)
        {
            return _givers.Where(b => b.YearOfCreation > yearofcreation).ToList();
        }
           
    }  
}
