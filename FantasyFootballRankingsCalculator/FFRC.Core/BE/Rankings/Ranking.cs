using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.BE.Rankings
{
    public class Ranking
    {
        public double Value { get; private set; }
        public bool NoRanking { get; private set; }

        public Ranking(double value)
        {
            Value = value;
            NoRanking = false;
        }

        public Ranking()
        {
            Value = 0;
            NoRanking = true;
        }

        public override string ToString()
        {
            return (NoRanking) ? "S.V." : Value.ToString();
        }
    }
}
