using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace MCS_HR_System
{
    public partial class Leaves1 : UserControl
    {
        public Leaves1()
        {
            InitializeComponent();
            LoadGridView();
            loadEmployees();
            loadLeaveType();
        }


        public void LoadGridView()
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    dataGridView1.Rows.Clear();

                    DateTime d1= dateTimePicker1.Value;

                    dataGridView1.ColumnCount = 5;
                    dataGridView1.Columns[0].Name = "Leave ID";
                    dataGridView1.Columns[1].Name = "Date";
                    dataGridView1.Columns[2].Name = "Employee ID";
                    dataGridView1.Columns[3].Name = "Employee Name";
                    dataGridView1.Columns[4].Name = "Leave Type";

                    List<Leave> em = db.Leaves.Where(x => x.Status.Trim() == "1" && x.L_Date.Value.Day==d1.Day && x.L_Date.Value.Month == d1.Month && x.L_Date.Value.Year == d1.Year).ToList();

                    string[] row = new string[] {"", "EMP20171023001", "Chamara", "Sasmith" };
                    int count = 0;
                    foreach (var item in em)
                    {
                        row = new string[] {item.Leave_ID.ToString().Trim(), item.L_Date.Value.ToString("yyyy-MM-dd"),item.Emp_ID.Trim(), item.Employee.First_Name.Trim() + " " + item.Employee.Last_Name.Trim(), item.Leave_Type.Leave_Type1.Trim() };
                        dataGridView1.Rows.Add(row);
                        count++;
                    }
                    label6.Text = count + "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }


        private void loadEmployees()
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    comboBox1.Items.Clear();

                    List<Employee> l1 = db.Employees.ToList();
                    
                    foreach (var item in l1)
                    {
                        comboBox1.Items.Add(item.Emp_ID.Trim() + ":" + item.First_Name.Trim() + " " + item.Last_Name.Trim());

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void loadLeaveType()
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    comboBox2.Items.Clear();

                    List<Leave_Type> l1 = db.Leave_Type.ToList();

                    foreach (var item in l1)
                    {
                        comboBox2.Items.Add(item.LT_ID.ToString().Trim() + ":" + item.Leave_Type1.Trim());

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    int i1 = comboBox1.SelectedIndex;
                    int i2 = comboBox2.SelectedIndex;

                    if (i1 >= 0 && i2 >= 0)
                    {
                        string emp = comboBox1.SelectedItem.ToString().Split(':')[0];
                        string lt = comboBox2.SelectedItem.ToString().Split(':')[0];
                        DateTime d1 = dateTimePicker1.Value;

                        Leave l = new Leave();
                        l.Emp_ID = emp;
                        l.LT_ID = int.Parse(lt);
                        l.L_Date = d1;
                        l.Status = "1";
                        db.Leaves.Add(l);
                        db.SaveChanges();

                        MessageBox.Show("Success..!");
                        LoadGridView();

                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                       int ri= dataGridView1.SelectedRows[0].Index;

                        string s = dataGridView1.Rows[ri].Cells["Leave ID"].FormattedValue.ToString();

                        int lid = int.Parse(s);

                        Leave l= db.Leaves.First(x=>x.Leave_ID==lid);

                        db.Leaves.Remove(l);
                        db.SaveChanges();
                        
                        MessageBox.Show("Success..!");
                        LoadGridView();
                    }
                    else
                    {
                        MessageBox.Show("Please Select One");
                    }
                }
                    }
            catch (Exception ex)
            {
                MessageBox.Show("Error : "+ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {

                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    dataGridView1.Rows.Clear();
                    
                    dataGridView1.ColumnCount = 5;
                    dataGridView1.Columns[0].Name = "Leave ID";
                    dataGridView1.Columns[1].Name = "Date";
                    dataGridView1.Columns[2].Name = "Employee ID";
                    dataGridView1.Columns[3].Name = "Employee Name";
                    dataGridView1.Columns[4].Name = "Leave Type";

                    DateTime d = dateTimePicker1.Value;

                    List<Leave> em = db.Leaves.ToList();

                    if (checkBox1.Checked)
                    {
                        em = db.Leaves.Where(x => (x.L_Date.Value.Year == d.Year && x.L_Date.Value.Month == d.Month && x.L_Date.Value.Day == d.Day) && (x.Emp_ID.Contains(textBox1.Text) || x.Employee.First_Name.Contains(textBox1.Text) || x.Employee.Last_Name.Contains(textBox1.Text) || x.Leave_Type.Leave_Type1.Contains(textBox1.Text))).ToList();

                    }
                    if (checkBox2.Checked)
                    {
                        em = db.Leaves.Where(x => (x.L_Date.Value.Year == d.Year) && (x.Emp_ID.Contains(textBox1.Text) || x.Employee.First_Name.Contains(textBox1.Text) || x.Employee.Last_Name.Contains(textBox1.Text) || x.Leave_Type.Leave_Type1.Contains(textBox1.Text))).ToList();

                    }
                    if (checkBox3.Checked)
                    {
                        em = db.Leaves.Where(x => (x.L_Date.Value.Month == d.Month) && (x.Emp_ID.Contains(textBox1.Text) || x.Employee.First_Name.Contains(textBox1.Text) || x.Employee.Last_Name.Contains(textBox1.Text) || x.Leave_Type.Leave_Type1.Contains(textBox1.Text))).ToList();

                    }
                    if(checkBox4.Checked)
                    {

                        if (comboBox1.SelectedIndex>=0)
                        {
                            string empid = comboBox1.SelectedItem.ToString().Split(':')[0];
                            em = db.Leaves.Where(x => x.Emp_ID.Trim() == empid && x.Leave_Type.Leave_Type1.Contains(textBox1.Text)).ToList();

                        }

                    }
                    if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
                    {
                        
                        em = db.Leaves.Where(x => x.Emp_ID.Contains(textBox1.Text) || x.Employee.First_Name.Contains(textBox1.Text) || x.Employee.Last_Name.Contains(textBox1.Text)|| x.Leave_Type.Leave_Type1.Contains(textBox1.Text)).ToList();

                    }


                    string[] row = new string[] { };

                    int count = 0;

                    foreach (var item in em)
                    {
                        row = new string[] { item.Leave_ID.ToString().Trim(), item.L_Date.Value.ToString("yyyy-MM-dd"), item.Emp_ID.Trim(), item.Employee.First_Name.Trim() + " " + item.Employee.Last_Name.Trim(), item.Leave_Type.Leave_Type1.Trim() };
                        dataGridView1.Rows.Add(row);
                        count++;
                    }

                    label6.Text = count + "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid (" + ex.Message + ")");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                checkBox3.Checked = false;

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox4.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;

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
                            var c5 = row.Cells[4].Value.ToString();
                            //var c6 = row.Cells[5].Value.ToString();
                            //var c7 = row.Cells[6].Value.ToString();
                            var line = string.Format("{0},{1},{2},{3},{4}", c1, c2, c3, c4, c5);
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
    }
}
