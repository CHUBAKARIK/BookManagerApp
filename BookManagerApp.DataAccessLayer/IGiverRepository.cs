using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.DataAccessLayer
{
    public interface IGiverRepository: IReadRepository<Giver>, IWriteRepository<Giver>
    {
        
    }
}
