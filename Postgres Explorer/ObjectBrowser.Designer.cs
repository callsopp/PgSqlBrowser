namespace PgSqlBrowser
{
    partial class ObjectBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectBrowser));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tvImageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialContextMenuStrip1 = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.disconnectFromBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListDrag = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.materialContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.White;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.tvImageList;
            this.treeView1.Location = new System.Drawing.Point(0, 24);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(241, 408);
            this.treeView1.TabIndex = 0;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // tvImageList
            // 
            this.tvImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tvImageList.ImageStream")));
            this.tvImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tvImageList.Images.SetKeyName(0, "001-rhombus.jpg");
            this.tvImageList.Images.SetKeyName(1, "004-server.jpg");
            this.tvImageList.Images.SetKeyName(2, "003-multi-tab.jpg");
            this.tvImageList.Images.SetKeyName(3, "003-db.jpg");
            this.tvImageList.Images.SetKeyName(4, "002_layers.ico");
            this.tvImageList.Images.SetKeyName(5, "002-circular-shape-silhouette.jpg");
            this.tvImageList.Images.SetKeyName(6, "001-menu.jpg");
            this.tvImageList.Images.SetKeyName(7, "007-seqeunce.jpg");
            this.tvImageList.Images.SetKeyName(8, "008-system.jpg");
            this.tvImageList.Images.SetKeyName(9, "009-schema.jpg");
            this.tvImageList.Images.SetKeyName(10, "010-index.jpg");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(241, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("connectToolStripMenuItem.Image")));
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.connectToolStripMenuItem.ToolTipText = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // materialContextMenuStrip1
            // 
            this.materialContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialContextMenuStrip1.Depth = 0;
            this.materialContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disconnectFromBrowserToolStripMenuItem,
            this.scriptObjectToolStripMenuItem});
            this.materialContextMenuStrip1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialContextMenuStrip1.Name = "materialContextMenuStrip1";
            this.materialContextMenuStrip1.Size = new System.Drawing.Size(227, 48);
            // 
            // disconnectFromBrowserToolStripMenuItem
            // 
            this.disconnectFromBrowserToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.disconnectFromBrowserToolStripMenuItem.Name = "disconnectFromBrowserToolStripMenuItem";
            this.disconnectFromBrowserToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.disconnectFromBrowserToolStripMenuItem.Text = "Disconnect Server In Browser";
            this.disconnectFromBrowserToolStripMenuItem.Click += new System.EventHandler(this.disconnectFromBrowserToolStripMenuItem_Click);
            // 
            // scriptObjectToolStripMenuItem
            // 
            this.scriptObjectToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.scriptObjectToolStripMenuItem.Name = "scriptObjectToolStripMenuItem";
            this.scriptObjectToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.scriptObjectToolStripMenuItem.Text = "Script Object";
            this.scriptObjectToolStripMenuItem.Visible = false;
            this.scriptObjectToolStripMenuItem.Click += new System.EventHandler(this.scriptObjectToolStripMenuItem_Click);
            // 
            // imageListDrag
            // 
            this.imageListDrag.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListDrag.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListDrag.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ObjectBrowser
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 432);
            this.CloseButton = false;
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ObjectBrowser";
            this.Text = "ObjectBrowser";
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.ObjectBrowser_DragOver);
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.ObjectBrowser_GiveFeedback);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.materialContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private MaterialSkin.Controls.MaterialContextMenuStrip materialContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem scriptObjectToolStripMenuItem;
        private System.Windows.Forms.ImageList tvImageList;
        private System.Windows.Forms.ToolStripMenuItem disconnectFromBrowserToolStripMenuItem;
    }
}