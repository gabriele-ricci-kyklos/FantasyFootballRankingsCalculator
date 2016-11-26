using FFRC.Core.BE.Football;
using FFRC.Core.BE.Players;
using FFRC.Core.BE.Rankings;
using FFRC.Core.BLL.Rankings;
using FFRC.Core.Support;
using GenericCore.Support;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FFRC.Core.PianetaFantacalcio.BLL.Rankings
{
    public class PFRankingsBLL : RankingsBLL, IRankingsBLL
    {
        public PFRankingsBLL(string url)
            : base(url)
        {
        }

        public override int GetSelectedWeek()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HtmlCode);

            HtmlNode element =
                doc
                    .DocumentNode
                    .SelectByClass("li", "active")
                    .Where(x => x.InnerText.Contains("current"))
                    .FirstOrDefault();

            if (element.IsNull())
            {
                return 1;
            }

            string regex = "[0-9]+";
            Match match = Regex.Match(element.InnerText, regex);
            if (!match.Success)
            {
                return 1;
            }

            int num = 0;
            int.TryParse(match.Value, out num);
            return num;
        }

        protected override Ranking CalculateRanking(HtmlNode rankingNode)
        {
            rankingNode.AssertNotNull("rankingNode");

            string rawRanking = rankingNode.InnerText;

            string playerRanking =
                Regex
                    .Replace
                    (
                        rawRanking.Trim(),
                        @"\t|\n|\r",
                        ""
                    );

            if (playerRanking.Contains("s.v."))
            {
                return new Ranking();
            }

            playerRanking = playerRanking.Replace('.', ',');
            return new Ranking(Math.Round(playerRanking.ConvertTo<double>(), 1));
        }

        protected override int GetAssists(IList<HtmlNode> dataNodes)
        {
            dataNodes.AssertNotNull("dataNodes");

            if(dataNodes.Count < 4)
            {
                return 0;
            }

            HtmlNode assistNode = dataNodes[3];

            if(assistNode.IsNull())
            {
                return 0;
            }

            int assists = GetNumberFromNode(assistNode);
            return assists;
        }

        protected override Card? GetCardForPlayer(PlayerNode playerNode)
        {
            playerNode.AssertNotNull("playerNode");
            playerNode.NameNode.AssertNotNull("playerNode.NameNode");


            bool hasYellowCard = HasCard(playerNode.NameNode, Card.YellowCard);
            if (hasYellowCard)
            {
                return Card.YellowCard;
            }

            bool hasRedCard = HasCard(playerNode.NameNode, Card.RedCard);
            if (hasRedCard)
            {
                return Card.RedCard;
            }

            return null;
        }

        protected override IList<HtmlNode> GetDataNodes(PlayerNode playerNode)
        {
            IList<HtmlNode> dataNodes =
                playerNode
                    .Node
                    .ChildNodes
                    .SelectByClass("td", "gazzetta")
                    .OrderBy(x => x.Line)
                    .ToList();

            return dataNodes;
        }

        protected override IList<Goal> GetGoals(PlayerNode playerNode, IList<HtmlNode> dataNodes, IList<HtmlNode> penaltyNodes)
        {
            dataNodes.AssertNotNull("dataNodes");
            penaltyNodes.AssertNotNull("penaltyNodes");

            if (dataNodes.Count < 3)
            {
                return null;
            }

            if (penaltyNodes.Count < 4)
            {
                return null;
            }

            HtmlNode scoredGoalNode = dataNodes[0];
            HtmlNode concededGoalNode = dataNodes[1];
            HtmlNode ownGoalNode = dataNodes[2];

            HtmlNode missedPenaltyNode = penaltyNodes[0];
            HtmlNode savedPenaltyNode = penaltyNodes[1];
            HtmlNode scoredPenaltyNode = penaltyNodes[2];
            HtmlNode concededPenaltyNode = penaltyNodes[3];

            int missedPenalties = GetNumberFromNode(missedPenaltyNode);
            int savedPenalties = GetNumberFromNode(savedPenaltyNode);
            int scoredPenalties = GetNumberFromNode(scoredPenaltyNode);
            int concededPenalties = GetNumberFromNode(concededPenaltyNode);

            int scoredGoals = GetNumberFromNode(scoredGoalNode) - scoredPenalties;
            int concededGoals = GetNumberFromNode(concededGoalNode) - concededPenalties;
            int ownGoals = GetNumberFromNode(ownGoalNode);

            IList<Goal> goals = new List<Goal>();

            #region Goalkeeper

            #region Conceded Penalties

            if (playerNode.Position == Position.GoalKeeper && concededPenalties > 0)
            {
                for (int i = 0; i < concededPenalties; ++i)
                {
                    Goal goal =
                        new Goal
                        {
                            Conceded = true,
                            Missed = false,
                            Own = false,
                            Penalty = true,
                            Saved = false
                        };

                    goals.Add(goal);
                }
            }

            #endregion

            #region Conceded Goals

            if (playerNode.Position == Position.GoalKeeper && concededGoals > 0)
            {
                for (int i = 0; i < concededGoals; ++i)
                {
                    Goal goal =
                        new Goal
                        {
                            Conceded = true,
                            Missed = false,
                            Own = false,
                            Penalty = false,
                            Saved = false
                        };

                    goals.Add(goal);
                }
            }

            #endregion

            #endregion

            #region Saved Penalties

            if (savedPenalties > 0)
            {
                for (int i = 0; i < savedPenalties; ++i)
                {
                    Goal goal =
                        new Goal
                        {
                            Conceded = false,
                            Missed = false,
                            Own = false,
                            Penalty = true,
                            Saved = true
                        };

                    goals.Add(goal);
                }
            }

            #endregion

            #region Missed Penalties

            if (missedPenalties > 0)
            {
                for (int i = 0; i < missedPenalties; ++i)
                {
                    Goal goal =
                        new Goal
                        {
                            Conceded = false,
                            Missed = true,
                            Own = false,
                            Penalty = true,
                            Saved = false
                        };

                    goals.Add(goal);
                }
            }

            #endregion

            #region Scored Penalties

            if (scoredPenalties > 0)
            {
                for (int i = 0; i < scoredPenalties; ++i)
                {
                    Goal goal =
                        new Goal
                        {
                            Conceded = false,
                            Missed = false,
                            Own = false,
                            Penalty = true,
                            Saved = false
                        };

                    goals.Add(goal);
                }
            }

            #endregion

            #region Scored Goals

            if (scoredGoals > 0)
            {
                for (int i = 0; i < scoredGoals; ++i)
                {
                    Goal goal =
                        new Goal
                        {
                            Conceded = false,
                            Missed = false,
                            Own = false,
                            Penalty = false,
                            Saved = false
                        };

                    goals.Add(goal);
                }
            }

            #endregion

            #region Own Goals

            if (ownGoals > 0)
            {
                for (int i = 0; i < ownGoals; ++i)
                {
                    Goal goal =
                        new Goal
                        {
                            Conceded = false,
                            Missed = false,
                            Own = true,
                            Penalty = false,
                            Saved = false
                        };

                    goals.Add(goal);
                }
            }

            #endregion

            return goals;
        }

        protected override IList<HtmlNode> GetPenaltyNodes(PlayerNode playerNode)
        {
            IList<HtmlNode> penaltyNodes =
                playerNode
                    .Node
                    .ChildNodes
                    .SelectByClass("td", "rigori")
                    .OrderBy(x => x.Line)
                    .ToList();

            return penaltyNodes;
        }

        protected override IList<PlayerNode> GetPlayerListByPosition(HtmlDocument doc, string[] playerNameList, Position position)
        {
            doc.AssertNotNull("doc");
            playerNameList.AssertNotNull("playerNameList");

            string strPosition = null;

            switch (position)
            {
                case Position.GoalKeeper:
                    strPosition = "portiere-tabella";
                    break;
                case Position.Defender:
                    strPosition = "difensore-tabella";
                    break;
                case Position.Midfielder:
                    strPosition = "centrocampista-tabella";
                    break;
                case Position.Forward:
                    strPosition = "attaccante-tabella";
                    break;
            }

            IList<PlayerNode> playerList =
                doc
                    .DocumentNode
                    .SelectByClass
                    (
                        "tr",
                        strPosition
                    )
                    .Select
                    (x =>
                    {
                        HtmlNode nameNode =
                            x
                                .SelectByClass("td", "text-left")
                                .FirstOrDefault();

                        string playerName = GetPlayerName(nameNode);

                        return
                            new PlayerNode
                            {
                                Name = playerName,
                                NameNode = nameNode,
                                Node = x,
                                Position = position
                            };
                    }
                    )
                    .Where(x => IsPlayerNameListContainingPlayerName(playerNameList, x.Name))
                    .ToEmptyListIfNull();

            return playerList;
        }

        protected override HtmlNode GetRankingNode(PlayerNode playerNode)
        {
            HtmlNode gazzettaRanking =
                playerNode
                    .Node
                    .SelectByClass("td", "violettog")
                    .FirstOrDefault();

            return gazzettaRanking;
        }

        #region Private Methods

        private bool HasCard(HtmlNode nameNode, Card cardType)
        {
            nameNode.AssertNotNull("nameNode");
            return
                nameNode
                    .ChildNodes
                    .SelectByClass("span", (cardType == Card.YellowCard) ? "cart-giallo" : "cart-rosso")
                    .Any();
        }

        private string GetPlayerName(HtmlNode nameNode)
        {
            nameNode.AssertNotNull("nameNode");

            return
                Regex
                    .Replace
                    (
                        nameNode
                            .FirstChild
                            .InnerText
                            .Trim(),
                        @"\t|\n|\r",
                        ""
                    );
        }

        private bool IsPlayerNameListContainingPlayerName(string[] playerNameList, string playerName)
        {
            playerNameList.AssertNotNull("playerNameList");
            playerName.AssertNotNull("playerName");

            string playerNameInvariant =
                playerName
                    .Split(' ')
                    .FirstOrDefault()
                    .Trim()
                    .ToLowerInvariant();

            string[] playerNameInvariantList =
                playerNameList
                .Select(x => x.ToLowerInvariant())
                .ToArray();

            return playerNameInvariantList.Contains(playerNameInvariant);
        }

        private int GetNumberFromNode(HtmlNode node)
        {
            node.AssertNotNull("node");

            int number = 0;
            string strNumber =
                Regex
                    .Replace
                    (
                        node
                            .FirstChild
                            .InnerText
                            .Trim(),
                        @"\t|\n|\r",
                        ""
                    );

            int.TryParse(strNumber, out number);
            return number;
        }

        #endregion
    }
}
