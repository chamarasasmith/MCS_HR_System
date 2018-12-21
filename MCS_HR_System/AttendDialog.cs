using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCS_HR_System
{
    public partial class AttendDialog : Form
    {
        MCSHRMEntities db = new MCSHRMEntities();
        string name1 = "";
        string id1 = "";
        int st = 0;
        public AttendDialog()
        {
            InitializeComponent();
            
        }
        public AttendDialog(int i1,string s1,string s2,string s3) {
            InitializeComponent();
            st = i1;
            name1 = s1;
            id1 = s3;
            label3.Text = name1;
            label6.Text = id1;
            label4.Text = s2;
            string hh = DateTime.Now.ToString("HH");
            string mm = DateTime.Now.ToString("mm");

            textBox1.Text = hh;
            textBox2.Text = mm;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                int yyyy = int.Parse(label4.Text.Split(':')[0]);
                int MM = int.Parse(label4.Text.Split(':')[1]);
                int dd = int.Parse(label4.Text.Split(':')[2]);

                int hh = int.Parse(textBox1.Text);
                int mm = int.Parse(textBox2.Text);
                DateTime d1 = new DateTime(yyyy, MM, dd, hh, mm, 0);

                if (st==1)
                {
                    Attendance att1 = db.Attendances.Where(x => x.Emp_ID.Trim() == id1.Trim() && x.Date1.Value.Year == d1.Year && x.Date1.Value.Month == d1.Month && x.Date1.Value.Day == d1.Day).SingleOrDefault();

                    if (att1==null)
                    {
                        Attendance att = new Attendance();
                        att.Enter_Time = d1;
                        att.Exit_Time = d1;
                        att.Emp_ID = label6.Text.Trim().ToString();
                        att.Status = "1";
                        att.Date1 = d1;
                        db.Attendances.Add(att);
                        db.SaveChanges();
                    }
                    else
                    {
                        att1.Enter_Time = d1;
                        db.SaveChanges();
                    }

                    
                    this.Close();
                }

                if (st == 2)
                {
                    Attendance att = db.Attendances.Where(x => x.Emp_ID.Trim() == id1.Trim() && x.Date1.Value.Year==d1.Year && x.Date1.Value.Month==d1.Month && x.Date1.Value.Day==d1.Day).SingleOrDefault();
                    if (att == null)
                    {
                        MessageBox.Show(name1+" didn't Come in "+label4.Text);
                        this.Close();
                    }
                    else
                    {
                        att.Exit_Time = d1;
                        db.SaveChanges();
                        this.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Unsuccess (Error : "+ex.Message+")");
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            int i=textBox1.Text.Length;

            int n;
            bool isNumeric = int.TryParse(textBox1.Text, out n);

            if (isNumeric)
            {
                if (i >= 2)
                {
                    textBox2.Focus();
                    textBox2.SelectAll();
                }
            }
            else {
                textBox1.Text = "";
            }

            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int i = textBox2.Text.Length;
            
            if (i >= 2)
            {
                
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                MessageBox.Show("Success");
            }
        }

        private void AttendDialog_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
