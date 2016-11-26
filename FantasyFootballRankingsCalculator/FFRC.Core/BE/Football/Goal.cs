using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.BE.Football
{
    public class Goal
    {
        public bool Conceded { get; set; }
        public bool Own { get; set; }
        public bool Penalty { get; set; }
        public bool Missed { get; set; }
        public bool Saved { get; set; }
    }
}
