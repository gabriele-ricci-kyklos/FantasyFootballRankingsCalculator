using FFRC.Core.BLL.Web;
using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRC.Console
{
    public partial class MainForm : Form
    {
        private IWebBLL WebBLL;

        public MainForm()
        {
            InitializeComponent();

            WebBLL = new WebBLL();

            BindWeeks();
        }

        private void BindWeeks()
        {
            Enumerable
                .Range(1, 38)
                .ForEach(x => comboWeek.Items.Add(x));
        }
    }
}
