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
    public partial class Delete : Form
    {
        public int Id;
        public Delete()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(int.TryParse(txtId.Text, out Id)) // make sure it is an int
            {
                this.Close(); //return to main page
            }
            else
            {
                MessageBox.Show("Invalid ID entered"); //error message
                txtId.Text = "";
            }
        }

        private void Delete_Load(object sender, EventArgs e)
        {
            txtId.Focus();
        }
    }
}
