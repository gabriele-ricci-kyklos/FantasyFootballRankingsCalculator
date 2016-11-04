using FFRC.Core.BLL;
using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FFRC.Core.PianetaFantacalcio.BLL.RankingsBLL
{
    public class RankingsBLL : IRankingsBLL
    {
        public int GetSelectedWeek(string htmlCode)
        {
            htmlCode.AssertNotNull("htmlCode");
            string regex = "^<ul class=\"pagination-round\">(?:.+?)<li class=\"active\"><a href=\"(?:.+?)\">(.+?)\\s<span class=\"(?:.+?)\">\\(current\\)<\\/span><\\/a><\\/li><\\/ul>";

            htmlCode = htmlCode.Replace("\r\n", string.Empty);
            htmlCode = htmlCode.Replace("\n", string.Empty);

            Match match = Regex.Match(htmlCode, regex, RegexOptions.IgnoreCase);
            if(match.Groups.Count == 0)
            {
                return 1;
            }

            Group g = match.Groups[1];
            if(g.Captures.Count == 0)
            {
                return 1;
            }

            return int.Parse(g.Captures[0].Value);
        }
    }
}
