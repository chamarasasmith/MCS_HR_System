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
    public partial class Main2 : Form
    {
        MCSHRMEntities hr = new MCSHRMEntities();
        public Main2()
        {
            InitializeComponent();
            c1.Show();
            c2.Hide();
            c3.Hide();
            c4.Hide();
            label1.Text = "Employee";
            LoadSubView(1);

           
            
        }

        private void LoadSubView(int st) {

            panel5.Controls.Clear();

            Employee1 emp = new Employee1();
            Attendance1 att = new Attendance1();
            Leaves1 leav = new Leaves1();
            Staff_Movements sm= new Staff_Movements();
            
            emp.Dock = DockStyle.Fill;
            att.Dock = DockStyle.Fill;
            leav.Dock = DockStyle.Fill;
            sm.Dock = DockStyle.Fill;

            if (st==1)
            {
                panel5.Controls.Add(emp);
            }
            if (st==2)
            {
                panel5.Controls.Add(att);
            }
            if (st == 3)
            {
                panel5.Controls.Add(leav);
            }
            if (st == 4)
            {
                panel5.Controls.Add(sm);
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            c1.Hide();
            c2.Show();
            c3.Hide();
            c4.Hide();
            LoadSubView(2);
            label1.Text = "Attendence";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            c1.Show();
            c2.Hide();
            c3.Hide();
            c4.Hide();
            LoadSubView(1);
            label1.Text = "Employee";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            c1.Hide();
            c2.Hide();
            c3.Show();
            c4.Hide();
            LoadSubView(3);
            label1.Text = "Leave";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            c1.Hide();
            c2.Hide();
            c3.Hide();
            c4.Show();
            LoadSubView(4);
            label1.Text = "Staff Movements";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
