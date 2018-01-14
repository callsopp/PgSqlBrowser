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
using MaterialSkin.Controls;
using MaterialSkin.Animations;
using MaterialSkin;

namespace PgSqlBrowser
{
    public partial class PgSqllBrowserMain : MaterialForm 
    {
        private peDAC dacInContext = null;
        private QueryWindow qwInContext = null;
        ObjectBrowser ob1 = new ObjectBrowser();
        int posX;
        int posY;
        bool drag;

        public PgSqllBrowserMain()
        {
            InitializeComponent();

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Grey100, Primary.Grey100,
                Primary.Grey100, Accent.Green100,
                TextShade.BLACK);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ob1.ConnSuccess += new ObjectBrowser.newConnSucessfull(CreateQueryWindowFromBrowser);
            ob1.dacInContext += new ObjectBrowser.DacInContextChanged(SetDACContextChanged);
            ob1.Show(dockPanel1, DockState.DockLeftAutoHide);
            ob1.OpenNewConnectBox();
        }
        
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Maximized))
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateQueryWindowFromBrowser(peDAC dac, string QueryText, EventArgs e)
        {
            dacInContext = dac;
            QueryWindow QW;
            QW = new QueryWindow(dac, QueryText);
            QW.qwInContext += new QueryWindow.QWInContextChanged(SetQueryWindowInContext);
            QW.Show(dockPanel1, DockState.Document);
        }

        private void SetQueryWindowInContext(QueryWindow f, EventArgs d)
        {
            qwInContext = f;
        }

        private void SetDACContextChanged(peDAC dac, EventArgs e)
        {
            dacInContext = dac;
        }

        private void newQueryWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNewQueryWindow();
        }

        private void OpenNewQueryWindow(string fileText = null, string fileNameWithPath = null)
        {
            if (dacInContext != null)
            {
                peDAC dac = new peDAC();
                dac.checkStatus += new peDAC.connectionAttemptStatus(UpdateStatus);
                dac.MakeConnection(dacInContext.serverName, ob1.DatabaseNameInContext, dacInContext.userName, dacInContext.pWord);
                QueryWindow QW;
                if (fileText != null)
                {
                    QW = new QueryWindow(dac, fileText, fileNameWithPath);
                }
                else
                {
                    QW = new QueryWindow(dac);
                }
                QW.qwInContext += new QueryWindow.QWInContextChanged(SetQueryWindowInContext);
                QW.Show(dockPanel1, DockState.Document);
            }
            else
            {
                ob1.OpenNewConnectBox();
            }
        }

        private void UpdateStatus(string s, EventArgs e)
        {
           // MessageBox.Show(s);
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                posX = Cursor.Position.X - PgSqllBrowserMain.ActiveForm.Left;
                posY = Cursor.Position.Y - Form.ActiveForm.Top;
            }
            this.Cursor = Cursors.Default;
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                this.WindowState = FormWindowState.Normal;
                this.Top = System.Windows.Forms.Cursor.Position.Y - posY;
                this.Left = System.Windows.Forms.Cursor.Position.X - posX;
            }
            this.Cursor = Cursors.Default;
        }

        private void menuStrip1_MouseLeave(object sender, EventArgs e)
        {
            drag = false;
        }

        private void menuStrip1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState.Equals(FormWindowState.Maximized))
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "SQL files (*.sql)|*.sql|" + "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fName in openFileDialog.FileNames)
                {
                    try
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(fName);
                        OpenNewQueryWindow(sr.ReadToEnd(), fName);
                        sr.Close();
                    }catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred trying to open file" + System.Environment.NewLine + ex.ToString());
                    }
                }
            }  
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (qwInContext != null)
            {
                qwInContext.SaveFile();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }


    }
}
