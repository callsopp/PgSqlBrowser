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
    public partial class ObjectInfoPane : Form
    {
        private DataSet _ds = null;
        public ObjectInfoPane(DataSet ds)
        {
            _ds = ds;
            InitializeComponent();
        }

        private void ObjectInfoPane_Load(object sender, EventArgs e)
        {
            foreach (DataTable dt in _ds.Tables)
            {
                TableLayoutPanel tlp = new TableLayoutPanel();
                tlp.CellPaint += new TableLayoutCellPaintEventHandler(cellPainter);
                tlp.ColumnCount = dt.Columns.Count;
                tlp.RowCount = dt.Rows.Count+1;
                tlp.Dock = DockStyle.Top;
                
                bool firstIn = true;
                foreach(DataRow dr in dt.Rows)
                {
                    if (firstIn)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            Label colname = new Label();
                            colname.BackColor = Color.Aquamarine;
                            colname.ForeColor = Color.Gray;
                            colname.Font = new Font("Tahoma", 8, FontStyle.Bold | FontStyle.Underline);
                            colname.Text = dt.Columns[i].ColumnName;
                            colname.AutoSize = true;
                            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp.Controls.Add(colname, i, 0);
                        }
                        firstIn = false;
                    }

                    for(int i = 0; i < dt.Columns.Count; i++)
                    {
                        Label lb = new Label();
                        lb.Text = "";
                        lb.BackColor = Color.Aquamarine;
                        lb.Font = new Font("Tahoma", 8, FontStyle.Regular);
                        if(dr[i] != System.DBNull.Value)
                        {
                            lb.Text = dr[i].ToString();
                        }
                        lb.AutoSize = true;
                        tlp.Controls.Add(lb, i, dt.Rows.IndexOf(dr)+1);
                    }
                }
                tlp.AutoSize = true;
                this.Controls.Add(tlp);
            }
        }

        void cellPainter(object sender, TableLayoutCellPaintEventArgs e)
        {
            if(e.Row == 0)
            {
                e.Graphics.FillRectangle(Brushes.Aquamarine, e.CellBounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Aquamarine, e.CellBounds);
            }
        }

        private void ObjectInfoPane_ControlAdded(object sender, ControlEventArgs e)
        {
            this.Refresh();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;

        }
    }
}
