using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Shared.models
{
    public class GiverDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }
        public int YearOfCreation { get; set; }
        public string Team { get; set; }
    }
}
