using FFRC.Core.BE.Rankings;
using FFRC.Core.Support.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.Rankings
{
    public interface IRankingsBLL : IBaseWebBLL
    {
        int GetSelectedWeek();
        PlayerStatsCollection GetPlayersRankingsByNameList(string[] playerNameList);
    }
}
