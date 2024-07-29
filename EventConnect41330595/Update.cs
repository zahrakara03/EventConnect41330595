using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventConnect41330595
{
    public partial class Update : Form
    {
        public int Id;
        public string choice;
        public Update()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtID.Text, out Id)) // make sure it is an int
            {
                if(rdoYes.Checked)
                {
                    choice = "Close";
                    this.Close(); //return to main page
                }
                else
                {
                    choice = "Open";
                    this.Close(); //return to main page
                }
            }
            else
            {
                MessageBox.Show("Invalid ID entered"); //error message
                txtID.Text = "";
            }
        }

        private void Update_Load(object sender, EventArgs e)
        {
            txtID.Focus();
        }
    }
}
