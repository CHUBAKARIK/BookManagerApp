using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Logic
{
    public interface IGiverCRUD
    {
        void AddGiver(string name, int bookId, int yearOfCreation, string team);
        bool DeleteGiver(int id);
        Giver[] GetAllGivers();
        bool UpdateGiver(int id, string newName, int newBookId, int newYearOfCreation, string newTeam);
        
    }
}
