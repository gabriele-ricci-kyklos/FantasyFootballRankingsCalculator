using FFRC.Core.BE.Football;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.BE.Rankings
{
    public class PlayerStats
    {
        public string PlayerName { get; set; }
        public IList<Goal> Goals { get; set; }
        public int Assists { get; set; }
        public Card? Card { get; set; }
        public Ranking Ranking { get; set; }
    }
}
