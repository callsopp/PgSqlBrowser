using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using WeifenLuo.WinFormsUI.Docking;

namespace PgSqlBrowser
{
    public partial class ObjectBrowser : DockContent
    {
        public event newConnSucessfull ConnSuccess;
        public EventArgs e;
        public delegate void newConnSucessfull(peDAC _nuDac, string ObjectText, EventArgs e);
        private string currentConnStatus;
        
        public event DacInContextChanged dacInContext;
        public EventArgs d;
        public delegate void DacInContextChanged(peDAC _nuDac, EventArgs d);

        public static List<peDAC> DAC = new List<peDAC>();
        private peDAC DACInContextLocal;
        private string _queryException;
        private string _sname;
        private string _dbname;
        public bool HasConnectionToServerButDifferentDatabase = false; /* this is used when a DAC exists for the server but not the database (for opening new query tabs) */
        public string DatabaseNameInContext;
        private System.Windows.Forms.ImageList imageListDrag;

        public ObjectBrowser()
        {
            InitializeComponent();
        }

        ~ObjectBrowser()
        {
            DAC.Clear();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNewConnectBox();
        }

        public void OpenNewConnectBox()
        {
            using (var form = new ConnectBox())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    AddServerToBrowserWindow(form.ServerName, form.UserName, form.pW);
                }
            }
        }

        private void AddServerToBrowserWindow(string serverName, string username = null, string pW = null)
        {
            // If already in the browser, close it and refresh it
            if (DAC.FindIndex(srv => srv.serverName.ToLower().Equals(serverName)) != -1)
            {
                peDAC _d = DAC.Find(d => d.serverName == _sname && d.databaseName == "postgres");
                try
                {
                    _d.conn.Close();
                    DAC.Remove(_d);
                    //treeView1.SelectedNode.Remove();
                    dacInContext(null, null);
                }
                catch (Exception b)
                {
                    // dont care
                }
            }



                /* ObjectBrowser always connects to postgres database */
                peDAC dac = new peDAC();
                dac.checkStatus += new peDAC.connectionAttemptStatus(UpdateStatus);
                dac.MakeConnection(serverName, "postgres", username, pW);
                if (dac.conn.State == ConnectionState.Open)
                {
                    ConnSuccess(dac, null, null);
                    DAC.Add(dac);

                    string sql_text = @"select schema_name from information_schema.schemata order by schema_name asc;
                                        select datname from pg_database order by datname asc;
                                        select schemaname, tablename from pg_tables order by schemaname asc , tablename asc;
                                        select schemaname, tablename, indexname, tablespace, indexdef from pg_indexes order by schemaname asc, tablename asc, indexname asc, tablespace asc, indexdef asc;
                                        SELECT r.routine_schema, r.routine_name FROM information_schema.routines r group by r.routine_schema, r.routine_name ORDER BY r.routine_schema asc, r.routine_name asc;
                                        select schemaname,viewname,viewowner,definition from pg_views order by schemaname asc,viewname asc;
                                        SELECT sequence_schema,sequence_name FROM information_schema.sequences;

                                ";
                    NpgsqlCommand Command = new NpgsqlCommand(sql_text);
                    DataSet ds = new DataSet();
                    _queryException = "";
                    try
                    {
                        Command.Connection = dac.conn;
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(Command);
                        da.Fill(ds);
                        TreeNode rootNode = null;
                        Dictionary<string, string> rootNodeTag = new Dictionary<string, string>();
                        rootNodeTag.Add("servername", dac.serverName);
                        rootNodeTag.Add("database", "postgres");
                        rootNodeTag.Add("rootnode", "true");
                        rootNode = treeView1.Nodes.Add(dac.serverName);
                        rootNode.Tag = rootNodeTag;
                        rootNode.ImageIndex = 1;
                        rootNode.SelectedImageIndex = 1;

                        TreeNode parentNode = null;
                        parentNode = rootNode.Nodes.Add("Databases");
                        Dictionary<string, string> parentNodeTag = new Dictionary<string, string>();
                        parentNodeTag.Add("servername", dac.serverName);
                        parentNodeTag.Add("database", "postgres");
                        parentNode.Tag = parentNodeTag;
                        parentNode.ImageIndex = 2;
                        parentNode.SelectedImageIndex = 2;
                        TreeNode childNode;

                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            childNode = parentNode.Nodes.Add((string)dr["datname"]);
                            childNode.ImageIndex = 3;
                            childNode.SelectedImageIndex = 3;
                            Dictionary<string, string> childNodeTag = new Dictionary<string, string>();
                            childNodeTag.Add("servername", dac.serverName);
                            childNodeTag.Add("database", (string)dr["datname"]);
                            childNode.Tag = childNodeTag;


                            foreach (DataRow schema in ds.Tables[0].Rows)
                            {
                                TreeNode schemaNode;
                                schemaNode = childNode.Nodes.Add((string)schema["schema_name"]);
                                Dictionary<string, string> schemaNodeTag = new Dictionary<string, string>();
                                schemaNodeTag.Add("servername", dac.serverName);
                                schemaNodeTag.Add("database", (string)dr["datname"]);
                                schemaNode.Tag = schemaNodeTag;

                                TreeNode innerTableChildNode;
                                innerTableChildNode = schemaNode.Nodes.Add("Tables");
                                Dictionary<string, string> innerTableChildNodeTag = new Dictionary<string, string>();
                                innerTableChildNodeTag.Add("servername", dac.serverName);
                                innerTableChildNodeTag.Add("database", (string)dr["datname"]);
                                innerTableChildNode.ImageIndex = 4;
                                innerTableChildNode.SelectedImageIndex = 4;
                                innerTableChildNode.Tag = innerTableChildNodeTag;


                                foreach (DataRow idr in ds.Tables[2].Rows)
                                {
                                    if ((string)idr["schemaname"] == (string)schema["schema_name"])
                                    {
                                        TreeNode innerObjectsChildNode;
                                        Dictionary<string, string> innerObjectsChildNodeTag = new Dictionary<string, string>();
                                        innerObjectsChildNodeTag.Add("servername", dac.serverName);
                                        innerObjectsChildNodeTag.Add("database", (string)dr["datname"]);
                                        innerObjectsChildNode = innerTableChildNode.Nodes.Add((string)idr["schemaname"] + "." + (string)idr["tablename"]);
                                        innerObjectsChildNode.Tag = innerObjectsChildNodeTag;
                                        TreeNode innerIndexChildNode;
                                        Dictionary<string, string> innerIndexChildNodeTag = new Dictionary<string, string>();
                                        innerIndexChildNodeTag.Add("servername", dac.serverName);
                                        innerIndexChildNodeTag.Add("database", (string)dr["datname"]);
                                        innerIndexChildNode = innerObjectsChildNode.Nodes.Add("Indexes");
                                        innerIndexChildNode.Tag = innerIndexChildNodeTag;
                                        innerIndexChildNode.ImageIndex = 5;
                                        innerIndexChildNode.SelectedImageIndex = 5;
                                        foreach (DataRow ixdr in ds.Tables[3].Rows)
                                        {
                                            TreeNode innerIndexObjectsChildNode;
                                            if ((string)idr["schemaname"] + "." + (string)idr["tablename"] == (string)ixdr["schemaname"] + "." + (string)ixdr["tablename"])
                                            {
                                                innerIndexObjectsChildNode = innerIndexChildNode.Nodes.Add((string)ixdr["indexname"]);
                                                Dictionary<string, string> indexTag = new Dictionary<string, string>();
                                                indexTag.Add("servername", dac.serverName);
                                                indexTag.Add("database", (string)dr["datname"]);
                                                indexTag.Add("indexdefinition", (string)ixdr["indexdef"]);
                                                innerIndexObjectsChildNode.Tag = indexTag;
                                                innerIndexObjectsChildNode.ImageIndex = -1;
                                                innerIndexObjectsChildNode.SelectedImageIndex = -1;

                                            }
                                        }
                                    }
                                }



                                TreeNode routinesNode;
                                routinesNode = schemaNode.Nodes.Add("Routines");
                                Dictionary<string, string> routinesNodeTag = new Dictionary<string, string>();
                                routinesNodeTag.Add("servername", dac.serverName);
                                routinesNodeTag.Add("database", (string)dr["datname"]);
                                routinesNode.ImageIndex = 5;
                                routinesNode.SelectedImageIndex = 5;
                                routinesNode.Tag = routinesNodeTag;

                                foreach (DataRow fr in ds.Tables[4].Rows)
                                {
                                    if ((string)fr["routine_schema"] == (string)schema["schema_name"])
                                    {
                                        TreeNode routinesChildNode;
                                        routinesChildNode = routinesNode.Nodes.Add((string)fr["routine_schema"] + "." + (string)fr["routine_name"]);
                                        Dictionary<string, string> routinesChildNodeTag = new Dictionary<string, string>();
                                        routinesChildNodeTag.Add("servername", dac.serverName);
                                        routinesChildNodeTag.Add("database", (string)dr["datname"]);
                                        routinesChildNodeTag.Add("routine_name", (string)fr["routine_schema"] + "." + (string)fr["routine_name"]);
                                        routinesChildNode.ImageIndex = -1;
                                        routinesChildNode.SelectedImageIndex = -1;
                                        routinesChildNode.Tag = routinesChildNodeTag;
                                    }
                                }


                                TreeNode viewsNode;
                                viewsNode = schemaNode.Nodes.Add("Views");
                                Dictionary<string, string> viewsNodeTag = new Dictionary<string, string>();
                                viewsNodeTag.Add("servername", dac.serverName);
                                viewsNodeTag.Add("database", (string)dr["datname"]);
                                viewsNode.ImageIndex = 6;
                                viewsNode.SelectedImageIndex = 6;
                                viewsNode.Tag = viewsNodeTag;
                                foreach (DataRow fr in ds.Tables[5].Rows)
                                {
                                    if ((string)fr["schemaname"] == (string)schema["schema_name"])
                                    {
                                        TreeNode viewsChildNode;
                                        viewsChildNode = viewsNode.Nodes.Add((string)fr["schemaname"] + "." + (string)fr["viewname"]);
                                        Dictionary<string, string> viewsChildNodeTag = new Dictionary<string, string>();
                                        viewsChildNodeTag.Add("servername", dac.serverName);
                                        viewsChildNodeTag.Add("database", (string)dr["datname"]);
                                        viewsChildNodeTag.Add("viewname", (string)fr["schemaname"] + "." + (string)fr["viewname"]);
                                        viewsChildNode.ImageIndex = -1;
                                        viewsChildNode.SelectedImageIndex = -1;
                                        viewsChildNode.Tag = viewsChildNodeTag;
                                    }
                                }


                                TreeNode sequencesNode;
                                sequencesNode = schemaNode.Nodes.Add("Sequences");
                                Dictionary<string, string> sequencesNodeTag = new Dictionary<string, string>();
                                sequencesNodeTag.Add("servername", dac.serverName);
                                sequencesNodeTag.Add("database", (string)dr["datname"]);
                                sequencesNode.ImageIndex = 7;
                                sequencesNode.SelectedImageIndex = 7;
                                sequencesNode.Tag = sequencesNodeTag;
                                foreach (DataRow sequence in ds.Tables[6].Rows)
                                {
                                    if ((string)sequence["sequence_schema"] == (string)schema["schema_name"])
                                    {
                                        TreeNode sequencesChildNode;
                                        sequencesChildNode = sequencesNode.Nodes.Add((string)sequence["sequence_schema"] + "." + (string)sequence["sequence_name"]);
                                        Dictionary<string, string> sequencesChildNodeTag = new Dictionary<string, string>();
                                        sequencesChildNodeTag.Add("servername", dac.serverName);
                                        sequencesChildNodeTag.Add("database", (string)dr["datname"]);
                                        sequencesChildNodeTag.Add("sequencename", (string)sequence["sequence_schema"] + "." + (string)sequence["sequence_name"]);
                                        sequencesChildNode.ImageIndex = -1;
                                        sequencesChildNode.SelectedImageIndex = -1;
                                        sequencesChildNode.Tag = sequencesChildNodeTag;
                                    }
                                }
                            }

                        }
                    }
                    catch (Exception execute_exception)
                    {
                        _queryException = execute_exception.Message;
                        MessageBox.Show(this, _queryException, "Internal Query Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Failed to connect to " + serverName + System.Environment.NewLine + currentConnStatus
                                        , "Connection Failure"
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Error);
                }
        }

        private void UpdateStatus(string s, EventArgs e)
        {
            currentConnStatus = s;
        }

        private void SetTreeviewNodeContext()
        {
            _sname = ((Dictionary<string, string>)treeView1.SelectedNode.Tag)["servername"];
            _dbname = ((Dictionary<string, string>)treeView1.SelectedNode.Tag)["database"];
            DatabaseNameInContext = _dbname;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
            SetTreeviewNodeContext();

            SetDACInContextOBLocal();
            
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bool contextItemsAvailable = false;
                scriptObjectToolStripMenuItem.Visible = false;
                disconnectFromBrowserToolStripMenuItem.Visible = false;

                treeView1.SelectedNode = e.Node;
                if (treeView1.SelectedNode.Tag != null)
                {

                    if (((Dictionary<string, string>)treeView1.SelectedNode.Tag).ContainsKey("rootnode"))
                    {
                        contextItemsAvailable = true;
                        disconnectFromBrowserToolStripMenuItem.Visible = true;
                    }
                    else if (((Dictionary<string, string>)treeView1.SelectedNode.Tag).ContainsKey("indexdefinition"))
                    {
                        contextItemsAvailable = true;
                        scriptObjectToolStripMenuItem.Visible = true;
                    }

                }
                if (contextItemsAvailable)
                {
                    materialContextMenuStrip1.Show(Cursor.Position);
                }

            }
        }

        private void SetDACInContextOBLocal()
        {
            /* try find a match - if there is no exact match then prepare a new DAC to the alternative database */
            peDAC _dtmp = DAC.Find(d => d.serverName == _sname && d.databaseName == _dbname);
            if (_dtmp == null)
            {
                /* look for the established connection to pull the username and password being used */
                peDAC _ddtmp = DAC.Find(d => d.serverName == _sname);
                if (_ddtmp != null)
                {
                    dacInContext(_ddtmp, null);
                    DACInContextLocal = _ddtmp;
                }

            }
            else
            {
                peDAC _dddtmp = DAC.Find(d => d.serverName == _sname);
                if (_dddtmp != null)
                {
                    dacInContext(_dtmp, null);
                    DACInContextLocal = _dtmp;
                }
                else
                {
                    MessageBox.Show(this, "Lost the DAC in context :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void scriptObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Dictionary<string, string>)treeView1.SelectedNode.Tag).ContainsKey("indexdefinition"))
            {
                ConnSuccess(DACInContextLocal, ((Dictionary<string, string>)treeView1.SelectedNode.Tag)["indexdefinition"], null);
            }
        }

        private void disconnectFromBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            peDAC _d = DAC.Find(d => d.serverName == _sname && d.databaseName == "postgres");
            try
            {
                _d.conn.Close();
                DAC.Remove(_d);
                treeView1.SelectedNode.Remove();
                dacInContext(null, null);
            }
            catch(Exception b)
            {
                // dont care
            }
        }


        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            treeView1.DoDragDrop(treeView1.SelectedNode.Text, DragDropEffects.Move);
        }

  

        private void ObjectBrowser_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }


        private void ObjectBrowser_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                // Show pointer cursor while dragging
                e.UseDefaultCursors = false;
                this.treeView1.Cursor = Cursors.Default;
            }
            else e.UseDefaultCursors = true;
        }


    }

    
}
