using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventConnect41330595
{

    public partial class Insert : Form
    {
        public int Id, Price;
        public DateTime selectedDate;
        public string Time, Venue;

        private void cmbVenue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cmbVenue.SelectedItem != null)
                {
                    if (cmbVenue.SelectedItem.ToString() == "Crystal Gardens Convention Center")
                    {
                        lblCapNotice.Text = "Venue capacity is 1000";
                        Venue = "Crystal Gardens Convention Center";
                    }
                    else if (cmbVenue.SelectedItem.ToString() == "Starlight Ballroom")
                    {
                        lblCapNotice.Text = "Venue capacity is 500";
                        Venue = "Starlight Ballroom";
                    }
                    else if (cmbVenue.SelectedItem.ToString() == "Serenity Plaza ")
                    {
                        lblCapNotice.Text = "Venue capacity is 300";
                        Venue = "Serenity Plaza ";
                    }
                    else if (cmbVenue.SelectedItem.ToString() == "Golden Pavilion")
                    {
                        lblCapNotice.Text = "Venue capacity is 500";
                        Venue = "Golden Pavilion";
                    }
                    else if (cmbVenue.SelectedItem.ToString() == "Emerald Hall")
                    {
                        lblCapNotice.Text = "Venue capacity is 200";
                        Venue = "Emerald Hall";
                    }
                    else if (cmbVenue.SelectedItem.ToString() == "Moonbeam Theater")
                    {
                        lblCapNotice.Text = "Venue capacity is 150";
                        Venue = "Moonbeam Theater";
                    }
                    else
                    {
                        lblCapNotice.Text = "Venue capacity is 100";
                        Venue = "Harmony Lounge";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Insert()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtId.Text, out Id)) // make sure it is an int
                {
                    if (int.TryParse(txtPrice.Text, out Price))
                    {
                        selectedDate = monthCalendar1.SelectionStart; // save the selected date in a variable

                        //see which timeslot was selected
                        if (rdoTen.Checked)
                        {
                            Time = "10:00";
                        }
                        else if (rdoTwelve.Checked)
                        {
                            Time = "12:00";
                        }
                        else if (rdoTwo.Checked)
                        {
                            Time = "14:00";
                        }
                        else if (rdoFour.Checked)
                        {
                            Time = "16:00";
                        }
                        else if (rdoSix.Checked)
                        {
                            Time = "18:00";
                        }
                        else
                        {
                            Time = "20:00";
                        }
                        
                        
                        this.Close(); //return to main page

                    }
                }
                else
                {
                    MessageBox.Show("Invalid ID entered"); //error message
                    txtId.Text = "";
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
