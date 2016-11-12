using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.Rankings
{
    public interface IRankingsBLL
    {
        int GetSelectedWeek(string htmlCode);
        IDictionary<string, double> GetPlayersRankingTable(string htmlCode);
    }
}
