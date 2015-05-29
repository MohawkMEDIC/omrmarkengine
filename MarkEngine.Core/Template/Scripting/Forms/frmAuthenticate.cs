using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmrMarkEngine.Core.Template.Scripting.Forms
{
    /// <summary>
    /// Username and password authentication dialog
    /// </summary>
    public partial class frmAuthenticate : Form
    {

        /// <summary>
        /// Gets or sets the username on the stcreen
        /// </summary>
        public String Username
        {
            get { return this.txtUsername.Text; }
            set { this.txtUsername.Text = value; }
        }

        /// <summary>
        /// Gets or sets the password on the screen
        /// </summary>
        public String Password
        {
            get { return this.txtPassword.Text; }
            set { this.txtPassword.Text = value; }
        }

        public frmAuthenticate()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPassword.Text) || String.IsNullOrEmpty(txtUsername.Text))
                MessageBox.Show("Username or password not provided");
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
