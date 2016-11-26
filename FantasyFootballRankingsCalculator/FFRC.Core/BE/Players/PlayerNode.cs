using FFRC.Core.BE.Football;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.BE.Players
{
    public class PlayerNode
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public HtmlNode Node { get; set; }
        public HtmlNode NameNode { get; set; }
    }
}
