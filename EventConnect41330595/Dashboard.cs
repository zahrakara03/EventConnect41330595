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
using System.Security.Cryptography.X509Certificates;

namespace EventConnect41330595
{
    public partial class Dashboard : Form
    {
        SqlConnection conn;//sql commands
        SqlCommand comm;
        SqlDataAdapter dataAdapter;
        SqlDataReader dataReader;
        string payment;
        public Dashboard()
        {
            InitializeComponent();
        }

        public void SetMembershipType(string membershipType)
        {
            if (membershipType == "Standard")
            {
                btnInsert.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else if (membershipType == "Premium")
            {
                btnInsert.Enabled = true;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Insert insert = new Insert(); //open insert form
            insert.ShowDialog();

            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sqlInsert = $"Insert INTO Hosting VALUES('{insert.Id}', '{insert.txtName.Text}', '{insert.txtDescription.Text}', '{insert.selectedDate}', '{insert.Time}', '{insert.Venue}', '{insert.txtCapacity.Text}', '{insert.Price}', '{insert.txtRegStat.Text}')";
                comm = new SqlCommand(sqlInsert, conn);
                dataAdapter.InsertCommand = comm;
                dataAdapter.InsertCommand.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                string sql = "SELECT * FROM Hosting"; //show all events including the ones that have been added now
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Hosting");

                //show all events in the datagrid
                dataGridHost.DataSource = ds;
                dataGridHost.DataMember = "Hosting";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update update = new Update(); // open update form
            update.ShowDialog();

            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sqlUpdate = $"UPDATE Hosting SET Registration = '{update.choice}' WHERE Id = '{update.Id}'"; //change the registration status
                comm = new SqlCommand(sqlUpdate, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.UpdateCommand = comm;
                dataAdapter.UpdateCommand.ExecuteNonQuery();
                conn.Close();


                conn.Open();
                string sql = "SELECT * FROM Hosting"; //show all events including one with updated registration status
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Hosting");

                //show all events in the datagrid
                dataGridHost.DataSource = ds;
                dataGridHost.DataMember = "Hosting";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Delete delete = new Delete();
                delete.ShowDialog();

                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form

                
                conn.Open();
                string sqlDel = $"DELETE FROM Hosting WHERE Id = '{delete.Id}'"; //delete based on id
                comm = new SqlCommand(sqlDel, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.DeleteCommand = comm;
                dataAdapter.DeleteCommand.ExecuteNonQuery(); //simply delete
                conn.Close();

                conn.Open();
                string sql = "SELECT * FROM Hosting"; // show without recently deleted event
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Hosting");

                //show events in the datagrid
                dataGridHost.DataSource = ds;
                dataGridHost.DataMember = "Hosting";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void tabPage1_Enter(object sender, EventArgs e)
        {
            txtMName.Focus();
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMyEvents_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sql = "SELECT EventName, EventDesription, Date, Time, Venue FROM MyEvents WHERE MemberName = '" + txtMName.Text + "'"; //select events for specific member
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "MyEvents");

                //show events in the datagrid
                dataGridMyEvents.DataSource = ds;
                dataGridMyEvents.DataMember = "MyEvents";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUName_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sql = $"UPDATE LoginInfo SET Name = '{txtUName.Text}' WHERE Password = '{txtCPassword.Text}'"; //update the username
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.UpdateCommand = comm;
                dataAdapter.UpdateCommand.ExecuteNonQuery(); //do not display onyl update
                conn.Close();

                conn.Open();
                string sql2 = $"UPDATE MyEvents SET MemberName = '{txtUName.Text}' WHERE MemberName = '{txtMName.Text}'"; //update the membername in the table contianing the events also
                comm = new SqlCommand(sql2, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.UpdateCommand = comm;
                dataAdapter.UpdateCommand.ExecuteNonQuery();
                conn.Close();
                txtCName.Text = txtUName.Text; //show new name for current name
                txtMName.Text = txtUName.Text;

                //update the table to show new name
                conn.Open();
                string sql3 = "SELECT EventName, EventDesription, Date, Time, Venue FROM MyEvents WHERE MemberName = '" + txtMName.Text + "'"; //display events with newly saved username
                comm = new SqlCommand(sql3, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "MyEvents");

                //show events in the datagrid
                dataGridMyEvents.DataSource = ds;
                dataGridMyEvents.DataMember = "MyEvents";

                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            
            txtCName.Focus();
            
        }

        private void btnUPassword_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sql = $"UPDATE LoginInfo SET Password = '{txtUPassword.Text}' WHERE Name = '{txtCName.Text}'"; //update password saved for member
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.UpdateCommand = comm;
                dataAdapter.UpdateCommand.ExecuteNonQuery(); //only update
                conn.Close();
                txtCPassword.Text = txtUPassword.Text; //update current password to show new password
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //exception handling
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {   //settings for scrollbar
            hScrollBar1.Value = 0;
            hScrollBar1.SmallChange = 50;
            hScrollBar1.LargeChange = 50;
            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = 1100;
        }

        private void btnUMembership_Click(object sender, EventArgs e)
        {
            //change member status
            if(rdoStandard.Checked) 
            {
                SetMembershipType("Standard");
            }
            else if (rdoPremium.Checked)
            {
                SetMembershipType("Premium");
            }
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            Application.Exit(); //close whole application
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {

            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sql = "SELECT * FROM Hosting"; //select all events that are being hosted
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Hosting");

                //show all hosted events in the datagrid
                dataGridHost.DataSource = ds;
                dataGridHost.DataMember = "Hosting";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit5_Click(object sender, EventArgs e)
        {
            Application.Exit(); //close whole application
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            txtPName.Focus();
        }

        private void btnPastEvents_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sql = "SELECT * FROM PastEvents WHERE MemberName = '" + txtPName.Text + "'"; //select past events for specific member
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Past");

                //show past events in the datagrid
                dataGridHistory.DataSource = ds;
                dataGridHistory.DataMember = "Past";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit4_Click(object sender, EventArgs e)
        {
            Application.Exit(); // close whole application
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sql = "SELECT * FROM Hosting WHERE Registration = 'Open'"; //select events where registration is still open
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Hosting");

                //show all open registration events in the datagrid
                dataGridSearch.DataSource = ds;
                dataGridSearch.DataMember = "Hosting";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //filter as you type
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sql = "SELECT * FROM Hosting WHERE Registration = 'Open' and Venue Like '%"+txtLocation.Text+"%' "; //select events that match the textbox criteria
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Hosting");

                //show all events that match the textbox in the datagrid
                dataGridSearch.DataSource = ds;
                dataGridSearch.DataMember = "Hosting";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart; //set variable for selected date on calender

            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string formattedDate = selectedDate.ToString("MM-dd-yyyy");
                string sql = "SELECT * FROM Hosting WHERE Registration = 'Open' and Date = '"+formattedDate+"'"; //select events based on the date the user selects
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Hosting");

                //show all events on a specific day in the datagrid
                dataGridSearch.DataSource = ds;
                dataGridSearch.DataMember = "Hosting";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            lblCapacity.Text = hScrollBar1.Value.ToString(); //display the value of the scrollbar in the label

            try
            {
                Form1 main = new Form1(); //link to main form
                conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                conn.Open();
                string sql = "SELECT * FROM Hosting WHERE Registration = 'Open' and Capacity < '" +hScrollBar1.Value.ToString()+ "'"; //select events that are less than the capacity on the scrollbar
                comm = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Hosting");

                //show all events that are less than the scrollbar capacity in the datagrid
                dataGridSearch.DataSource = ds;
                dataGridSearch.DataMember = "Hosting";

                conn.Close();
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridSearch.SelectedRows.Count > 0) //make sure a row is selected
                {
                    //see which payment method was selected
                    if (rdoBank.Checked)
                    {
                        payment = "a Bank Transfer";
                    }
                    else if (rdoDebit.Checked)
                    {
                        payment = "a Debit or Credit Card";
                    }
                    else
                    {
                        payment = "PayPal";
                    }

                    DataGridViewRow selectedRow = dataGridSearch.SelectedRows[0]; //save selected row

                    //initialize variables and save the selected row info in the variables
                    int id = Convert.ToInt32(selectedRow.Cells["Id"].Value) + 10; //add 10 so that ids do not clash
                    string eventName = selectedRow.Cells["Event"].Value.ToString();
                    string description = selectedRow.Cells["EventDescription"].Value.ToString();
                    DateTime date = Convert.ToDateTime(selectedRow.Cells["Date"].Value);
                    string time = selectedRow.Cells["Time"].Value.ToString();
                    string venue = selectedRow.Cells["Venue"].Value.ToString();
                    string registration = selectedRow.Cells["Registration"].Value.ToString();
                    string memberName = txtPayName.Text;

                    if(registration == "Open")
                    {
                        Form1 main = new Form1(); //link to main form
                        conn = new SqlConnection(main.connectionstring); // receive global connectionstring from main form
                        conn.Open();
                        string sqlInsert = $"Insert INTO MyEvents VALUES('{id}', '{memberName}', '{eventName}', '{description}', '{date}', '{time}', '{venue}')"; //insert the selected events details into myevents
                        comm = new SqlCommand(sqlInsert, conn);
                        dataAdapter.InsertCommand = comm;
                        dataAdapter.InsertCommand.ExecuteNonQuery();//only add into table do not display
                        conn.Close();

                        MessageBox.Show("You have successfully paid via " + payment + " for the event: " + eventName); //display message 
                    }
                    else
                    {
                        MessageBox.Show("Sorry this events registration is already closed"); //if registration is closed display this
                    }
                    
                }
                
            }

            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridSearch.SelectedRows.Count > 0) // see if there is a selected row
                {
                    DataGridViewRow selectedRow = dataGridSearch.SelectedRows[0]; // save selected row

                    //initialize variables and save the selected row info in the variables
                    int price = Convert.ToInt32(selectedRow.Cells["Price"].Value);
                    string eventName = selectedRow.Cells["Event"].Value.ToString();
                    string registration = selectedRow.Cells["Registration"].Value.ToString();

                    if (registration == "Open") //if registration status is open then show receipt
                    {
                        Receipt receipt = new Receipt();
                        receipt.lstReceipt.Items.Clear();
                        receipt.lstReceipt.Items.Add("Your payment of: R" + price + ".00 for  " + eventName + " event has been finalised you are now registered for this event."); //display info in receipt form
                        receipt.Show();
                    }
                    else
                    {
                        MessageBox.Show("Sorry this events registration is already closed"); // if registration is closed do not show receipt
                    }
                }
            }
            catch (Exception ex) //exception handling 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
