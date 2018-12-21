using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MCS_HR_System
{
    public partial class Attendance1 : UserControl
    {
        
        
        public Attendance1()
        {
            InitializeComponent();
            createGridView();
            loadEmployees();
            LoadGridView();
        }

        private void loadEmployees() {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    checkedListBox1.Items.Clear();

                    List<Employee> l1 = db.Employees.ToList();

                    foreach (var item in l1)
                    {
                        checkedListBox1.Items.Add(item.Emp_ID.Trim() + ":" + item.First_Name.Trim() + " " + item.Last_Name.Trim());
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : "+ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime d= monthCalendar1.SelectionRange.Start;
            var l= checkedListBox1.CheckedItems;
            foreach (var item in l)
            {
                new AttendDialog(1,item.ToString().Split(':')[1],d.ToString("yyyy:MM:dd"), item.ToString().Split(':')[0]).ShowDialog();
                //MessageBox.Show(item +" : "+ d.ToString());
            }
            LoadGridView();
            
        }

        public void createGridView() {
            try
            {
                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[0].Name = "Date";
                dataGridView1.Columns[1].Name = "Employee Name";
                dataGridView1.Columns[2].Name = "Enter Time";
                dataGridView1.Columns[3].Name = "Exit Time";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        public void LoadGridView()
        {

            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    dataGridView1.Rows.Clear();

                    DateTime d = monthCalendar1.SelectionRange.Start;

                    List<Attendance> em = db.Attendances.Where(x => x.Status.Trim() == "1" && x.Date1.Value.Year == d.Year && x.Date1.Value.Month == d.Month).ToList();

                    string[] row = new string[] { };

                    foreach (var item in em)
                    {
                        row = new string[] { item.Date1.Value.ToString("yyyy:MM:dd"), item.Employee.First_Name.Trim() + " " + item.Employee.Last_Name.Trim(), item.Enter_Time.Value.ToString("HH:mm"), item.Exit_Time.Value.ToString("HH:mm") };
                        dataGridView1.Rows.Add(row);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : "+ ex.Message);
                
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime d = monthCalendar1.SelectionRange.Start;
            var l = checkedListBox1.CheckedItems;
            foreach (var item in l)
            {
                new AttendDialog(2,item.ToString().Split(':')[1], d.ToString("yyyy:MM:dd"), item.ToString().Split(':')[0]).ShowDialog();
                //MessageBox.Show(item +" : "+ d.ToString());
            }
            LoadGridView();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadEmployees();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    dataGridView1.Rows.Clear();

                    dataGridView1.ColumnCount = 4;
                    dataGridView1.Columns[0].Name = "Date";
                    dataGridView1.Columns[1].Name = "Employee Name";
                    dataGridView1.Columns[2].Name = "Enter Time";
                    dataGridView1.Columns[3].Name = "Exit Time";

                    DateTime d = monthCalendar1.SelectionRange.Start;

                    List<Attendance> em = db.Attendances.ToList();

                    if (checkBox1.Checked)
                    {
                        em = db.Attendances.Where(x => (x.Date1.Value.Year == d.Year && x.Date1.Value.Month == d.Month && x.Date1.Value.Day==d.Day) && (x.Emp_ID.Contains(textBox1.Text) || x.Employee.First_Name.Contains(textBox1.Text) || x.Employee.Last_Name.Contains(textBox1.Text))).ToList();

                    }
                    else
                    {
                        em = db.Attendances.Where(x => x.Emp_ID.Contains(textBox1.Text) || x.Employee.First_Name.Contains(textBox1.Text) || x.Employee.Last_Name.Contains(textBox1.Text)).ToList();

                    }


                    string[] row = new string[] { };

                    foreach (var item in em)
                    {
                        row = new string[] { item.Date1.Value.ToString("yyyy:MM:dd"), item.Employee.First_Name.Trim() + " " + item.Employee.Last_Name.Trim(), item.Enter_Time.Value.ToString("HH:mm"), item.Exit_Time.Value.ToString("HH:mm") };
                        dataGridView1.Rows.Add(row);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid (" + ex.Message + ")");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog open = new SaveFileDialog()
                {
                    Filter = "Comma Separated Values(*.csv)|*.csv"
                };

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string path = open.FileName;
                    using (var w = new StreamWriter(path))
                    {

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            var c1 = row.Cells[0].Value.ToString();
                            var c2 = row.Cells[1].Value.ToString();
                            var c3 = row.Cells[2].Value.ToString();
                            var c4 = row.Cells[3].Value.ToString();
                            //var c5 = row.Cells[4].Value.ToString();
                            //var c6 = row.Cells[5].Value.ToString();
                            //var c7 = row.Cells[6].Value.ToString();
                            var line = string.Format("{0},{1},{2},{3}", c1, c2, c3, c4);
                            w.WriteLine(line);
                            w.Flush();
                        }
                        MessageBox.Show("Success..!!!");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadGridView();
        }
    }
}
