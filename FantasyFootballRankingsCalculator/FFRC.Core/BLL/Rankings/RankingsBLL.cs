using FFRC.Core.BE.Football;
using FFRC.Core.BE.Players;
using FFRC.Core.BE.Rankings;
using FFRC.Core.Support.BLL;
using GenericCore.Support;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.BLL.Rankings
{
    public abstract class RankingsBLL : BaseWebBLL, IRankingsBLL
    {
        public RankingsBLL(string url)
            : base(url)
        {
        }

        public abstract int GetSelectedWeek();

        protected abstract IList<PlayerNode> GetPlayerListByPosition(HtmlDocument doc, string[] playerNameList, Position position);
        protected abstract HtmlNode GetRankingNode(PlayerNode playerNode);
        protected abstract IList<HtmlNode> GetDataNodes(PlayerNode playerNode);
        protected abstract IList<HtmlNode> GetPenaltyNodes(PlayerNode playerNode);
        protected abstract IList<Goal> GetGoals(PlayerNode playerNode, IList<HtmlNode> dataNodes, IList<HtmlNode> penaltyNodes);
        protected abstract Card? GetCardForPlayer(PlayerNode playerNode);
        protected abstract int GetAssists(IList<HtmlNode> dataNodes);
        protected abstract Ranking CalculateRanking(HtmlNode rankingNode);

        public PlayerStatsCollection GetPlayersRankingsByNameList(string[] playerNameList)
        {
            playerNameList.AssertNotNull("playerNameList");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HtmlCode);

            IList<PlayerNode> keeperRows = GetPlayerListByPosition(doc, playerNameList, Position.GoalKeeper);
            IList<PlayerNode> defenderRows = GetPlayerListByPosition(doc, playerNameList, Position.Defender);
            IList<PlayerNode> midfielderRows = GetPlayerListByPosition(doc, playerNameList, Position.Midfielder);
            IList<PlayerNode> forwardRows = GetPlayerListByPosition(doc, playerNameList, Position.Forward);

            IList<PlayerNode> allPlayerRows =
                keeperRows
                    .Concat(defenderRows)
                    .Concat(midfielderRows)
                    .Concat(forwardRows)
                    .ToList();

            PlayerStatsCollection playerStats = new PlayerStatsCollection();
            foreach (PlayerNode player in allPlayerRows)
            {
                PlayerStats stats = CalculatePlayerStats(player);

                if (stats.IsNull())
                {
                    continue;
                }

                playerStats.Add(stats);
            }

            return playerStats;
        }

        private PlayerStats CalculatePlayerStats(PlayerNode playerNode)
        {
            HtmlNode rankingNode = GetRankingNode(playerNode);

            IList<HtmlNode> dataNodes =
                GetDataNodes(playerNode)
                    .ToEmptyListIfNull();

            IList<HtmlNode> penaltyNodes = GetPenaltyNodes(playerNode)
                .ToEmptyListIfNull();

            IList<Goal> goals =
                GetGoals(playerNode, dataNodes, penaltyNodes)
                    .ToEmptyListIfNull();

            Card? card = GetCardForPlayer(playerNode);
            Ranking ranking = CalculateRanking(rankingNode);
            int assists = GetAssists(dataNodes);

            PlayerStats stats =
                new PlayerStats
                {
                    Assists = assists,
                    Card = card,
                    Goals = goals,
                    PlayerName = playerNode.Name,
                    Ranking = ranking
                };

            return stats;
        }
    }
}
