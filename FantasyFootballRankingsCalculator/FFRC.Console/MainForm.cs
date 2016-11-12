using FFRC.Core.BLL.Names;
using FFRC.Core.BLL.Web;
using FFRC.Core.PianetaFantacalcio.BLL.Names;
using FFRC.Core.PianetaFantacalcio.BLL.Rankings;
using FFRC.Core.Rankings;
using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FFRC.Console
{
    public partial class MainForm : Form
    {
        private IWebBLL WebBLL;
        private IRankingsBLL RankingsBLL;
        private INamesBLL NamesBLL;
        private Lazy<string> _page;
        private Lazy<IDictionary<string, double>> _playersTable;

        public MainForm()
        {
            InitializeComponent();
            InitializeBLLs();
            
            _page = new Lazy<string>(() => WebBLL.RetrievePage(ConfigurationManager.AppSettings["RankingsUrl"]));
            _playersTable = new Lazy<IDictionary<string, double>>(() => RankingsBLL.GetPlayersRankingTable(_page.Value));

            BindWeeks();
        }

        private void InitializeBLLs()
        {
            WebBLL = new WebBLL();
            RankingsBLL = new RankingsBLL();
            NamesBLL = new NamesBLL();
        }

        private void BindWeeks()
        {
            Enumerable
                .Range(1, 38)
                .ForEach(x => comboWeek.Items.Add(x));

            int week = RankingsBLL.GetSelectedWeek(_page.Value);
            comboWeek.SelectedIndex = week == 0 ? week : week - 1;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (playersListBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("No players name were inserted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            string[] players = 
                playersListBox
                    .Text
                    .Split(Environment.NewLine.ToCharArray())
                    .Select(x => x.ToLowerInvariant())
                    .ToArray();

            if(players.Length == 0)
            {
                MessageBox.Show("No players name were inserted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            IList<IDictionary<string, double>> playersRankings = new List<IDictionary<string, double>>();
            foreach (string player in players)
            {
                IDictionary<string, double> playersForName =
                    _playersTable
                        .Value
                        .Where(x => NamesBLL.RetrievePlayerName(x.Key) == player)
                        .ToDictionary(x => x.Key, x => x.Value);

                if(playersForName.Count > 0)
                {
                    playersRankings.Add(playersForName);
                }
            }

            var duplicates = playersRankings.Where(x => x.Count > 1);
            if(duplicates.Any())
            {

            }

            var flattenedPlayersRankings = 
                playersRankings
                    .SelectMany(x => x)
                    .ToDictionary(x => x.Key, x => x.Value);

            playersListBox.Text = 
                string
                    .Join
                    (
                        Environment.NewLine,
                        flattenedPlayersRankings.Select(x => "{0} {1}".FormatWith(x.Key, x.Value))
                    );

            playersListBox.Text += Environment.NewLine + Environment.NewLine;
            playersListBox.Text +=
                "TOT: {0}"
                    .FormatWith
                    (
                        flattenedPlayersRankings.Sum(x => x.Value)
                    );
        }
    }
}
