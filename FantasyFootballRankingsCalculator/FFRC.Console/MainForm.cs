using FFRC.Core.BE.Rankings;
using FFRC.Core.BLL.Names;
using FFRC.Core.PianetaFantacalcio.BLL.Names;
using FFRC.Core.PianetaFantacalcio.BLL.Rankings;
using FFRC.Core.Rankings;
using FFRC.Core.Support;
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
        private IRankingsBLL RankingsBLL;
        private INamesBLL NamesBLL;
        private string _url;

        public MainForm()
        {
            InitializeComponent();

            _url = ConfigurationManager.AppSettings["RankingsUrl"];

            InitializeBLLs();
            BindWeeks();
        }

        private void InitializeBLLs()
        {
            RankingsBLL = new RankingsBLL(_url);
            NamesBLL = new NamesBLL();
        }

        private void BindWeeks()
        {
            Enumerable
                .Range(1, 38)
                .ForEach(x => comboWeek.Items.Add(x));

            int week = RankingsBLL.GetSelectedWeek();
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

            PlayerStatsCollection playersRankings =
                RankingsBLL
                    .GetPlayersRankingsByNameList(players);

            //IList<PlayerStatsCollection> playersRankings = new List<PlayerStatsCollection>();
            //foreach (string player in players)
            //{
            //    PlayerStatsCollection playersForName =
            //        RankingsBLL
            //            .GetPlayersRankingsByNameList()

            //    if(playersForName.Count > 0)
            //    {
            //        playersRankings.Add(playersForName);
            //    }
            //}

            //var duplicates = playersRankings.Where(x => x.Count > 1);
            //if(duplicates.Any())
            //{

            //}

            //var flattenedPlayersRankings =
            //    playersRankings
            //        .SelectMany(x => x)
            //        .ToPlayerStatsCollection();

            playersListBox.Text = 
                string
                    .Join
                    (
                        Environment.NewLine,
                        playersRankings.Select(x => "{0} {1}".FormatWith(x.PlayerName, x.Ranking))
                    );

            playersListBox.Text += Environment.NewLine + Environment.NewLine;
            playersListBox.Text +=
                "TOT: {0}"
                    .FormatWith
                    (
                        playersRankings.Sum(x => x.Ranking.Value)
                    );
        }
    }
}
