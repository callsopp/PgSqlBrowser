namespace PgSqlBrowser
{
    partial class QueryWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryWindow));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.runQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.executionTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInPopoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autocompleteMenu = new AutocompleteMenuNS.AutocompleteMenu();
            this.q_Worker = new System.ComponentModel.BackgroundWorker();
            this.queryTimerOutput = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.dgContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Bisque;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 648);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(883, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(90, 17);
            this.toolStripStatusLabel1.Text = "ServerConnText";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runQueryToolStripMenuItem,
            this.showResultsToolStripMenuItem,
            this.objectInfoToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, -2);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(304, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // runQueryToolStripMenuItem
            // 
            this.runQueryToolStripMenuItem.Name = "runQueryToolStripMenuItem";
            this.runQueryToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runQueryToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.runQueryToolStripMenuItem.Text = "Run Query";
            this.runQueryToolStripMenuItem.Click += new System.EventHandler(this.runQueryToolStripMenuItem_Click);
            // 
            // showResultsToolStripMenuItem
            // 
            this.showResultsToolStripMenuItem.Name = "showResultsToolStripMenuItem";
            this.showResultsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.showResultsToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.showResultsToolStripMenuItem.Text = "Show Results";
            this.showResultsToolStripMenuItem.Click += new System.EventHandler(this.showResultsToolStripMenuItem_Click);
            // 
            // objectInfoToolStripMenuItem
            // 
            this.objectInfoToolStripMenuItem.Name = "objectInfoToolStripMenuItem";
            this.objectInfoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.objectInfoToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.objectInfoToolStripMenuItem.Text = "Object Info";
            this.objectInfoToolStripMenuItem.Click += new System.EventHandler(this.objectInfoToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Enabled = false;
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.autocompleteMenu.SetAutocompleteMenu(this.fastColoredTextBox1, null);
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.fastColoredTextBox1.BackBrush = null;
            this.fastColoredTextBox1.CharHeight = 15;
            this.fastColoredTextBox1.CharWidth = 7;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.fastColoredTextBox1.IsReplaceMode = false;
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox1.ServiceColors")));
            this.fastColoredTextBox1.Size = new System.Drawing.Size(877, 149);
            this.fastColoredTextBox1.TabIndex = 0;
            this.fastColoredTextBox1.Zoom = 100;
            this.fastColoredTextBox1.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChanged_1);
            this.fastColoredTextBox1.Click += new System.EventHandler(this.fastColoredTextBox1_Click);
            this.fastColoredTextBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.fastColoredTextBox1_DragDrop);
            this.fastColoredTextBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.fastColoredTextBox1_DragEnter);
            this.fastColoredTextBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.fastColoredTextBox1_DragOver);
            this.fastColoredTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fastColoredTextBox1_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 23);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fastColoredTextBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Cyan;
            this.splitContainer1.Size = new System.Drawing.Size(877, 624);
            this.splitContainer1.SplitterDistance = 149;
            this.splitContainer1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(883, 670);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executionTimeToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(883, 20);
            this.menuStrip2.TabIndex = 4;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // executionTimeToolStripMenuItem
            // 
            this.executionTimeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.executionTimeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.executionTimeToolStripMenuItem.Enabled = false;
            this.executionTimeToolStripMenuItem.Name = "executionTimeToolStripMenuItem";
            this.executionTimeToolStripMenuItem.Size = new System.Drawing.Size(61, 16);
            this.executionTimeToolStripMenuItem.Text = "00:00:00";
            // 
            // dgContextMenu
            // 
            this.dgContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInPopoutToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem});
            this.dgContextMenu.Name = "dgContextMenu";
            this.dgContextMenu.Size = new System.Drawing.Size(175, 48);
            // 
            // showInPopoutToolStripMenuItem
            // 
            this.showInPopoutToolStripMenuItem.Name = "showInPopoutToolStripMenuItem";
            this.showInPopoutToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.showInPopoutToolStripMenuItem.Text = "Show In Popout";
            this.showInPopoutToolStripMenuItem.Click += new System.EventHandler(this.showInPopoutToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // autocompleteMenu
            // 
            this.autocompleteMenu.AllowsTabKey = true;
            this.autocompleteMenu.Colors = ((AutocompleteMenuNS.Colors)(resources.GetObject("autocompleteMenu.Colors")));
            this.autocompleteMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu.ImageList = null;
            this.autocompleteMenu.Items = new string[0];
            this.autocompleteMenu.TargetControlWrapper = null;
            // 
            // q_Worker
            // 
            this.q_Worker.WorkerSupportsCancellation = true;
            this.q_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.q_Worker_DoWork);
            this.q_Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.q_Worker_RunWorkerCompleted);
            // 
            // queryTimerOutput
            // 
            this.queryTimerOutput.Enabled = true;
            this.queryTimerOutput.Tick += new System.EventHandler(this.queryTimerOutput_Tick);
            // 
            // QueryWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 670);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "QueryWindow";
            this.Text = "QueryWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QueryWindow_FormClosed);
            this.Load += new System.EventHandler(this.QueryWindow_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.dgContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem runQueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showResultsToolStripMenuItem;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip dgContextMenu;
        private System.Windows.Forms.ToolStripMenuItem showInPopoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectInfoToolStripMenuItem;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu;
        private System.ComponentModel.BackgroundWorker q_Worker;
        private System.Windows.Forms.Timer queryTimerOutput;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem executionTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
    }
}