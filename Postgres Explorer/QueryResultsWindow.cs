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
    public partial class QueryResultsWindow : Form
    {
        private DataSet _ds = null;

        public QueryResultsWindow(DataSet ds)
        {
            _ds = ds;
            InitializeComponent();
        }

        private void QueryResultsWindow_Load(object sender, EventArgs e)
        {
            Panel parentPanel = new Panel();
            parentPanel.AutoScroll = true;
            parentPanel.BackColor = Color.White;
            parentPanel.Tag = "resultHolder";
            parentPanel.Dock = DockStyle.Fill;
            this.Controls.Add(parentPanel);

            /* add the datagrids to the result pane in reverse so they are in the correct order */
            for (int _dt = _ds.Tables.Count - 1; _dt >= 0; _dt--)
            {
                Splitter DynSplitter = new Splitter();
                DynSplitter.Dock = DockStyle.Top;
                DynSplitter.BackColor = System.Drawing.Color.Tan;
                DynSplitter.BorderStyle = BorderStyle.FixedSingle;
                DynSplitter.Tag = "otherSplitter";
                DataGridView dg = new DataGridView();
                dg.DoubleBuffered(true);
                dg.ReadOnly = true;
                dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dg.AllowUserToResizeRows = false;
                dg.RowTemplate.Height = 16;
                if (_ds.Tables.IndexOf(_ds.Tables[_dt]) == _ds.Tables.Count - 1)
                {
                    dg.Dock = DockStyle.Fill;
                }
                else
                {
                    dg.Dock = DockStyle.Top;
                }
                dg.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dg.BackgroundColor = Color.White;
                dg.DefaultCellStyle.Font = new Font("Arial", 8);
                dg.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgGrid_RowPostPaint);
                dg.DataSource = _ds.Tables[_dt];
                dg.AllowDrop = false;
                dg.AllowUserToAddRows = false;
                parentPanel.Controls.Add(dg);
                for (int x = 0; x < _ds.Tables[_dt].Columns.Count; x++)
                {
                    dg.Columns[x].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                parentPanel.Controls.Add(DynSplitter);
            }

        }

        private void dgGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            //get the size of the string
            Size textSize = TextRenderer.MeasureText(rowIdx, this.Font);
            //if header width lower than string width then resize
            if (grid.RowHeadersWidth < textSize.Width + 40)
            {
                grid.RowHeadersWidth = textSize.Width + 40;
            }

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        
    }
}
