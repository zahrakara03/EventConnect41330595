using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EventConnect41330595
{
    public partial class Form1 : Form
    {
        SqlConnection conn;//sql commands
        SqlCommand comm;
        SqlDataAdapter dataAdapter;
        SqlDataReader dataReader;
        // global connection to database
        public String connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Lenovo1\OneDrive\Documents\Campus\Year 2\Semester 1\CMPG 212\EventConnect41330595\EventConnect41330595\Events.mdf"";Integrated Security=True";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtPassword.Text == "")
                {
                    errorProviderPassword.SetError(txtPassword, "Please enter a Password"); //error if password is not entered
                    txtPassword.Text = "";
                    txtPassword.Focus(); //let user add password
                }
                else
                {
                    conn = new SqlConnection(connectionstring);
                    conn.Open();
                    string sql = "SELECT * FROM LoginInfo WHERE Name = '" + txtName.Text + "'"; //compare the given username to the saved one
                    comm = new SqlCommand(sql, conn);
                    dataReader = comm.ExecuteReader();
                    while(dataReader.Read())
                    {
                        if(txtPassword.Text == dataReader.GetValue(2).ToString()) // compare the password that is given to the one saved for the user
                        {
                            Dashboard dashboard = new Dashboard();
                            if (txtMembership.Text == "Standard") //if user has a standard membership cannot host events
                            {
                                dashboard.SetMembershipType("Standard");
                            }
                            else if (txtMembership.Text == "Premium") //premium memebers can host events 
                            {
                                dashboard.SetMembershipType("Premium");
                            }
                            else
                            {
                                errorProviderPassword.SetError(txtMembership, "Can only be a standard or premium member!"); //error if any other membership was entered
                                txtMembership.Text = ""; //allow to reenter the membership type
                                txtMembership.Focus(); 
                            }
                            dashboard.Show();
                        }
                        else
                        {
                            errorProviderPassword.SetError(txtPassword, "Password invalid please try again!"); //error if password is not entered
                            txtPassword.Text = "";
                            txtPassword.Focus(); //let user add password
                        }

                        
                    }
                    conn.Close();
                }
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtName.Focus();
        }
    }
}
