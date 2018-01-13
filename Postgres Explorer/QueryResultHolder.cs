using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PgSqlBrowser
{
    public partial class QueryResultHolder : DockContent
    {
        public peDAC _dac = null;

        public QueryResultHolder(peDAC dac)
        {
            _dac = dac;
            InitializeComponent();
        }

        private void QueryResultHolder_Load(object sender, EventArgs e)
        {

        }

                    
    }
}
