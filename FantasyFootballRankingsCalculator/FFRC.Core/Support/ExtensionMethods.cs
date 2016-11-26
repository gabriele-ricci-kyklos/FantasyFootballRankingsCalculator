using FFRC.Core.BE.Rankings;
using GenericCore.Support;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.Support
{
    public static class ExtensionMethods
    {
        public static IEnumerable<HtmlNode> SelectByClass(this HtmlNode documentNode, string descendantsName, string className)
        {
            return SelectByClass(documentNode, descendantsName, className.AsArray());
        }

        public static IEnumerable<HtmlNode> SelectByClass(this HtmlNode documentNode, string descendantsName, string[] classNames)
        {
            documentNode.AssertNotNull("documentNode");
            classNames.AssertNotNull("className");

            return
                documentNode
                    .Descendants(descendantsName)
                    .Where
                    (x =>
                        x.Attributes.Contains("class")
                        && classNames.Any(x.Attributes["class"].Value.Contains)
                    );
        }

        public static IEnumerable<HtmlNode> SelectByClass(this HtmlNodeCollection documentNodes, string descendantsName, string className)
        {
            return SelectByClass(documentNodes, descendantsName, className.AsArray());
        }

        public static IEnumerable<HtmlNode> SelectByClass(this HtmlNodeCollection documentNodes, string descendantsName, string[] classNames)
        {
            documentNodes.AssertNotNull("documentNodes");
            classNames.AssertNotNull("classNames");

            return
                documentNodes
                    .Where
                    (x =>
                        x.Name == descendantsName
                        && x.Attributes.Contains("class")
                        && !x.Attributes["class"].Value.Split(' ').Except(classNames).Any()
                    );
        }

        public static PlayerStatsCollection ToPlayerStatsCollection(this IEnumerable<PlayerStats> list)
        {
            list.AssertNotNull("list");
            return new PlayerStatsCollection(list);
        }
    }
}
