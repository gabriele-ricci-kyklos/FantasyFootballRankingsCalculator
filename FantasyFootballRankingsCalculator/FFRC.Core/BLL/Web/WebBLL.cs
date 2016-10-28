using FFRC.Core.Support.BLL;
using FFRC.Core.Support.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.BLL.Web
{
    public class WebBLL : BaseBLL, IWebBLL
    {
        public string RetrievePage(string pageUrl)
        {
            return WebPageRetriever.RetrievePage(pageUrl);
        }
    }
}
