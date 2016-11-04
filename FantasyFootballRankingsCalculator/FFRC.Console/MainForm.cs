using FFRC.Core.BLL;
using FFRC.Core.BLL.Web;
using FFRC.Core.PianetaFantacalcio.BLL.RankingsBLL;
using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FFRC.Console
{
    public partial class MainForm : Form
    {
        private IWebBLL WebBLL;
        private IRankingsBLL RankingsBLL;
        private Lazy<string> _page;

        public MainForm()
        {
            InitializeComponent();
            InitializeBLLs();
            
            _page = new Lazy<string>(() => WebBLL.RetrievePage(ConfigurationManager.AppSettings["RankingsUrl"]));

            BindWeeks();
        }

        private void InitializeBLLs()
        {
            WebBLL = new WebBLL();
            RankingsBLL = new RankingsBLL();
        }

        private void BindWeeks()
        {
            Enumerable
                .Range(1, 38)
                .ForEach(x => comboWeek.Items.Add(x));
            
            comboWeek.SelectedIndex = RankingsBLL.GetSelectedWeek(_page.Value);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {

        }
    }
}
