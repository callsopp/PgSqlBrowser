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
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using System.Xml;
using Npgsql;
using AutocompleteMenuNS;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace PgSqlBrowser
{
    public partial class QueryWindow : DockContent
    {
        Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        Style MethodNameStyles = new TextStyle(Brushes.DarkBlue, null, FontStyle.Bold);
        private peDAC DAC = null;
        private string _serverName;
        private string _databaseName;
        private string _userName;
        private DataSet ds_public = null;
        private string _queryException = "";
        private string _LastQuery = "";
        private DataGridView dgInContext = null;
        private int pid = 0;
        private string _QueryString;
        private Stopwatch executionTimer;
        private string _FileNameWithPath;

//        select * from pg_stat_activity;
//select pg_sleep(1);
//select * from pg_stat_activity;
        public QueryWindow(peDAC NpgSqlConn, string QueryString = null, string FileNameWithPath = null)
        {
            DAC = NpgSqlConn;
            _QueryString = QueryString;
            _FileNameWithPath = FileNameWithPath;
            InitializeComponent();
        }



        private void QueryWindow_Load(object sender, EventArgs e)
        {

            splitContainer1.Panel2Collapsed = true;
            _serverName = DAC.serverName;
            _databaseName = DAC.databaseName;
            _userName = DAC.userName;
            if (DAC.conn.State != ConnectionState.Open)
            {
                MessageBox.Show(this, "Unable to connect to " + _serverName + System.Environment.NewLine + DAC.error_string       
                                , "Connection Failed"
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "Disconnected :(";
            }
            else
            {
                SetText();
            }
            fastColoredTextBox1.Language = Language.SQL;

            if (_QueryString != null)
            {
                fastColoredTextBox1.Text = _QueryString;
                fastColoredTextBox1.AllowDrop = true;
            }
        }

        private void SetText()
        {
            pid = DAC.conn.ProcessID;
            string justTheFilename = "";
            if (_FileNameWithPath != null)
            {
                justTheFilename = _FileNameWithPath.Split('\\')[_FileNameWithPath.Split('\\').Length - 1];
            }
            this.Text = "(" + pid.ToString() + ")" + justTheFilename;
            toolStripStatusLabel1.Text = "Connected: " + _userName + "@" + _serverName + "." + _databaseName;
        }

        private void fastColoredTextBox1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(GreenStyle);
            
            //comment highlighting
            e.ChangedRange.SetStyle(GreenStyle, @"--.*$", RegexOptions.Multiline);

            e.ChangedRange.SetStyle(GreenStyle, @"\/\*(\*(?!\/)|[^*])*\*\/", RegexOptions.Multiline);
            //postgres only words
            e.ChangedRange.ClearStyle(MethodNameStyles);
            e.ChangedRange.SetStyle(MethodNameStyles, @"create procedure");

            try
            {
                e.ChangedRange.ClearFoldingMarkers();
                
                e.ChangedRange.SetFoldingMarkers(@"/\*<", @">\*/");
            }catch(Exception eee)
            {

            }
        }

        private void runQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cancelToolStripMenuItem.Enabled = true;
            executionTimer = new Stopwatch();
            executionTimer.Start();
            runQueryToolStripMenuItem.Enabled = false;
            this.UseWaitCursor = true;
            this.Text = this.Text + " executing..";
            q_Worker.RunWorkerAsync();
        }

        private void OutputToResultContainer(DataSet ds)
        {
            int panelToRemove = -1;
            foreach(Panel p in splitContainer1.Panel2.Controls)
            {
                if(p.Tag.Equals("resultHolder"))
                {
                    panelToRemove = splitContainer1.Panel2.Controls.IndexOf(p);
                }
            }

            if (!panelToRemove.Equals(-1))
            {
                splitContainer1.Panel2.Controls.RemoveAt(panelToRemove);
            }

            Panel parentPanel = new Panel();
            parentPanel.AutoScroll = true;
            parentPanel.Tag = "resultHolder";
            parentPanel.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(parentPanel);

            TabControl tabs = new TabControl();
            TabPage queryTab = new TabPage("Results");
            TabPage messageTab = new TabPage("Messages");
            queryTab.AutoScroll = true;
            tabs.Controls.Add(queryTab);
            tabs.Controls.Add(messageTab);
            tabs.Dock = DockStyle.Fill;
            parentPanel.Controls.Add(tabs);

            if(ds.Tables.Count > 0)
            {
                /* add the datagrids to the result pane in reverse so they are in the correct order */
                for (int _dt = ds.Tables.Count-1; _dt >= 0; _dt--)
                {
                    if (ds.Tables[_dt].Rows.Count > 0)
                    {
                        Splitter DynSplitter = new Splitter();
                        DynSplitter.Dock = DockStyle.Top;
                        DynSplitter.BackColor = System.Drawing.Color.Tan;
                        DynSplitter.BorderStyle = BorderStyle.FixedSingle;
                        DynSplitter.Tag = "otherSplitter";
                        DataGridView dg = new DataGridView();
                        dg.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
                        dg.DoubleBuffered(true);
                        dg.ReadOnly = true;
                        dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dg.AllowUserToResizeRows = false;
                        dg.RowTemplate.Height = 16;
                        if (ds.Tables.IndexOf(ds.Tables[_dt]) == ds.Tables.Count - 1)
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
                        dg.MouseClick += new MouseEventHandler(SetDGInContext);
                        dg.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(SelectColumnContents);
                        dg.CellMouseClick += new DataGridViewCellMouseEventHandler(ShowContextMenu);
                        dg.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgGrid_RowPostPaint);
                        dg.DataSource = ds.Tables[_dt];
                        var thing = ds.Tables[_dt].Rows[0][0];
                        dg.AllowDrop = false;
                        dg.AllowUserToAddRows = false;
                        queryTab.Controls.Add(dg);
                        if (splitContainer1.Panel2Collapsed)
                        {
                            splitContainer1.Panel2Collapsed = false;
                        }
                       
                        for (int x = 0; x < ds.Tables[_dt].Columns.Count; x++ )
                        {
                            try
                            {
                                dg.Columns[x].SortMode = DataGridViewColumnSortMode.NotSortable;
                            }catch(Exception p)
                            { }
                        }
                        queryTab.Controls.Add(DynSplitter);
                    }
                }

                foreach (Panel p in splitContainer1.Panel2.Controls)
                {
                    if (p.Tag.Equals("resultHolder"))
                    {
                        foreach (Splitter s in p.Controls.OfType<Splitter>())
                        {
                            if (s.Tag.Equals("BottomSplitter"))
                            {
                                s.SplitPosition = 1500;
                            }
                        }
                    }
                }

                ds_public = ds;
                
            }
            else
            {
                tabs.Controls.Remove(queryTab);
            }



            string tBtext = @"/*" + System.Environment.NewLine + System.Environment.NewLine + _LastQuery + System.Environment.NewLine + System.Environment.NewLine + @"*/";
            ds_public = null;
            TextBox tB = new TextBox();
            tB.Multiline = true;
            tB.ReadOnly = true;
                
            if (_queryException.Length > 0)
            {
                tBtext = tBtext + System.Environment.NewLine + System.Environment.NewLine + _queryException;
            }
            tB.Text = tBtext;
            tB.Dock = DockStyle.Fill;
            messageTab.Controls.Add(tB);

            
            foreach (DataGridView dgv in this.Controls.OfType<DataGridView>())
            {
                foreach (DataGridViewColumn c in dgv.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    
                    c.ReadOnly = true;
                }
                
                dgv.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            }

            splitContainer1.Panel2Collapsed = false;
        }

        private void SelectColumnContents(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ColIndex = e.ColumnIndex;
            
            if (((DataGridView)sender).SelectionMode != DataGridViewSelectionMode.FullColumnSelect)
            {
                this.SuspendLayout();
                ((DataGridView)sender).ClearSelection();
                ((DataGridView)sender).SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                ((DataGridView)sender).CurrentCell = ((DataGridView)sender)[ColIndex, 0];
                this.ResumeLayout();
            }
        }

        private void SetDGInContext(object sender, MouseEventArgs e)
        {
            dgInContext = (DataGridView)sender;
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



        void ShowContextMenu(object sender, DataGridViewCellMouseEventArgs e)
        {
            /* now always set any clicks other than those on the headers to reset the column select mode */
            
            if (e.RowIndex != -1)
            {
                this.SuspendLayout();
                ((DataGridView)sender).SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                this.ResumeLayout();
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dgContextMenu.Show(Cursor.Position);
            }
        }


        private void showResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
            }
        }

        private void showInPopoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryResultsWindow QRW = new QueryResultsWindow(ds_public);
            QRW.Show();
        }

        private void objectInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.SelectedText.Length > 0)
            {
                string sql_text = @"
                                select * from pg_views where schemaname = <REPLACESCHEMA> and viewname = '<REPLACEOBJECT>';

                                select relname, n_live_tup, n_dead_tup,last_vacuum,last_autovacuum,last_analyze,last_autoanalyze from 
                                (
                                select relname, n_live_tup, n_dead_tup,last_vacuum,last_autovacuum,last_analyze,last_autoanalyze from pg_stat_sys_tables
                                union
                                select relname, n_live_tup, n_dead_tup,last_vacuum,last_autovacuum,last_analyze,last_autoanalyze from pg_stat_user_tables
                                )x where x.relname = '<REPLACEOBJECT>';

                                select
                                t.relname as table_name,
                                i.relname as index_name,
                                (case when ix.indisunique then 'Unique' else 'Non-Unique' end) as is_unique,
                                (case when ix.indisprimary then 'PRIMARY KEY - ' else '' end) || (case when ix.indisclustered then 'CLUSTERED' else 'NON-CLUSTERED' end) as index_type,
                                string_agg(a.attname,',') as index_columns
                                from pg_class t
                                inner join pg_index ix on t.oid = ix.indrelid
                                inner join pg_class i on i.oid = ix.indexrelid
                                inner join pg_attribute a on a.attrelid = t.oid
                                inner join pg_type y on y.oid = a.atttypid
                                where a.attnum = ANY(ix.indkey)
                                and t.relkind = 'r'
                                and t.relname = '<REPLACEOBJECT>'
                                group by t.relname,i.relname,ix.indisunique,ix.indisprimary,ix.indisclustered
                                order by t.relname,i.relname ;

                                select column_name, data_type, is_nullable from information_schema.columns where table_name = '<REPLACEOBJECT>';
                               ";
                
                string replace_schema = "schemaname";
                string replace_object = fastColoredTextBox1.SelectedText;

                if(fastColoredTextBox1.SelectedText.Contains("."))
                {
                    string[] str = fastColoredTextBox1.SelectedText.ToString().Split('.');
                    replace_schema = "'" + str[0] + "'";
                    replace_object = str[1];
                }

                NpgsqlCommand Command = new NpgsqlCommand(sql_text.Replace("<REPLACESCHEMA>", replace_schema).Replace("<REPLACEOBJECT>", replace_object));
                
                DataSet ds = new DataSet();
                _queryException = "";
                try
                {
                    Command.Connection = DAC.conn;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(Command);
                    da.Fill(ds);
                }
                catch (Exception execute_exception)
                {
                    _queryException = execute_exception.Message;
                    MessageBox.Show(this, _queryException, "Internal Query Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                OutputToResultContainer(ds);
            }
        }

        private void QueryWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                DAC.conn.Close();
            }catch(Exception ex)
            {
                // do nothing
            }
        }

        private void fastColoredTextBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)) || e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void fastColoredTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                fastColoredTextBox1.Text = node.Text;
            }
            else if (e.Data.GetDataPresent(typeof(string)))
            {
                fastColoredTextBox1.InsertText(e.Data.GetData(typeof(string)) as string);
            }
        }

        private void fastColoredTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgInContext.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    // Add the selection to the clipboard.
                    dgInContext.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                    Clipboard.SetDataObject(dgInContext.GetClipboardContent());
                    dgInContext.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {

                }
            }
        }

        void q_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string sql_text;
            if (fastColoredTextBox1.SelectedText.Length > 0)
            {
                sql_text = fastColoredTextBox1.SelectedText;
            }
            else
            {
                sql_text = fastColoredTextBox1.Text;
            }
            NpgsqlCommand Command = new NpgsqlCommand(sql_text);
            Command.CommandTimeout = 0;
            DataSet ds = new DataSet();
            _queryException = "";
            _LastQuery = sql_text;
            int _x = 0;
            Command.Connection = DAC.conn;
            
            if(Command.Connection.State != ConnectionState.Open)
            {
                Command.Connection.Open();
            }
            
            using (NpgsqlDataReader q_WorkerReader = Command.ExecuteReader()) 
            {
                    do
                    {
                        while (q_WorkerReader.HasRows)
                        {
                            if (this.q_Worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                                try
                                {
                                    DataTable dt = new DataTable();
                                    dt.BeginLoadData();
                                    dt.Load(q_WorkerReader);
                                    dt.EndLoadData();
                                    ds.Tables.Add(dt);
                                    _LastQuery += System.Environment.NewLine + "--" + dt.Rows.Count.ToString() + " row(s) affected.";
                                }
                                catch (System.Data.DataException de)
                                {
                                    if (de.Message.Contains("Invalid storage type: DBNull."))
                                    {
                                        Console.WriteLine(de.Message);
                                        _LastQuery += System.Environment.NewLine + "Command executed successfully";
                                    }
                                }
                            _x++;
                        }
                    } while (q_WorkerReader.NextResult());
            }
           
            e.Result = ds;
            
            if (pid != DAC.conn.ProcessID)
            {
                (sender as BackgroundWorker).ReportProgress(0, "pid_changed:" + DAC.conn.ProcessID);
            }
        }

        private void q_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                _queryException = e.Error.Message;
                if (e.Error.Message.Contains("no binary output function available"))
                {
                    _queryException += "  - try casting binary columns to ::text";
                }
                this.Text = this.Text + "(ERROR)";
                fastColoredTextBox1.BackColor = Color.OrangeRed;
                OutputToResultContainer(new DataSet());
            }
            else if (e.Cancelled)
            {
                this.Text = this.Text + "(CANCELLED)";
                fastColoredTextBox1.BackColor = Color.IndianRed;
                splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                OutputToResultContainer((DataSet)e.Result);
            }

            this.UseWaitCursor = false;
            runQueryToolStripMenuItem.Enabled = true;
            cancelToolStripMenuItem.Enabled = false;
            executionTimer.Stop();
            this.Text = this.Text.Replace(" executing..","");
        }

        private void queryTimerOutput_Tick(object sender, EventArgs e)
        {
            if (executionTimer != null && executionTimer.IsRunning)
            {
                TimeSpan ts = executionTimer.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
                executionTimeToolStripMenuItem.Text = elapsedTime;
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            q_Worker.CancelAsync();
        }

        private void fastColoredTextBox1_Click(object sender, EventArgs e)
        {
            ResetCancelledView();
        }

        private void fastColoredTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ResetCancelledView();
        }

        private void ResetCancelledView()
        {
            fastColoredTextBox1.BackColor = Color.White;
            this.Text = this.Text.Replace("(CANCELLED)","").Replace("(ERROR)","");
        }
        
    }

   
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
        public static void InvokeEx<T>(this T @this, Action<T> action) where T : Form
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, @this);
            }
            else
            {
                action(@this);
            }
        }
    }

}

