using FFRC.Core.BE.Rankings;
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
        IDictionary<string, Ranking> GetPlayersRankingTable(string htmlCode);
    }
}
