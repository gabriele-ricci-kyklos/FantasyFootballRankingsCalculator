using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.Support.Web
{
    internal static class WebPageRetriever
    {
        public static string RetrievePage(string pageUrl)
        {
            string htmlCode = null;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(pageUrl);
            }

            return htmlCode;
        }
    }
}
