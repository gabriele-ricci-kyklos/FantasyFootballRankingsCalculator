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

        public IDictionary<string, double> GetPlayersRankingTable(string htmlCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlCode);

            var playerRows =
                doc
                    .DocumentNode
                    .SelectByClass("tr", new string[] { "portiere-tabella", "difensore-tabella", "centrocampista-tabella", "attaccante-tabella" });

            IDictionary<string, double> playersMap = new Dictionary<string, double>();
            foreach (HtmlNode node in playerRows)
            {
                HtmlNode playerNode = 
                    node
                        .SelectByClass("td", "text-left")
                        .FirstOrDefault();

                HtmlNode rankingVG =
                    node
                        .SelectByClass("td", "fantag")
                        .FirstOrDefault();

                if (playerNode.IsNull() || rankingVG.IsNull())
                {
                    continue;
                }

                string playerName =
                    Regex
                        .Replace
                        (
                            playerNode
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

                playersMap
                    .Add
                    (
                        playerName,
                        Math
                            .Round
                            (
                                rankingVG
                                    .InnerText
                                    .Trim()
                                    .ConvertTo<double>(),
                                1
                            )
                    );
            }

            return playersMap;
        }
    }
}
