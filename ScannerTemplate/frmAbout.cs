using OmrMarkEngine.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateDesigner
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            lblEngineVersion.Text = typeof(Engine).Assembly.GetName().Version.ToString();
            lblVersion.Text = typeof(frmAbout).Assembly.GetName().Version.ToString();

        }

        private void lnkCodePlex_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://omrmarkengine.codeplex.com") { UseShellExecute = true });

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://bitbucket.org/fyfesoftware/sketchy") { UseShellExecute = true });

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://www.archlinux.org/packages/extra/any/oxygen-icons/download") { UseShellExecute = true });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://www.aforgenet.com/") { UseShellExecute = true });

            
        }

        
    }
}
