using FFRC.Core.BLL.Names;
using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRC.Core.PianetaFantacalcio.BLL.Names
{
    public class NamesBLL : INamesBLL
    {
        public string RetrievePlayerName(string tablePlayerName)
        {
            tablePlayerName.AssertNotNull("tablePlayerName");
            return 
                tablePlayerName
                    .Split(' ')
                    .FirstOrDefault()
                    .Trim()
                    .ToLowerInvariant();
        }
    }
}
