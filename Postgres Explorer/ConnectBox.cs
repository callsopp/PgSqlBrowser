﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin.Animations;
using MaterialSkin;
using System.Diagnostics;

namespace PgSqlBrowser
{
    public partial class ConnectBox : MaterialForm
    {
        public string ServerName;
        public string UserName;
        public string pW;

        public ConnectBox()
        {
            InitializeComponent();

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Grey50, Primary.Grey50,
                Primary.Grey50, Accent.Teal700,
                TextShade.BLACK);

            snameTB.GotFocus += TB_GotFocus;
            unameTB.GotFocus += TB_GotFocus;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            SubmitForm();
        }

        private void TB_GotFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.Black;
        }

        private void pwordTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SubmitForm();
            }
        }

        private void SubmitForm()
        {
            ServerName = snameTB.Text;
            UserName = unameTB.Text;
            pW = pwordTB.Text;
            if (ServerName != "" && UserName != "" && pW != "")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Some parameters are missing", "Parameter Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ConnectBox_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            versionLabel.Text = "PgSqlBrowser v" + fvi.FileVersion;
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
