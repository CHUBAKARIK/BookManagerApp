using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Logic
{
    public interface IGiverBUSINESS
    {
        Dictionary<string,List<Giver>> GroupGiversByTeams();
        Giver[] GetGiversWithBooksAfterYear(int year);
        string GetGiverBookInfo(int giverId);
    }
}
