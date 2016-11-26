using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GenericCore.Collections.Dictionaries;
using System.Collections.ObjectModel;

namespace FFRC.Core.BE.Rankings
{
    public class PlayerStatsCollection : KeyedCollection<string, PlayerStats>
    {
        public PlayerStatsCollection()
            : base()
        {
        }

        public PlayerStatsCollection(IEnumerable<PlayerStats> list)
            : base()
        {
            list.AssertNotNull("list");
            list.ForEach(x => Add(x));
        }

        protected override string GetKeyForItem(PlayerStats item)
        {
            return item.PlayerName;
        }
    }
}
