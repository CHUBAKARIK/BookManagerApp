using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Model
{
    public class GiverDisplay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAbility { get; set; }
        public int PowerCount { get; set; }
        public string Team { get; set; }
    }
}
