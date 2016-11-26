using FFRC.Core.BE.Football;
using FFRC.Core.BE.Players;
using FFRC.Core.BE.Rankings;
using FFRC.Core.Support;
using FFRC.Core.Support.BLL;
using GenericCore.Support;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FFRC.Core.PianetaFantacalcio.BLL.Rankings
{
    public class oldRankingsBLL// : BaseWebBLL, IRankingsBLL
    {
        //public oldRankingsBLL(string url)
        //    : base(url)
        //{
        //}

        //public int GetSelectedWeek()
        //{
        //    HtmlDocument doc = new HtmlDocument();
        //    doc.LoadHtml(HtmlCode);

        //    HtmlNode element =
        //        doc
        //            .DocumentNode
        //            .SelectByClass("li", "active")
        //            .Where(x => x.InnerText.Contains("current"))
        //            .FirstOrDefault();

        //    if(element.IsNull())
        //    {
        //        return 1;
        //    }

        //    string regex = "[0-9]+";
        //    Match match = Regex.Match(element.InnerText, regex);
        //    if(!match.Success)
        //    {
        //        return 1;
        //    }

        //    int num = 0;
        //    int.TryParse(match.Value, out num);
        //    return num;
        //}

        //public PlayerStatsCollection GetPlayersRankingsByNameList(string[] playerNameList)
        //{
        //    playerNameList.AssertNotNull("playerNameList");

        //    HtmlDocument doc = new HtmlDocument();
        //    doc.LoadHtml(HtmlCode);

        //    IList<PlayerNode> keeperRows = GetPlayerListByPosition(doc, playerNameList, Position.GoalKeeper);
        //    IList<PlayerNode> defenderRows = GetPlayerListByPosition(doc, playerNameList, Position.Defender);
        //    IList<PlayerNode> midfielderRows = GetPlayerListByPosition(doc, playerNameList, Position.Midfielder);
        //    IList<PlayerNode> forwardRows = GetPlayerListByPosition(doc, playerNameList, Position.Forward);

        //    IList<PlayerNode> allPlayerRows = 
        //        keeperRows
        //            .Concat(defenderRows)
        //            .Concat(midfielderRows)
        //            .Concat(forwardRows)
        //            .ToList();

        //    PlayerStatsCollection playerStats = new PlayerStatsCollection();
        //    foreach (PlayerNode player in allPlayerRows)
        //    {
        //        HtmlNode gazzettaRanking =
        //            player
        //                .Node
        //                .SelectByClass("td", "violettog")
        //                .FirstOrDefault();

        //        IList<HtmlNode> dataNodes =
        //            player
        //                .Node
        //                .ChildNodes
        //                .SelectByClass("td", "gazzetta")
        //                .OrderBy(x => x.Line)
        //                .ToList();

        //        IList<HtmlNode> penaltyNodes =
        //            player
        //                .Node
        //                .ChildNodes
        //                .SelectByClass("td", "rigori")
        //                .OrderBy(x => x.Line)
        //                .ToList();

        //        PlayerStats stats = CalculatePlayerStats(player.Name, player.NameNode, gazzettaRanking, dataNodes, penaltyNodes);

        //        if (stats.IsNull())
        //        {
        //            continue;
        //        }

        //        playerStats.Add(stats);
        //    }

        //    return playerStats;
        //}

        //private IList<PlayerNode> GetPlayerListByPosition(HtmlDocument doc, string[] playerNameList, Position position)
        //{
        //    doc.AssertNotNull("doc");
        //    playerNameList.AssertNotNull("playerNameList");

        //    string strPosition = null;

        //    switch(position)
        //    {
        //        case Position.GoalKeeper:
        //            strPosition = "portiere-tabella";
        //            break;
        //        case Position.Defender:
        //            strPosition = "difensore-tabella";
        //            break;
        //        case Position.Midfielder:
        //            strPosition = "centrocampista-tabella";
        //            break;
        //        case Position.Forward:
        //            strPosition = "attaccante-tabella";
        //            break;
        //    }

        //    IList<PlayerNode> playerList =
        //        doc
        //            .DocumentNode
        //            .SelectByClass
        //            (
        //                "tr",
        //                strPosition
        //            )
        //            .Select
        //            (x =>
        //                {
        //                    HtmlNode nameNode =
        //                        x
        //                            .SelectByClass("td", "text-left")
        //                            .FirstOrDefault();

        //                    string playerName = GetPlayerName(nameNode);

        //                    return
        //                        new PlayerNode
        //                        {
        //                            Name = playerName,
        //                            NameNode = nameNode,
        //                            Node = x,
        //                            Position = position
        //                        };
        //                }
        //            )
        //            .Where(x => IsPlayerNameListContainingPlayerName(playerNameList, x.Name))
        //            .ToEmptyListIfNull();

        //    return playerList;
        //}

        //private PlayerStats CalculatePlayerStats(string name, HtmlNode nameNode, HtmlNode rankingNode, IList<HtmlNode> dataNodes, IList<HtmlNode> penaltyNodes)
        //{
        //    name.AssertNotNull("name");
        //    nameNode.AssertNotNull("nameNode");
        //    rankingNode.AssertNotNull("rankingNode");
        //    dataNodes.AssertNotNull("dataNodes");
        //    penaltyNodes.AssertNotNull("penaltyNodes");

        //    if (dataNodes.Count != 4)
        //    {
        //        return null;
        //    }

        //    if (penaltyNodes.Count != 4)
        //    {
        //        return null;
        //    }

        //    HtmlNode scoredGoalNode = dataNodes[0];
        //    HtmlNode concededGoalNode = dataNodes[1];
        //    HtmlNode ownGoalNode = dataNodes[2];
        //    HtmlNode assistsNode = dataNodes[3];

        //    Card? card = GetCardForPlayer(nameNode);
        //    Ranking ranking = CalculateRanking(rankingNode.InnerText);
        //    int assists = GetAssists(assistsNode);

        //    PlayerStats stats =
        //        new PlayerStats
        //        {
        //            Assists = assists,
        //            Card = card,
        //            Goals = null,
        //            PlayerName = name,
        //            Ranking = ranking
        //        };

        //    return stats;
        //}

        //private int GetAssists(HtmlNode assistNode)
        //{
        //    assistNode.AssertNotNull("assistNode");

        //    int assists = 0;
        //    string numberOfAssists =
        //        Regex
        //            .Replace
        //            (
        //                assistNode
        //                    .FirstChild
        //                    .InnerText
        //                    .Trim(),
        //                @"\t|\n|\r",
        //                ""
        //            );

        //    int.TryParse(numberOfAssists, out assists);
        //    return assists;
        //}

        //private string GetPlayerName(HtmlNode nameNode)
        //{
        //    nameNode.AssertNotNull("nameNode");

        //    return
        //        Regex
        //            .Replace
        //            (
        //                nameNode
        //                    .FirstChild
        //                    .InnerText
        //                    .Trim(),
        //                @"\t|\n|\r",
        //                ""
        //            );
        //}

        //private Ranking CalculateRanking(string rawRanking)
        //{
        //    rawRanking.AssertNotNull("rawRanking");

        //    string playerRanking =
        //        Regex
        //            .Replace
        //            (
        //                rawRanking.Trim(),
        //                @"\t|\n|\r",
        //                ""
        //            );

        //    if(playerRanking.Contains("s.v."))
        //    {
        //        return new Ranking();
        //    }

        //    playerRanking = playerRanking.Replace('.', ',');
        //    return new Ranking(Math.Round(playerRanking.ConvertTo<double>(), 1));
        //}

        //private Card? GetCardForPlayer(HtmlNode nameNode)
        //{
        //    nameNode.AssertNotNull("nameNode");

        //    bool hasYellowCard = HasCard(nameNode, Card.YellowCard);
        //    if(hasYellowCard)
        //    {
        //        return Card.YellowCard;
        //    }

        //    bool hasRedCard = HasCard(nameNode, Card.RedCard);
        //    if (hasRedCard)
        //    {
        //        return Card.RedCard;
        //    }

        //    return null;
        //}

        //private bool HasCard(HtmlNode nameNode, Card cardType)
        //{
        //    nameNode.AssertNotNull("nameNode");
        //    return 
        //        nameNode
        //            .ChildNodes
        //            .SelectByClass("span", (cardType == Card.YellowCard) ? "cart-giallo" : "cart-rosso")
        //            .Any();
        //}

        //private bool IsPlayerNameListContainingPlayerName(string[] playerNameList, string playerName)
        //{
        //    playerNameList.AssertNotNull("playerNameList");
        //    playerName.AssertNotNull("playerName");

        //    string playerNameInvariant =
        //        playerName
        //            .Split(' ')
        //            .FirstOrDefault()
        //            .Trim()
        //            .ToLowerInvariant();

        //    string[] playerNameInvariantList = 
        //        playerNameList
        //        .Select(x => x.ToLowerInvariant())
        //        .ToArray();

        //    return playerNameInvariantList.Contains(playerNameInvariant);
        //}
    }
}

