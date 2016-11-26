using FFRC.Core.Support.Web;
using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.Support.BLL
{
    public abstract class BaseWebBLL : IBaseWebBLL
    {
        public string Url { get; private set; }
        public string HtmlCode { get; private set; }

        public BaseWebBLL(string url)
        {
            url.AssertNotNull("url");

            Url = url;
            HtmlCode = WebPageRetriever.RetrievePage(Url);
        }
    }
}
