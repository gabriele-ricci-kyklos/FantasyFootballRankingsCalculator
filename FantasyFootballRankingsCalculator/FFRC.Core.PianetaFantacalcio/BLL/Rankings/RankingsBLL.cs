using FFRC.Core.BE.Rankings;
using FFRC.Core.Rankings;
using FFRC.Core.Support;
using GenericCore.Support;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FFRC.Core.PianetaFantacalcio.BLL.Rankings
{
    public class RankingsBLL : IRankingsBLL
    {
        public int GetSelectedWeek(string htmlCode)
        {
            htmlCode.AssertNotNull("htmlCode");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlCode);

            HtmlNode element =
                doc
                    .DocumentNode
                    .SelectByClass("li", "active")
                    .Where(x => x.InnerText.Contains("current"))
                    .FirstOrDefault();

            if(element.IsNull())
            {
                return 1;
            }

            string regex = "[0-9]+";
            Match match = Regex.Match(element.InnerText, regex);
            if(!match.Success)
            {
                return 1;
            }

            int num = 0;
            int.TryParse(match.Value, out num);
            return num;
        }

        public IDictionary<string, Ranking> GetPlayersRankingTable(string htmlCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlCode);

            var playerRows =
                doc
                    .DocumentNode
                    .SelectByClass("tr", new string[] { "portiere-tabella", "difensore-tabella", "centrocampista-tabella", "attaccante-tabella" });

            IDictionary<string, Ranking> playersMap = new Dictionary<string, Ranking>();
            foreach (HtmlNode node in playerRows)
            {
                HtmlNode nameNode = 
                    node
                        .SelectByClass("td", "text-left")
                        .FirstOrDefault();

                HtmlNode rankingG =
                    node
                        .SelectByClass("td", "violettog")
                        .FirstOrDefault();

                if (nameNode.IsNull() || rankingG.IsNull())
                {
                    continue;
                }

                string playerName =
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

                if(playerName.IsNull())
                {
                    continue;
                }

                Ranking ranking = CalculateRanking(rankingG.InnerText);
                playersMap.Add(playerName, ranking);
            }

            return playersMap;
        }

        private Ranking CalculateRanking(string rawRanking)
        {
            rawRanking.AssertNotNull("rawRanking");

            string playerRanking =
                Regex
                    .Replace
                    (
                        rawRanking.Trim(),
                        @"\t|\n|\r",
                        ""
                    );

            if(playerRanking.Contains("s.v."))
            {
                return new Ranking();
            }

            playerRanking = playerRanking.Replace('.', ',');
            return new Ranking(Math.Round(playerRanking.ConvertTo<double>(), 1));
        }
    }
}
